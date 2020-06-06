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
    public class Button : GameObject
    {
        private SpriteFont font;
        private Color textColor;
        private Color backgroundColor;
        private float scale;

        public Color TextColor { set { textColor = value; } }
        public Color Color { get { return backgroundColor; } set { backgroundColor = value; } }
        public bool IsSelected { get; set; }
        public string Text { get; set; }
        public Vector2 StringSize { get; private set; }
        public Button(string text, string fontPath, string path, Vector2 position, Vector2 dimension, float scale = 1f) : base(path, position, dimension)
        {
            Text = text;
            this.font = Globals.Content.Load<SpriteFont>(fontPath);
            this.textColor = Color.White;
            this.backgroundColor = Color.White;
            this.IsSelected = false;
            this.scale = scale;
        }

        public override void Update()
        {
            Color = Color.White;
        }

        public override void Draw()
        {
            Globals.spriteBatch.Draw(
                texture,
                new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y),
                null,
                backgroundColor,
                0.0f,
                Origin,
                SpriteEffects.None,
                0f
            );

            StringSize = font.MeasureString(Text) * scale / 2;

            Globals.spriteBatch.DrawString(
                font,
                Text,
                (position + dimension/2) - StringSize,
                textColor,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}