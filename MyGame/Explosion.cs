using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Explosion {
		public Animation ExplosionAnimation;
		public Vector2 Position;
		public bool Active;
		public int Width { get { return ExplosionAnimation.FrameWidth; } }
		public int Height { get { return ExplosionAnimation.FrameHeight; } }

		float moveSpeed;

		public void Initialize(Animation animation, Vector2 position, float moveSpeed) {
			ExplosionAnimation = animation;
			Position = position;
			Active = true;
			this.moveSpeed = moveSpeed;
		}

		public void Update(GameTime gameTime) {
			Position.X -= moveSpeed;
			ExplosionAnimation.Position = Position;
			ExplosionAnimation.Update (gameTime);
			if (!ExplosionAnimation.Active) Active = false;
		}

		public void Draw(SpriteBatch sb) {
			if (!Active) return;
			ExplosionAnimation.Draw (sb);
		}
	}
}

