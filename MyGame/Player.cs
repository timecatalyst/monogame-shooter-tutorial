using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Player {
		public Animation PlayerAnimation;
		public Vector2 Position;
		public bool Active;
		public int Health;
		public int Width  { get { return PlayerAnimation.FrameWidth; } }
		public int Height { get { return PlayerAnimation.FrameHeight; } }
		public Rectangle BoundingBox { get { return PlayerAnimation.BoundingBox; } }

		public void Initialize(Animation animation, Vector2 position, int health) {
			PlayerAnimation = animation;
			Position = position;
			Health = health;
			Active = true;
		}

		public void Update(GameTime gameTime) {
			PlayerAnimation.Position = Position;
			PlayerAnimation.Update (gameTime);
		}

		public void Draw(SpriteBatch sb) {
			if (!Active) return;
			PlayerAnimation.Draw (sb);
		}

	}
}

