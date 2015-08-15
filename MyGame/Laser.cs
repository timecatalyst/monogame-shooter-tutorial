using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Laser : GameObject {
		private int viewportWidth;

		public void Initialize(Texture2D texture, Vector2 position, int viewportWidth) {
			this.viewportWidth = viewportWidth;
			base.Initialize (texture, position, Constants.PLAYER_LASER_SPEED, 0f, 1f, 0, 0, 
				             Constants.PLAYER_LASER_DAMAGE, true);
		}

		new public void Update(GameTime gt) {
			base.Update (gt);
			if (Position.X > viewportWidth) Active = false;
		}
	}
}