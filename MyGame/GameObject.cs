using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	abstract public class GameObject {
		protected Texture2D texture;

		public bool Active;
		public Vector2 Position;
		public Rectangle BoundingBox;
		public Color Tint;
		public int Width { get { return BoundingBox.Width; } }
		public int Height { get { return BoundingBox.Height; } }

		public int Health, PointValue, DamageDealt;
		public float xSpeed, ySpeed, Scale;

		protected bool animated, looping;
		protected int frameWidth, frameHeight, elapsedTime, frameTime, frameCount, currentFrame;
		protected Rectangle frameBox;

		protected void _initAnimationParameters(int fw, int fh, int fc, int ft, bool looping) {
			this.animated = true;
			this.frameWidth = fw;
			this.frameHeight = fh;
			this.frameCount = fc;
			this.frameTime = ft;
			this.elapsedTime = 0;
			this.currentFrame = 0;
			this.looping = looping;
		}

		public void Initialize(Texture2D texture, Vector2 position, float xSpeed, float ySpeed, float scale, 
			                   int health, int pointValue, int damageDealt, bool active) {
			this.Active = active;
			this.texture = texture;
			this.Position = position;
			this.xSpeed = xSpeed;
			this.ySpeed = ySpeed;
			this.Scale = scale;
			this.Health = health;
			this.PointValue = pointValue;
			this.DamageDealt = damageDealt;
			this.Tint = Color.White;

			if (animated) {
				BoundingBox = new Rectangle ((int)Position.X, (int)Position.Y, (int)(frameWidth * Scale), (int)(frameHeight * Scale));
				frameBox = new Rectangle (0, 0, frameWidth, frameHeight);
			} else {
				BoundingBox = new Rectangle ((int)Position.X, (int)Position.Y, (int)(texture.Width * Scale), (int)(texture.Height * Scale));
			}
		}

		public void Update(GameTime gt) {
			if (!Active) return;

			Position.X += xSpeed;
			Position.Y += ySpeed;
			BoundingBox.X = (int)Position.X;
			BoundingBox.Y = (int)Position.Y;

			if (animated) _updateAnimation (gt);
		}

		protected void _updateAnimation(GameTime gt) {
			elapsedTime += (int)gt.ElapsedGameTime.TotalMilliseconds;
			if (elapsedTime > frameTime) {
				currentFrame++;
				if (currentFrame >= frameCount) {
					currentFrame = 0;
					if (!looping) Active = false;
				}
				elapsedTime = 0;
			}	

			frameBox.X = currentFrame * frameWidth;
		}

		public void Draw(SpriteBatch sb) {
			if (!Active) return;

			if (animated) {
				sb.Draw (texture, BoundingBox, frameBox, Tint);
			} else {
				sb.Draw (texture, Position, Tint);
			}
		}
	}
}

