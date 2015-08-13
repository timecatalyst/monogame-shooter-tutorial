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
		float Speed;

		public void Initialize(Texture2D texture, Vector2 position, float speed) {
			Texture = texture;
			Position = position;
			Speed = speed;
			Active = true;
			Health = 100;
			Damage = 10;
			Value = 500;
		}

		public void Update(GameTime gt) {
			Position.X -= Speed;
		}

		public void Draw(SpriteBatch sb) {
			sb.Draw (Texture, Position, Color.White);
		}
	}
}

