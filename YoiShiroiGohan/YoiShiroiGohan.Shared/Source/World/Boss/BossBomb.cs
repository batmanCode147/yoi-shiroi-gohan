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
    public class BossBomb : Projectile, ICollidable
    {
        private SoundItem bomb_sound;

        private Vector2 prevPos;
        private float Amplitute { get; set; }
        private int Health { get; set; } = 20;

        public BossBomb(string path, Vector2 position, Vector2 dimension, Vector2 velocity, int speed) : base(path, position, dimension, velocity, speed)
        {
            bomb_sound = new SoundItem("Audio\\boss_bullet", 0.1f, false);
            bomb_sound.PlaySound();
            Amplitute = Globals.random.Next(5, 13);
            Speed = Globals.random.Next(3, 7);
        }

        public override void Update()
        {
            float dt = (float)Globals.gameTime.TotalGameTime.TotalSeconds;
            prevPos = Position;

            prevPos.X += -Speed;
            prevPos.Y += (float)Math.Sin(dt * Speed) * Amplitute;

            if (Vector2.Distance(startPos, Position) > Globals.WindowWidth || Health <= 0)
                IsVisible = false;

            Position = prevPos;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnCollision()
        {
            IsVisible = false;
            Health--;
        }
    }
}