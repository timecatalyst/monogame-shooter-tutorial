using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame {
	public class ParallaxingBackground {
		Texture2D texture;
		Vector2[] positions;
		int speed;
		int bgHeight;
		int bgWidth;

		public void Initialize(Texture2D texture, int screenWidth, int screenHeight, int speed) {
			bgHeight = screenHeight;
			bgWidth = screenWidth;

			this.texture = texture;
			this.speed = speed;

			positions = new Vector2[screenWidth / texture.Width + 1];
			for (int i = 0; i < positions.Length; i++) {
				positions [i] = new Vector2 (i * texture.Width, 0);
			}
		}

		public void Update(GameTime gameTime) {
			for (int i = 0; i < positions.Length; i++) {
				positions [i].X += speed;

				if (speed <= 0) {
					if (positions [i].X <= -texture.Width) {
						positions [i].X = texture.Width * (positions.Length - 1);
					}
				} else {
					if (positions[i].X >= texture.Width * (positions.Length - 1)) {
						positions[i].X = -texture.Width;
					}
				}
			}
		}

		public void Draw(SpriteBatch sb) {
			for (int i = 0; i < positions.Length; i++) {
				Rectangle rectBg = new Rectangle ((int)positions [i].X, (int)positions [i].Y, bgWidth, bgHeight);
				sb.Draw (texture, rectBg, Color.White);
			}
		}
	}
}

