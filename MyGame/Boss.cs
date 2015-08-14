using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Boss {
		public Texture2D Texture;
		public Vector2 Position;
		public bool Active;
		public int Health;
		public int Damage;
		public int Value;
		public int Width { get { return Texture.Width; } }
		public int Height { get { return Texture.Height; } }
		public float hSpeed;
		public float vSpeed;
		public Rectangle BoundingBox;

		public void Initialize(Texture2D texture, Vector2 position) {
			Texture = texture;
			Position = position;
			Health = 100;
			Damage = 10;
			Value = 500;
			hSpeed = -2f;
			vSpeed = 0f;
			BoundingBox = new Rectangle ((int)Position.X, (int)Position.Y, Width, Height);
		}

		public void Update(GameTime gt) {
			Position.X += hSpeed;
			Position.Y += vSpeed;
			BoundingBox.X = (int)Position.X;
			BoundingBox.Y = (int)Position.Y;
			if (Health <= 0) Active = false;
		}

		public void Draw(SpriteBatch sb) {
			if (!Active) return;
			sb.Draw (Texture, Position, Color.White);
		}
	}
}

