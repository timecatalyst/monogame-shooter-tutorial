using System;

namespace MyGame {
	public static class Constants {
		public const bool GAMEPAD_ENABLED = true;

		public const int PLAYER_HEALTH = 100;
		public const int PLAYER_COLLIDE_DAMAGE = 10;
		public const int PLAYER_LASER_DAMAGE = 10;
		public const float PLAYER_LASER_SPEED = 6f;

		public const int BOSS_HEALTH = 100;
		public const int BOSS_COLLIDE_DAMAGE = 10;
		public const int BOSS_POINT_VALUE = 500;

		public const int ENEMY_HEALTH = 10;
		public const int ENEMY_COLLIDE_DAMAGE = 10;
		public const float ENEMY_SPEED = -6f;
		public const int ENEMY_POINT_VALUE = 100;
		public const int ENEMY_COUNT = 30;
	}
}

