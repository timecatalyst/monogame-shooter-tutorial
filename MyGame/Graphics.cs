using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyGame {
	public static class Graphics {
		public static Texture2D StartMenu;
		public static Texture2D GameOverScreen;

		public static Texture2D Player;
		public static Texture2D Laser;
		public static Texture2D Enemy;
		public static Texture2D Explosion;
		public static Texture2D Boss;
		public static Texture2D HealthBar;

		public static Texture2D Background;
		public static Texture2D BackgroundLayer1;
		public static Texture2D BackgroundLayer2;

		public static SpriteFont Font;

		public static void load(ContentManager content) {
			StartMenu = content.Load<Texture2D> ("Graphics\\mainMenu");
			GameOverScreen = content.Load<Texture2D> ("Graphics\\endMenu");

			Player = content.Load<Texture2D> ("Graphics\\shipAnimation");
			Laser = content.Load<Texture2D> ("Graphics\\laser");
			Enemy = content.Load<Texture2D> ("Graphics\\mineAnimation");
			Explosion = content.Load<Texture2D> ("Graphics\\explosion");
			Boss = content.Load<Texture2D> ("Graphics\\boss");
			HealthBar = content.Load<Texture2D> ("Graphics\\healthBar");

			Background = content.Load<Texture2D> ("Graphics\\mainbackground");
			BackgroundLayer1 = content.Load<Texture2D> ("Graphics\\bgLayer1");
			BackgroundLayer2 = content.Load<Texture2D> ("Graphics\\bgLayer2");
		
			Font = content.Load<SpriteFont> ("gameFont");
		}
	}
}