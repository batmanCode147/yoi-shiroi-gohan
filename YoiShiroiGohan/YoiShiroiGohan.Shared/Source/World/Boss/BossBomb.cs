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
        //private Hitbox hitbox;
        public new Rectangle Bounds
        {
            get
            {
                int hitboxOffset = 10;
                int x = (int)Position.X + hitboxOffset;
                int y = (int)Position.Y + hitboxOffset;
                int width = (int)Dimension.X - hitboxOffset * 2;
                int height = (int)Dimension.Y - hitboxOffset * 2;
                return new Rectangle(x, y, width, height);
            }
            set {; }
        }

        public BossBomb(string path, Vector2 position, Vector2 dimension, Vector2 velocity, int speed) : base(path, position, dimension, velocity, speed)
        {
            bomb_sound = new SoundItem("Audio\\boss_bullet", 0.1f, false);
            bomb_sound.PlaySound();
            Amplitute = Globals.random.Next(5, 13);
            Speed = Globals.random.Next(3, 7);

            //hitbox = new Hitbox("Images\\hitbox", Bounds);
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
            //hitbox.position = new Vector2(Bounds.X, Bounds.Y);
        }

        public override void Draw()
        {
            //hitbox.Draw();
            base.Draw();
        }

        public override void OnCollision()
        {
            IsVisible = false;
            Health--;
        }
    }
}