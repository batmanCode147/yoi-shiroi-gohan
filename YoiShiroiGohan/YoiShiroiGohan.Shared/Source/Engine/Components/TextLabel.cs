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
    class TextLabel
    {
        private SpriteFont font;
        private string text;
        private Vector2 position;
        private float scale;
        private Color color;

        public Vector2 StringSize { get; private set; }
        public string Text { get { return text; } set { text = value; } }
        public TextLabel(string text, string path, Vector2 position, float scale, Color color)
        { 
            font = Globals.Content.Load<SpriteFont>(path);

            this.text = text;
            this.position = position;
            this.scale = scale;
            this.color = color;
            StringSize = font.MeasureString(Text) * scale;
        }

        public void Update()
        { 
            
        }

        public void Draw()
        {
            Globals.spriteBatch.DrawString(
                font,
                text,
                position,
                color,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
