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
    public class MenuState : State
    {
        private enum SelectedMenuBtn
        {
            Start,
            Options,
            Quit
        }

        private TextLabel copyright;
        private Button startBtn;
        private Button optionBtn;
        private Button quitBtn;
        private GameObject menuTitle;
        private GameObject easterEgg;

        SelectedMenuBtn currentSelected;
        private int estEgg;

        public MenuState()
        {
            bg_music.PlaySound();

            easterEgg = new GameObject("Images\\easteregg", new Vector2(Globals.WindowWidth+300, Globals.WindowHeight/2-50), new Vector2(900, 100));

            startBtn = new Button("Start", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(162, 400), new Vector2(200, 100));
            optionBtn = new Button("Options", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(412, 400), new Vector2(200, 100));
            quitBtn = new Button("Quit", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(662, 400), new Vector2(200, 100));
            menuTitle = new GameObject("Images\\title-template", new Vector2(162, 100), new Vector2(700, 200));
            copyright = new TextLabel("      Yoi Shiroi Gohan (c)\nMulti Media Technology MMP1\n       FH-Salzburg 2019", "Fonts\\PixelFont", new Vector2(Globals.WindowWidth / 2 - 140, 630), 0.5f, new Color(63, 63, 116));

            this.buttons = new List<Button>()
            {
                startBtn,
                optionBtn,
                quitBtn
            };

            currentSelected = (SelectedMenuBtn)menuNum;
        }

        public override void Update()
        {
            base.Update();

            GetInput();
            currentSelected = (SelectedMenuBtn)menuNum;

            foreach (var button in buttons)
            {
                button.Update();
            }

            buttons[menuNum].Color = new Color(237, 71, 86);

            easterEgg.Update();
            if (estEgg >= 3)
            {
                MoveEgg();
            }
        }

        public override void GetInput()
        {
            base.GetInput();

            if (input.ButtonPressed(Buttons.A) || input.KeyPressed(Keys.Enter))
            {
                menu_sound.PlaySound();

                switch (currentSelected)
                {
                    case SelectedMenuBtn.Start:
                        Console.WriteLine("Game Starts");
                        Main.CurrenGameState = GameState.Playing;
                        break;
                    case SelectedMenuBtn.Options:
                        Console.WriteLine("Options");
                        Main.CurrenGameState = GameState.Options;
                        break;
                    case SelectedMenuBtn.Quit:
                        Console.WriteLine("Quit");
                        Main.CurrenGameState = GameState.Quit;
                        break;
                    default:
                        break;
                }
            }

            if (input.ButtonPressed(Buttons.RightShoulder))
            {
                estEgg++;
            }
        }

        public override void Draw()
        {
            base.Draw();

            menuTitle.Draw();
            copyright.Draw();

            foreach (var button in buttons)
            {
                button.Draw();
            }

            easterEgg.Draw();
        }

        #region Egg
        private void MoveEgg()
        {
            if (easterEgg.position.X + easterEgg.dimension.X < -100)
                easterEgg.position.X = Globals.WindowWidth + 300;
            else
                easterEgg.position.X -= 2f;
        }
        #endregion
    }
}
