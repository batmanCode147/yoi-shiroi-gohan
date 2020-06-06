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
    class GameOverState : State
    {
        private enum SelectedGameOverBtn
        {
            PlayAgain,
            Menu
        }

        private AnimationManager animationManager;
        private List<Button> buttons;
        private Button playAgainBtn;
        private Button menuBtn;
        private GameObject bg_card;
        public static GameObject player_progress;
        private Animation progressAnim;

        SelectedGameOverBtn currentSelected;

        public GameOverState()
        {
            playAgainBtn = new Button("Play Again", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(250, 450), new Vector2(245, 100));
            menuBtn = new Button("Menu", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(550, 450), new Vector2(200, 100));

            bg_card = new GameObject("Images\\gameover_card", new Vector2(176, 100), new Vector2(671, 294));
            player_progress = new GameObject("Images\\plyer_progress", new Vector2(720, 298), new Vector2(55, 55));
            animationManager = new AnimationManager();
            progressAnim = new Animation("Images\\player_progress_sheet", player_progress.dimension, 80f, true);
            animationManager.PlayAnimation(progressAnim, Anim.Idle);

            this.buttons = new List<Button>()
            {
                playAgainBtn,
                menuBtn
            };

            currentSelected = (SelectedGameOverBtn)menuNum;
        }

        public override void Update()
        {
            GetInput();
            currentSelected = (SelectedGameOverBtn)menuNum;

            foreach (var button in buttons)
            {
                button.Update();
            }

            buttons[menuNum].Color = new Color(237, 71, 86);

            foreach (var b in background)
            {
                b.Update();
            }
        }

        public override void GetInput()
        {
            InputManager input = Globals.inputManager;

            if (input.ButtonPressed(Buttons.DPadLeft) || input.KeyPressed(Keys.A))
            {
                if (menuNum > 0)
                {
                    menuNum--;
                    menu_sound.PlaySound();
                }
            }

            if (input.ButtonPressed(Buttons.DPadRight) || input.KeyPressed(Keys.D))
            {
                if (menuNum < buttons.Count - 1)
                {
                    menuNum++;
                    menu_sound.PlaySound();
                }
            }

            if (input.ButtonPressed(Buttons.A) || input.KeyPressed(Keys.Enter))
            {
                menu_sound.PlaySound();

                switch (currentSelected)
                {
                    case SelectedGameOverBtn.PlayAgain:
                        Console.WriteLine("New Game");
                        player_progress.position = player_progress.StartPos;
                        Main.CurrenGameState = GameState.NewGame;
                        break;
                    case SelectedGameOverBtn.Menu:
                        Console.WriteLine("Menu");
                        player_progress.position = player_progress.StartPos;
                        Main.NewGame();
                        Main.CurrenGameState = GameState.Menu;
                        break;
                    default:
                        break;
                }
            }
        }

        public override void Draw()
        {
            Console.WriteLine(player_progress.position);
            foreach (var b in background)
            {
                b.Draw();
            }

            bg_card.Draw();
            animationManager.Draw(player_progress.position);

            foreach (var button in buttons)
            {
                button.Draw();
            }
        }
    }
}
