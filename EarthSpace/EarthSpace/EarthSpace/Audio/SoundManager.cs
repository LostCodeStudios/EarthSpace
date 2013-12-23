using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace EarthSpace.Audio
{
    public static class SoundManager
    {
        private static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        private static float volume;

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

        public static void AddSong(string key, SoundEffect sound)
        {
            sounds.Add(key, sound);
        }

        public static void PlaySound(string key, float volume, float pitch, float pan)
        {
            sounds[key].Play(Volume * volume, pitch, pan);
        }

        //public static
    }
}