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
    public class TutorialState : State
    {
        private Button backBtn;
        private GameObject bg_card;

        public TutorialState()
        {
            backBtn = new Button("Back", "Fonts\\PixelFont", "Images\\btn-template", new Vector2((Globals.WindowWidth / 2) - (210 / 2), 450), new Vector2(210, 100));
            bg_card = new GameObject("Images\\tutorial", new Vector2(176, 100), new Vector2(671, 310));
        }

        public override void Update()
        {
            base.Update();
            GetInput();

            backBtn.Update();
            backBtn.Color = new Color(237, 71, 86);
        }

        public override void GetInput()
        {
            if (input.ButtonPressed(Buttons.A) || input.KeyPressed(Keys.Enter))
            {
                menu_sound.PlaySound();

                Console.WriteLine("Options");
                Main.CurrenGameState = GameState.Options;
            }
        }

        public override void Draw()
        {
            base.Draw();
            bg_card.Draw();
            backBtn.Draw();
        }
    }
}