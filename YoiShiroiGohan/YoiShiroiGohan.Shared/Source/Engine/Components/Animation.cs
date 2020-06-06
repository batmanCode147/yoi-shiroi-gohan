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
    public class Animation
    {
        private Texture2D texture;
        private int frameWidth;
        private int frameHeight;
        private float duration;
        private int maxFrames;
        private bool isLooping;
        private float scale;

        public Texture2D Texture { get { return texture; } }
        public int FrameWidth { get { return frameWidth; } }
        public int FrameHeight { get { return frameHeight; } }
        public float Duration { get { return duration; } }
        public int MaxFrames { get { return maxFrames; } }
        public bool IsLoopin { get { return isLooping; } }
        public float Scale { get { return scale; } }
        public bool flip { get; set; } = false;

        public Animation(string path, Vector2 dimension, float duration, bool isLooping, float scale = 1f)
        {
            this.texture = Globals.Content.Load<Texture2D>(path);
            this.frameWidth = (int)dimension.X;
            this.frameHeight = (int)dimension.Y;
            this.duration = duration;
            this.isLooping = isLooping;
            this.maxFrames = texture.Width / frameWidth;
            this.scale = scale;
        }
    }
}
