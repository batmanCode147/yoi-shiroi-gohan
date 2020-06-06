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
    public class Projectile : ICollidable
    {
        protected AnimationManager animationManager;
        protected Animation animation;

        public Vector2 startPos;

        public int Speed { get; set; }
        public bool IsVisible { get; set; }
        public int Damage { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Dimension { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Dimension.X, (int)Dimension.Y); }
            set {; }
        }

        public Projectile(string path, Vector2 position, Vector2 dimension, Vector2 velocity, int speed)
        {
            animationManager = new AnimationManager();
            animation = new Animation(path, dimension, 50f, true);
            animationManager.PlayAnimation(animation, Anim.Idle);

            Position = position;
            Dimension = dimension;
            IsVisible = true;
            Velocity = velocity;
            Speed = speed;
            startPos = position;
        }

        public virtual void Update()
        {
            Position += Velocity * Speed;

            if (Vector2.Distance(startPos, Position) > Globals.WindowWidth)
                IsVisible = false;
        }

        public virtual void Draw()
        {
            animationManager.Draw(Position);
        }

        public virtual void OnCollision()
        {
            Console.WriteLine("Hit");
        }
    }
}
