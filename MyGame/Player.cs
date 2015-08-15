using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Player : GameObject {
		public void Initialize(Texture2D texture, Vector2 position) {
			_initAnimationParameters(115, 69, 8, 30, true);
			base.Initialize (texture, position, 0f, 0f, 1f, Constants.PLAYER_HEALTH, 0, Constants.PLAYER_COLLIDE_DAMAGE, true);
		}	

		new public void Update(GameTime gt) {
			base.Update (gt);
			if (Health <= 0) Active = false;
		}
	}
}