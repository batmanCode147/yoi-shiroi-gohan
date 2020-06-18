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
using System.Text;
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
    class WinState : State
    {
        private enum SelectedWinBtn
        {
            PlayAgain,
            Menu
        }

        private SoundItem win_sound;
        private Button playAgainBtn;
        private Button menuBtn;
        private GameObject bg_card;
        private GameObject plane;

        SelectedWinBtn currentSelected;

        public WinState()
        {
            win_sound = new SoundItem("Audio\\win_firework", 1f, true);

            playAgainBtn = new Button("Play Again", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(250, 450), new Vector2(245, 100));
            menuBtn = new Button("Menu", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(550, 450), new Vector2(200, 100));

            bg_card = new GameObject("Images\\win_card", new Vector2(176, 100), new Vector2(671, 294));
            plane = new GameObject("Images\\win_plane", new Vector2(Globals.WindowWidth + 100, 100), new Vector2(900, 200));

            this.buttons = new List<Button>()
            {
                playAgainBtn,
                menuBtn
            };

            currentSelected = (SelectedWinBtn)menuNum;
        }

        public override void Update()
        {
            base.Update();

            GetInput();
            currentSelected = (SelectedWinBtn)menuNum;

            foreach (var button in buttons)
            {
                button.Update();
            }

            buttons[menuNum].Color = new Color(237, 71, 86);

            MovePlane();
        }

        private void MovePlane()
        {
            plane.position.X -= 3f;
            if (plane.position.X+plane.dimension.X <= -10)
            {
                plane.position.X = Globals.WindowWidth + 100;
            }
            plane.Update();
        }

        public override void GetInput()
        {
            base.GetInput();

            if (input.ButtonPressed(Buttons.A) || input.KeyPressed(Keys.Enter))
            {
                menu_sound.PlaySound();

                switch (currentSelected)
                {
                    case SelectedWinBtn.PlayAgain:
                        Main.CurrenGameState = GameState.NewGame;
                        break;
                    case SelectedWinBtn.Menu:
                        Main.NewGame();
                        Main.CurrenGameState = GameState.Menu;
                        break;
                    default:
                        break;
                }

                win_sound.StopSound();
            }
        }

        public override void Draw()
        {
            base.Draw();

            bg_card.Draw();

            foreach (var button in buttons)
            {
                button.Draw();
            }

            plane.Draw();
        }

        public void TurnOnMusic()
        {
            win_sound.PlaySound();
        }
    }
}
