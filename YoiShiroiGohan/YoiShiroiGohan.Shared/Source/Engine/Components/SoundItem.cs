#region Impressum
// Yoi Shiroi Gohan MMP1
// Manuel Obertlik
// Multimedia Technology
// FH Salzburg 1910601028
#endregion

#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace YoiShiroiGohan
{
    public class SoundItem
    {
        public float Volume { get; set; }
        public bool IsLooping { get; set; }
        public SoundEffect Sound { get; private set; }
        public SoundEffectInstance Instance { get; private set; }

        public SoundItem(string path, float volume, bool loop)
        {
            Volume = MathHelper.Lerp(0, volume, Globals.MASTER_VOLUME);
            IsLooping = loop;
            Sound = Globals.Content.Load<SoundEffect>(path);
            SetSoundSettings();
        } 
        private void SetSoundSettings()
        {
            Instance = Sound.CreateInstance();
            Instance.IsLooped = IsLooping;
            Instance.Volume = Volume;
        }

        public void PlaySound()
        {
            if(Instance.State != SoundState.Playing)
                Instance.Play();
        }

        public void StopSound()
        {
            if (Instance.State != SoundState.Stopped)
                Instance.Stop();
        }

        public static void Mute()
        {
            SoundEffect.MasterVolume = 0f;
        }

        public static void UnMute()
        {
            SoundEffect.MasterVolume = Globals.MASTER_VOLUME;
        }
    }
}
