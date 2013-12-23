using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace EarthSpace.Audio
{
    /// <summary>
    /// Manages the game's sound effects.
    /// </summary>
    public static class SoundManager
    {
        private static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        private static float volume;

        /// <summary>
        /// The volume of the game's sound.
        /// </summary>
        public static float Volume
        {
            get { return volume; }
            set
            {
                volume = value;

                if (volume < 0) volume = 0;
                if (volume > 1) volume = 1;
            }
        }

        /// <summary>
        /// Adds a sound effect to the manager.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sound"></param>
        public static void AddSound(string key, SoundEffect sound)
        {
            sounds.Add(key, sound);
        }

        /// <summary>
        /// Plays a sound effect.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="volume"></param>
        /// <param name="pitch"></param>
        /// <param name="pan"></param>
        public static void PlaySound(string key, float volume, float pitch, float pan)
        {
            sounds[key].Play(Volume * volume, pitch, pan);
        }

    }
}