using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Enemy {
		public Animation EnemyAnimation;
		public Vector2 Position;
		public bool Active;
		public int Health;
		public int Damage;
		public int Value;
		public float Speed;
		public int Width { get { return EnemyAnimation.FrameWidth; } }
		public int Height { get { return EnemyAnimation.FrameHeight; } }
		public Rectangle BoundingBox { get { return EnemyAnimation.BoundingBox; } }

		public void Initialize(Animation animation, Vector2 position) {
			EnemyAnimation = animation;
			Position = position;
			Active = true;
			Health = 10;
			Damage = 10;
			Speed = 6f;
			Value = 100;
		}

		public void Update(GameTime gameTime) {
			Position.X -= Speed;
			EnemyAnimation.Position = Position;
			EnemyAnimation.Update (gameTime);
			if (Position.X < -Width || Health <= 0) Active = false;
		}

		public void Draw(SpriteBatch sb){
			if (!Active) return;
			EnemyAnimation.Draw (sb);
		}
	}
}

