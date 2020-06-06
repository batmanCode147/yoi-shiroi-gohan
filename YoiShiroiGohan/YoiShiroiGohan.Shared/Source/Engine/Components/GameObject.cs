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
    public class GameObject
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 dimension;

        public Vector2 Origin
        {
            get { return Vector2.Zero; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y); }
            set {; }
        }
        public Vector2 StartPos { get; set; }

        public GameObject(string path, Vector2 position, Vector2 dimension)
        {
            this.position = position;
            this.dimension = dimension;
            StartPos = position;
            texture = Globals.Content.Load<Texture2D>(path);
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            Globals.spriteBatch.Draw(
                texture,
                new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y),
                null,
                Color.White,
                0.0f,
                Origin,
                new SpriteEffects(),
                0
            );
        }
    }
}
