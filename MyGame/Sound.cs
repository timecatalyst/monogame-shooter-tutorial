using System;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace MyGame {
	public static class Sound {
		public static SoundEffect SFX_Laser;
		public static SoundEffect SFX_Explosion;

		public static Song MenuMusic;
		public static Song GameMusic;

		public static void load(ContentManager content) {
			SFX_Laser = content.Load<SoundEffect> ("Sound\\laserFire");
			SFX_Explosion = content.Load<SoundEffect> ("Sound\\explosion");
		
			MenuMusic = content.Load<Song> ("Sound\\menuMusic");
			GameMusic = content.Load<Song> ("Sound\\gameMusic");

			MediaPlayer.IsRepeating = true;
		}
	}
}