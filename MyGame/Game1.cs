#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace MyGame {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game {
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		enum GameStates { Start, Playing, GameOver }
		enum MenuOptions { Start=300, Restart=300, Quit=340 }
		GameStates GameState;
		MenuOptions menuCursor;

		Player player;
		Boss boss;
		bool bossFight;

		Rectangle rectBackground;
		ParallaxingBackground bgLayer1;
		ParallaxingBackground bgLayer2;

		List<Enemy> enemies;
		int maxEnemyCount;
		TimeSpan enemySpawnTime;
		TimeSpan prevEnemySpawnTime;
		Random random;

		List<Explosion> explosions;

		List<Laser> lasers;
		TimeSpan laserSpawnTime;
		TimeSpan previousLaserSpawnTime;

		Rectangle healthBarRec;
		int healthBarVal;
		int score;

		public Game1 () {
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;		
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize () {
			random = new Random ();

			player = new Player();
			boss = new Boss ();

			bgLayer1 = new ParallaxingBackground ();
			bgLayer2 = new ParallaxingBackground ();

			enemies = new List<Enemy> ();
			explosions = new List<Explosion> ();
			lasers = new List<Laser> ();

			enemySpawnTime = TimeSpan.FromSeconds (1.0f);
			prevEnemySpawnTime = TimeSpan.Zero;
			laserSpawnTime = TimeSpan.FromSeconds (0.5f);

			healthBarVal = Constants.PLAYER_HEALTH;
			maxEnemyCount = Constants.ENEMY_COUNT;
			score = 0;
			bossFight = false;

			GameState = GameStates.Start;
			menuCursor = MenuOptions.Start;
			base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent () {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			Graphics.load (Content);
			Sound.load (Content);

			Vector2 playerPosition = new Vector2 (GraphicsDevice.Viewport.TitleSafeArea.X,
				                         GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
			player.Initialize (Graphics.Player, playerPosition);

			bgLayer1.Initialize (Graphics.BackgroundLayer1, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
			bgLayer2.Initialize (Graphics.BackgroundLayer2, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);
			rectBackground = new Rectangle (0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

			healthBarRec = new Rectangle (15, 15, Graphics.HealthBar.Width, Graphics.HealthBar.Height);

			Vector2 bossPosition = new Vector2(GraphicsDevice.Viewport.Width + Graphics.Boss.Width,
				GraphicsDevice.Viewport.Height / 2 - Graphics.Boss.Height / 2);
			boss.Initialize (Graphics.Boss, bossPosition, -2f, 0f, false);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime) {
			Input.GetInputState ();

			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (Input.Quit())
				Exit ();
			#endif

			switch(GameState) {
				case GameStates.Start:
					UpdateStartMenu ();
					break;
				case GameStates.GameOver:
					UpdateGameOverScreen();
					break;
				case GameStates.Playing:
					UpdateGamePlay(gameTime);
					break;
			}
			base.Update (gameTime);
		}

		private void UpdateStartMenu() {
			if ( MediaPlayer.State != MediaState.Playing )
				MediaPlayer.Play (Sound.MenuMusic);

			if (Input.Start()) {
				if (menuCursor == MenuOptions.Quit)
					Exit ();

				GameState = GameStates.Playing;
				MediaPlayer.Stop ();
			}

			if (Input.Down()) menuCursor = MenuOptions.Quit;
			if (Input.Up())   menuCursor = MenuOptions.Start;
		}

		private void UpdateGameOverScreen() {
			if (Input.Start()) {
				if (menuCursor == MenuOptions.Quit)
					Exit ();

				player.Health = healthBarVal = Constants.PLAYER_HEALTH;
				score = 0;
				GameState = GameStates.Playing;
				player.Active = true;
				player.Position.X = GraphicsDevice.Viewport.TitleSafeArea.X;
				player.Position.Y = GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2;
				healthBarRec.Width = Graphics.HealthBar.Width;
				enemies.Clear ();
				bossFight = false;
				boss.Initialize(Graphics.Boss, new Vector2(GraphicsDevice.Viewport.Width + Graphics.Boss.Width, GraphicsDevice.Viewport.Height / 2 - Graphics.Boss.Height / 2), -2f, 0f, false);
				maxEnemyCount = Constants.ENEMY_COUNT;
			}

			if (Input.Down()) menuCursor = MenuOptions.Quit;
			if (Input.Up())   menuCursor = MenuOptions.Restart;
		}

		private void UpdateGamePlay(GameTime gt) {
			/* Set gameover if the player dies, or the boss dies during the boss fight.
			 * We want the explosion animations to finish as well, so we check to see if they were cleaned up.
			 */
			if ((!player.Active || (bossFight && !boss.Active)) && explosions.Count <= 0) {
				GameState = GameStates.GameOver;
				MediaPlayer.Stop ();
				return;
			}

			if (MediaPlayer.State != MediaState.Playing)
				MediaPlayer.Play (Sound.GameMusic);	
	
			if (player.Health < healthBarVal) {
				healthBarVal--;
				healthBarRec.Width = (int)(Graphics.HealthBar.Width * (healthBarVal / (float)Constants.PLAYER_HEALTH));
			}

			UpdatePlayer (gt);
			bgLayer1.Update (gt);
			bgLayer2.Update (gt);

			if (bossFight) UpdateBoss (gt);

			UpdateObjectLists (gt);
			UpdateCollision ();
		}

		private void UpdatePlayer(GameTime gameTime) {
			if (!player.Active) return;

			player.xSpeed = Input.LeftAnalogX () * Constants.PLAYER_MOVE_SPEED;
			player.ySpeed = Input.LeftAnalogY () * -Constants.PLAYER_MOVE_SPEED;

			if (Input.Left ())  player.Position.X -= Constants.PLAYER_MOVE_SPEED;
			if (Input.Right ()) player.Position.X += Constants.PLAYER_MOVE_SPEED;
			if (Input.Up ())    player.Position.Y -= Constants.PLAYER_MOVE_SPEED;
			if (Input.Down ())  player.Position.Y += Constants.PLAYER_MOVE_SPEED;

			if (Input.Shoot ()) {
				if (gameTime.TotalGameTime - previousLaserSpawnTime > laserSpawnTime) {
					previousLaserSpawnTime = gameTime.TotalGameTime;
					AddLaser (player.Position, 80, 25);
				}
			}

			player.Position.X = MathHelper.Clamp (player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
			player.Position.Y = MathHelper.Clamp (player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);
			player.Update (gameTime);
		}

		private void UpdateBoss(GameTime gt) {
			if (boss.xSpeed < 0f && (boss.Position.X < GraphicsDevice.Viewport.Width - boss.Width)) {
				boss.xSpeed = 0f;
				boss.ySpeed = 1f;
			}

			if (boss.xSpeed == 0f) {
				if (boss.Position.Y >= (GraphicsDevice.Viewport.Height - boss.Height))
					boss.ySpeed = -1f;
			
				if (boss.Position.Y <= 50f)
					boss.ySpeed = 1f;

				if (boss.Active && gt.TotalGameTime - prevEnemySpawnTime > enemySpawnTime) {
					prevEnemySpawnTime = gt.TotalGameTime;

					AddEnemy (new Vector2 (boss.Position.X + 20, boss.Position.Y));
					AddEnemy (new Vector2 (boss.Position.X + 20, boss.Position.Y + 200));
				}
			}
			boss.Update (gt);
		}

		private void UpdateObjectLists(GameTime gt) {
			if (!bossFight && (gt.TotalGameTime - prevEnemySpawnTime > enemySpawnTime)) {
				prevEnemySpawnTime = gt.TotalGameTime;
				AddEnemy( new Vector2 (GraphicsDevice.Viewport.Width + Graphics.Enemy.Width / 2, random.Next (50, GraphicsDevice.Viewport.Height - 50)));
				if (--maxEnemyCount <= 0) { 
					bossFight = true;
					boss.Active = true;
				}
			}

			for (int i = enemies.Count - 1; i >= 0; i--) {
				enemies [i].Update (gt);
				if (!enemies [i].Active) enemies.RemoveAt (i);
			}

			for (int i = explosions.Count - 1; i >= 0; i--) {
				explosions [i].Update (gt);
				if (!explosions [i].Active) explosions.RemoveAt (i);
			}

			for (int i = lasers.Count - 1; i >= 0; i--) {
				lasers [i].Update (gt);
				if (!lasers [i].Active) lasers.RemoveAt (i);
			}
		}

		private void UpdateCollision() {
			// Check to see if any enemies collide with the player or lasers.
			for (int i = 0; i < enemies.Count; i++) {
				if (player.Active && player.BoundingBox.Intersects (enemies [i].BoundingBox)) {
					player.Health -= enemies [i].DamageDealt;
					enemies [i].Health = 0;

					// switch to an explosion animation
					AddExplosion (enemies [i].Position, -50, -30, enemies[i].xSpeed, 30);

					if (player.Health <= 0) {
						AddExplosion (player.Position, 0, -30, 0f, 200);
						player.Active = false;
						player.Position = Vector2.Zero;
					}
				}

				for (int j = 0; j < lasers.Count; j++) {
					if (lasers [j].BoundingBox.Intersects (enemies [i].BoundingBox)) {
						enemies [i].Health = 0;
						AddExplosion (enemies [i].Position, -50, -30, -enemies [i].xSpeed, 30);
						lasers [j].Active = false;
						score += enemies [i].PointValue;
					}
				}
			}

			if (!boss.Active) return;

			// If the boss is active, check to see if it collides with lasers

			for (int i = 0; i < lasers.Count; i++ ) {
				if (!lasers [i].Active) continue;  // The current laser already hit something else.

				if (lasers [i].BoundingBox.Intersects (boss.BoundingBox)) {
					lasers [i].Active = false;
					boss.Health -= 10;

					if (boss.Health <= 0) {
						AddExplosion (boss.Position, 20, 50, 0f, 200);
						score += boss.PointValue;
					}
				}
			}
		}

		private void AddEnemy(Vector2 position) {
			Enemy enemy = new Enemy ();
			enemy.Initialize (Graphics.Enemy, position);
			enemies.Add (enemy);
		}


		private void AddLaser(Vector2 position, int xOffset, int yOffset) {
			position.X += xOffset;
			position.Y += yOffset;

			Laser laser = new Laser ();
			laser.Initialize (Graphics.Laser, position, GraphicsDevice.Viewport.Width);
			lasers.Add (laser);
			Sound.SFX_Laser.Play ();
		}

		private void AddExplosion(Vector2 position, int xOffset, int yOffset, float velocity, int animationSpeedMS) {
			position.X += xOffset;
			position.Y += yOffset;

			Explosion ex = new Explosion ();
			ex.Initialize (Graphics.Explosion, position, velocity, animationSpeedMS);
			explosions.Add (ex);
			Sound.SFX_Explosion.Play ();
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			spriteBatch.Begin ();

			switch (GameState) {
				case GameStates.Start:
					DrawStartMenu ();
					break;
				case GameStates.GameOver:
					DrawGameOverScreen ();
					break;
				case GameStates.Playing:
					DrawGamePlay ();
					break;
			}

			spriteBatch.End ();
			base.Draw (gameTime);
		}

		private void DrawStartMenu() {
			spriteBatch.Draw (Graphics.StartMenu, rectBackground, Color.White);

			spriteBatch.DrawString (Graphics.Font, ">", new Vector2 (325, (int)menuCursor), Color.White);
			spriteBatch.DrawString (Graphics.Font, "Start", new Vector2 (350, 300), Color.White);
			spriteBatch.DrawString (Graphics.Font, "Quit", new Vector2 (350, 340), Color.White);
		}

		private void DrawGameOverScreen() {
			spriteBatch.Draw (Graphics.GameOverScreen, rectBackground, Color.White);

			spriteBatch.DrawString (Graphics.Font, "Final Score: " + score, new Vector2 (300, 200), Color.White);

			spriteBatch.DrawString (Graphics.Font, ">", new Vector2 (325, (int)menuCursor), Color.White);
			spriteBatch.DrawString (Graphics.Font, "Restart", new Vector2 (350, 300), Color.White);
			spriteBatch.DrawString (Graphics.Font, "Quit", new Vector2 (350, 340), Color.White);
		}

		private void DrawGamePlay() {
			spriteBatch.Draw (Graphics.Background, rectBackground, Color.White);
			bgLayer1.Draw (spriteBatch);
			bgLayer2.Draw (spriteBatch);

			for (int i = 0; i < enemies.Count; i++)
				enemies [i].Draw (spriteBatch);

			for (int i = 0; i < explosions.Count; i++)
				explosions [i].Draw (spriteBatch);

			for (int i = 0; i < lasers.Count; i++)
				lasers [i].Draw (spriteBatch);

			if (bossFight) boss.Draw (spriteBatch);

			player.Draw (spriteBatch);

			spriteBatch.Draw (Graphics.HealthBar, healthBarRec, Color.White);
			spriteBatch.DrawString (Graphics.Font, "Score: " + score, new Vector2 (600, 10), Color.White);
		}
	}
}