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
    public class Platform : GameObject, ICollidable
    {
        public int Speed { get; set; }
        public Vector2 startPos { get; private set; }
        new public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y/2); }
            set {; }
        }
        public Platform(string path, Vector2 position, Vector2 dimension) : base(path, position, dimension)
        {
            startPos = position;
            Speed = Globals.PLATFORM_SPEED;
        }

        public override void Update()
        {
            Move();
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void Move()
        {
            if (position.X + dimension.X > 0)
                position.X -= Speed;
            else
                ResetPosition();
        }

        private void ResetPosition()
        {
            position.X = Globals.WindowWidth;
        }

        public void OnCollision()
        { 
            //FEATURE: Animate Cloud when Player is Ontop.
        }
    }
}
