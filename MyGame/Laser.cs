using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Laser {
		public Vector2 Position;
		public bool Active;
		public int Width;
		public int Height;

		private float moveSpeed;
		private Texture2D texture;

		public void Initialize(Texture2D texture, Vector2 position) {
			this.texture = texture;
			Position = position;
			moveSpeed = 6f;
			Active = true;
			Width = texture.Width;
			Height = texture.Height;
		}

		public void Update(GameTime gameTime, int viewportWidth) {
			Position.X += moveSpeed;
			if (Position.X > viewportWidth) {
				Active = false;
			}
		}

		public void Draw(SpriteBatch sb) {
			if (!Active) return;
			sb.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}

