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
    public enum Anim
    {
        Idle,
        Jump,
        Dead,
        Chill,
        Hit
    }
    public class AnimationManager
    {
        private Animation animation;
        private Rectangle sourceRect;
        private int frameIndex;
        private float timer;
        private Anim anim;

        public Color Color { get; set; } = Color.White;

        public Animation Animation { get { return animation; } }
        public int FrameIndex { get; set; }
        public Vector2 Origin
        {
            get { return Vector2.Zero; }
        }

        public AnimationManager()
        {
            frameIndex = 0;
        }

        public void PlayAnimation(Animation animation, Anim anim)
        {
            if (this.animation == animation && this.anim == anim)
                return;

            this.animation = animation;
            this.anim = anim;
            frameIndex = 0;
            timer = 0;
        }

        public void Draw(Vector2 position)
        {
            if (animation == null)
                throw new NotSupportedException("wie stellst da des vor du lappen, es is koa fucking animation do?!");

            timer += (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= animation.Duration)
            {
                if (frameIndex >= animation.MaxFrames - 1)
                {
                    if (animation.IsLoopin)
                        frameIndex = 0;
                }
                else
                {
                    frameIndex++;
                }
                timer = 0f;
            }

            if (anim != Anim.Hit)
            {
                sourceRect = new Rectangle((int)frameIndex * Animation.FrameWidth, (int)anim * Animation.FrameHeight, Animation.FrameWidth, Animation.FrameHeight);
                Color = Color.White;
            }
            else
            {
                sourceRect = new Rectangle(0, 0, 0, 0);
                Color = Color.BlueViolet;
            }

            SpriteEffects flip = (Animation.flip) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Globals.spriteBatch.Draw(
                Animation.Texture,
                position,
                sourceRect,
                Color,
                0f,
                Origin,
                Animation.Scale,
                flip,
                0f
            );
        }
    }
}
