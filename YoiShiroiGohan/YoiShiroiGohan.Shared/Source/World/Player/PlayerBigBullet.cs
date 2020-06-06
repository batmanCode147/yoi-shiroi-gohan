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
using System.Net.NetworkInformation;
using System.Reflection;
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
    class PlayerBigBullet : Projectile
    {
        private SoundItem hit_sound;

        public PlayerBigBullet(string path, Vector2 position, Vector2 dimension, Vector2 velocity, int speed) : base(path, position, dimension, velocity, speed)
        {
            Damage = Globals.PLAYER_BIG_BULLET_DAMAGE;
            hit_sound = new SoundItem("Audio\\player_shoot", 0.02f, false);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnCollision()
        {
            hit_sound.PlaySound();
            IsVisible = false;
        }
    }
}
