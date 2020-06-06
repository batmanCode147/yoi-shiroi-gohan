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
    public class Ground : GameObject
    {
        new public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y-10); }
            set {; }
        }
        public Ground(string path, Vector2 position, Vector2 dimension) : base(path, position, dimension)
        {
            //FEATURE: Animate the Ground
        }
    }
}
