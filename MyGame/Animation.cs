using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Animation {
		Texture2D spriteStrip;
		float scale;
		int elapsedTime;
		int frameTime;
		int frameCount;
		int currentFrame;
		Color color;
		Rectangle sourceRect = new Rectangle();
		Rectangle destinationRect = new Rectangle();

		public int FrameWidth;
		public int FrameHeight;
		public bool Active;
		public bool Looping;
		public Vector2 Position;

		public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, 
			                   int frameCount, int frameTime, Color color, float scale, bool looping) {
			this.color = color;
			this.FrameWidth = frameWidth;
			this.FrameHeight = frameHeight;
			this.frameCount = frameCount;
			this.frameTime = frameTime;
			this.scale = scale;

			Looping = looping;
			Position = position;
			spriteStrip = texture;

			elapsedTime = 0;
			currentFrame = 0;
			Active = true;
		}

		public void Update(GameTime gameTime) {
			if (!Active) return;

			elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
			if (elapsedTime > frameTime) {
				currentFrame++;
				if (currentFrame >= frameCount) {
					currentFrame = 0;
					if (!Looping) Active = false;
				}

				elapsedTime = 0;
			}	

			sourceRect = new Rectangle (currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
			destinationRect = new Rectangle ((int)Position.X, (int)Position.Y,
				                             (int)(FrameWidth * scale), (int)(FrameHeight * scale));
		}

		public void Draw(SpriteBatch spriteBatch) {
			if (!Active) return;
			spriteBatch.Draw (spriteStrip, destinationRect, sourceRect, color);
		}
	}
}

