using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class Boss : GameObject {
		public void Initialize(Texture2D texture, Vector2 position, float xSpeed, float ySpeed, bool active) {
			base.Initialize (texture, position, xSpeed, ySpeed, 1f, Constants.BOSS_HEALTH, 
				             Constants.BOSS_POINT_VALUE , Constants.BOSS_COLLIDE_DAMAGE, active);	
		}

		new public void Update(GameTime gt) {
			base.Update (gt);
			if (Health <= 0) Active = false;
		}
	}
}