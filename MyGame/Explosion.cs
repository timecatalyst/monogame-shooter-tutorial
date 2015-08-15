using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Explosion : GameObject {
		public void Initialize(Texture2D texture, Vector2 Position, float speed, int animationSpeed) {
			_initAnimationParameters(133, 134, 12, animationSpeed, false);
			base.Initialize (texture, Position, speed, 0f, 1f, 0, 0, 0, true);
		}
	}
}

