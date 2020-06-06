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
    public class Background
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 dimension;
        public float ScrollSpeed { get; set; }
        public Background(string path, Vector2 position, Vector2 dimension, float speed)
        {
            //FEATURE: Make a Parallax Scrolling Background
            texture = Globals.Content.Load<Texture2D>(path);
            this.position = position;
            this.dimension = dimension;
            ScrollSpeed = speed;
        }

        public void Update()
        {
            Move();
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(
                texture,
                new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y),
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                new SpriteEffects(),
                0
            );
        }

        private void Move()
        {
            if (position.X + dimension.X > 0)
            {
                position.X -= ScrollSpeed * Globals.gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                //TODO: Why do i need an offset of 10?
                position.X = Globals.WindowWidth - 10;
            }
        }
    }
}