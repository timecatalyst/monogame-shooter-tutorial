using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Enemy : GameObject {
		public void Initialize(Texture2D texture, Vector2 position) {
			// Frame width, frame height, frame count, time between frames(milliseconds), looping
			_initAnimationParameters(47, 61, 8, 30, true);
			base.Initialize (texture, position, Constants.ENEMY_SPEED, 0f, 1f, Constants.ENEMY_HEALTH, 
				             Constants.ENEMY_POINT_VALUE, Constants.ENEMY_COLLIDE_DAMAGE, true);
		}

		new public void Update(GameTime gt) {
			base.Update (gt);
			if (Position.X < -Width || Health <= 0) Active = false;
		}
	}
}