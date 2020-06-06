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
    public enum GameState
    {
        Menu,
        Playing,
        Options,
        Pause,
        GameOver,
        Win,
        Quit,
        NewGame,
        Credits,
        Tutorial
    }
    public abstract class State
    {
        protected List<Background> background = new List<Background>()
        {
            new Background("Images\\background_clouds", new Vector2(0, 0), new Vector2(Globals.WindowWidth, Globals.WindowHeight), 0.1f),
            new Background("Images\\background_clouds", new Vector2(Globals.WindowWidth, 0), new Vector2(Globals.WindowWidth, Globals.WindowHeight), 0.1f),
            new Background("Images\\menu_background", new Vector2(0, 0), new Vector2(Globals.WindowWidth, Globals.WindowHeight), 0.2f),
            new Background("Images\\menu_background", new Vector2(Globals.WindowWidth, 0), new Vector2(Globals.WindowWidth, Globals.WindowHeight), 0.2f)
        };
        protected SoundItem menu_sound = new SoundItem("Audio\\menu_select", 0.25f, false);
        protected SoundItem bg_music = new SoundItem("Audio\\bg_music2", 0.05f, true);
        protected int menuNum = 0;

        public abstract void GetInput();
        public abstract void Update();
        public abstract void Draw();
    }
}
