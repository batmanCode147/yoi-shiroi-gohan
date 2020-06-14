﻿#region Impressum
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
    class PauseState : State
    {
        private enum SelectedPauseBtn
        {
            Continue,
            Menu
        }

        private Button continueBtn;
        private Button menuBtn;
        private GameObject title;
        private GameObject bg_card;

        SelectedPauseBtn currentSelected;

        public PauseState()
        {
            int offset = 125;
            continueBtn = new Button("Continue", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(250, 450+offset), new Vector2(210, 100));
            menuBtn = new Button("Menu", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(550, 450+offset), new Vector2(200, 100));

            title = new GameObject("Images\\pause_card", new Vector2(202, 100), new Vector2(621, 179));
            bg_card = new GameObject("Images\\tutorial", new Vector2(201, 270), new Vector2(621, 260));

            this.buttons = new List<Button>()
            {
                continueBtn,
                menuBtn
            };

            currentSelected = (SelectedPauseBtn)menuNum;
        }

        public override void Update()
        {
            base.Update();

            GetInput();
            currentSelected = (SelectedPauseBtn)menuNum;

            foreach (var button in buttons)
            {
                button.Update();
            }

            buttons[menuNum].Color = new Color(237, 71, 86);
        }

        public override void GetInput()
        {
            base.GetInput();

            if (input.ButtonPressed(Buttons.A) || input.KeyPressed(Keys.Enter))
            {
                menu_sound.PlaySound();

                switch (currentSelected)
                {
                    case SelectedPauseBtn.Continue:
                        Console.WriteLine("Continue");
                        Main.CurrenGameState = GameState.Playing;
                        break;
                    case SelectedPauseBtn.Menu:
                        Console.WriteLine("Menu");
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
            bg_card.Draw();

            title.Draw();

            foreach (var button in buttons)
            {
                button.Draw();
            }
        }
    }
}
