using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Laser {
		public Vector2 Position;
		public bool Active;
		public int Width;
		public int Height;
		public Rectangle BoundingBox;

		private float moveSpeed;
		private Texture2D texture;
		private int viewportWidth;

		public void Initialize(Texture2D texture, Vector2 position, int viewportWidth) {
			this.texture = texture;
			this.viewportWidth = viewportWidth;
			Position = position;
			moveSpeed = 6f;
			Active = true;
			Width = texture.Width;
			Height = texture.Height;
			BoundingBox = new Rectangle ((int)Position.X, (int)Position.Y, Width, Height);
		}

		public void Update(GameTime gameTime) {
			Position.X += moveSpeed;
			BoundingBox.X = (int)Position.X;
			if (Position.X > viewportWidth) Active = false;
		}

		public void Draw(SpriteBatch sb) {
			if (!Active) return;
			sb.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}

