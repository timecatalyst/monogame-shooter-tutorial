using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Animation {
		public int FrameWidth, FrameHeight;
		public bool Active, Looping;
		public Vector2 Position;
		public Rectangle BoundingBox;

		Texture2D spriteStrip;
		Rectangle frameBox;
		Color color;
		float scale;
		int elapsedTime, frameTime, frameCount, currentFrame;

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

			BoundingBox = new Rectangle ((int)Position.X, (int)Position.Y, (int)(FrameWidth * this.scale), (int)(FrameHeight * this.scale));
			frameBox = new Rectangle (0, 0, FrameWidth, FrameHeight);
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

			frameBox.X = currentFrame * FrameWidth;
			BoundingBox.X = (int)Position.X;
			BoundingBox.Y = (int)Position.Y;
		}

		public void Draw(SpriteBatch spriteBatch) {
			if (!Active) return;
			spriteBatch.Draw (spriteStrip, BoundingBox, frameBox, color);
		}
	}
}

