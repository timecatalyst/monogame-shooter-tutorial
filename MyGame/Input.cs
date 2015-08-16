using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame {
	public static class Input {
		private static KeyboardState kbs;
		private static GamePadState gps;

		public static void GetInputState() {
			kbs = Keyboard.GetState ();
			if (Constants.GAMEPAD_ENABLED)
				gps = GamePad.GetState (PlayerIndex.One);
		}

		public static bool Quit()  { return _check (Keys.Escape, Buttons.Back);      }
		public static bool Up()    { return _check (Keys.Up,     Buttons.DPadUp);    }
		public static bool Down()  { return _check (Keys.Down,   Buttons.DPadDown);  }
		public static bool Left()  { return _check (Keys.Left,   Buttons.DPadLeft);  }
		public static bool Right() { return _check (Keys.Right,  Buttons.DPadRight); }
		public static bool Shoot() { return _check (Keys.Space,  Buttons.A);         }
		public static bool Start() { return _check (Keys.Enter,  Buttons.Start);     }

		public static float LeftAnalogX() { 
			return Constants.GAMEPAD_ENABLED ? gps.ThumbSticks.Left.X : 0f; 
		}

		public static float LeftAnalogY() { 
			return Constants.GAMEPAD_ENABLED ? gps.ThumbSticks.Left.Y : 0f; 
		}

		private static bool _check(Keys key, Buttons button) {
			if (kbs.IsKeyDown (key))
				return true;

			if (Constants.GAMEPAD_ENABLED && gps.IsButtonDown (button))
				return true;

			return false;

		}
	}
}

