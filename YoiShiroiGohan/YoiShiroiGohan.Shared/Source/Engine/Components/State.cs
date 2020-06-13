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
        protected InputManager input = Globals.inputManager;
        protected List<Button> buttons;
        protected int analogX;
        protected int analogY;
        protected int prevAnalogX;
        protected int prevAnalogY;
        protected int menuNum = 0;

        #region Update
        public virtual void Update()
        {
            foreach (var b in background)
            {
                b.Update();
            }
        }
        #endregion

        #region Draw
        public virtual void Draw()
        {
            foreach (var b in background)
            {
                b.Draw();
            }
        }
        #endregion

        #region Input
        public virtual void GetInput()
        {
            Vector2 thumb = input.GamePadState.ThumbSticks.Left;
            analogX = (int)Math.Round(thumb.X);
            analogY = (int)Math.Round(thumb.Y);

            if (input.ButtonPressed(Buttons.DPadLeft) || input.KeyPressed(Keys.A) || prevAnalogX < 0 && prevAnalogX != analogX)
            {
                MenuPrev();
            }

            if (input.ButtonPressed(Buttons.DPadRight) || input.KeyPressed(Keys.D) || prevAnalogX > 0 && prevAnalogX != analogX)
            {
                MenuNext();
            }

            if (input.ButtonPressed(Buttons.DPadDown) || input.KeyPressed(Keys.S) || prevAnalogY < 0 && prevAnalogY != analogY)
            {
                MenuPrev();
            }

            if (input.ButtonPressed(Buttons.DPadUp) || input.KeyPressed(Keys.W) || prevAnalogY > 0 && prevAnalogY != analogY)
            {
                MenuNext();
            }

            prevAnalogX = analogX;
            prevAnalogY = analogY;
        }
        #endregion

        #region Helper
        private void MenuPrev()
        {
            if (menuNum > 0)
            {
                menuNum--;
                menu_sound.PlaySound();
            }
        }

        private void MenuNext()
        {
            if (menuNum < buttons.Count - 1)
            {
                menuNum++;
                menu_sound.PlaySound();
            }
        }
        #endregion
    }
}
