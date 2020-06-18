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
    class CreditsState : State
    {
        private Button backBtn;
        private GameObject bg_card;
        private TextLabel credits;
        private GameObject menuTitle;

        public CreditsState()
        {
            string creditsText = "OwlBeats: SFX\nFinal Gate Studios: Music\nJalastram: SFX\nJdwasabi: SFX\nSavvyCow: Background\nHelianthus Games: Lavaball Sprite\nTiny World: Font\nKaganawa: Game Design\n               Sprite Assets\n               Programming";

            backBtn = new Button("Back", "Fonts\\PixelFont", "Images\\btn-template", new Vector2((Globals.WindowWidth/2) - (210/2), 625), new Vector2(210, 100));

            menuTitle = new GameObject("Images\\title-template", new Vector2(152, 25), new Vector2(720, 170));

            credits = new TextLabel(creditsText, "Fonts\\PixelFont", new Vector2(220, 205), 0.8f, Color.White);

            bg_card = new GameObject("Images\\option_card", new Vector2(176, 175), new Vector2(671, 435));
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
                Main.CurrenGameState = GameState.Options;
            }
        }

        public override void Draw()
        {
            base.Draw();

            bg_card.Draw();
            credits.Draw();
            backBtn.Draw();
            menuTitle.Draw();
        }
    }
}
