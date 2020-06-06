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
    public class OptionState : State
    {
        private enum SelectedOptionBtn
        {
            Back,
            Save,
            Tutorial,
            Credits,
            Mode,
            Mute
        }

        private List<Button> buttons;
        private List<TextLabel> labels;
        
        private TextLabel muteLabel;
        private TextLabel modeLabel;
        private TextLabel credits;
        private TextLabel tutorial;

        private Button backBtn;
        private Button saveBtn;
        private Button muteBtn;
        private Button modeBtn;
        private Button creditBtn;
        private Button tutorialBtn;

        private GameObject bg_card;

        SelectedOptionBtn currentSelected;

        bool isMuted = false;
        bool hardMode = false;

        public OptionState()
        {
            muteLabel = new TextLabel("Volume", "Fonts\\PixelFont", new Vector2(270, 140), 1f, Color.White);
            modeLabel = new TextLabel("Mode", "Fonts\\PixelFont", new Vector2(270, 200), 1f, Color.White);
            credits = new TextLabel("Credits", "Fonts\\PixelFont", new Vector2(270, 260), 1f, Color.White);
            tutorial = new TextLabel("Tutorial", "Fonts\\PixelFont", new Vector2(270, 320), 1f, Color.White);

            this.labels = new List<TextLabel>()
            {
                muteLabel,
                modeLabel,
                credits,
                tutorial
            };

            backBtn = new Button("Back", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(250, 450), new Vector2(200, 100));
            saveBtn = new Button("Save", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(550, 450), new Vector2(200, 100));

            muteBtn = new Button("Mute", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(570, 140), new Vector2(150, 50), 0.5f);
            modeBtn = new Button("Normal", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(570, 200), new Vector2(150, 50), 0.5f);
            creditBtn = new Button("Credits", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(570, 260), new Vector2(150, 50), 0.5f);
            tutorialBtn = new Button("Tutorial", "Fonts\\PixelFont", "Images\\btn-template", new Vector2(570, 320), new Vector2(150, 50), 0.5f);

            bg_card = new GameObject("Images\\option_card", new Vector2(176, 100), new Vector2(671, 310));

            this.buttons = new List<Button>()
            {
                backBtn,
                saveBtn,
                tutorialBtn,
                creditBtn,
                modeBtn,
                muteBtn
            };

            currentSelected = (SelectedOptionBtn)menuNum;
        }

        public override void Update()
        {
            GetInput();
            currentSelected = (SelectedOptionBtn)menuNum;

            foreach (var label in labels)
            {
                label.Update();
            }

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


            if (input.ButtonPressed(Buttons.DPadDown) || input.KeyPressed(Keys.S))
            {
                if (menuNum > 0)
                {
                    menuNum--;
                    menu_sound.PlaySound();
                }
            }

            if (input.ButtonPressed(Buttons.DPadUp) || input.KeyPressed(Keys.W))
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
                    case SelectedOptionBtn.Back:
                        Console.WriteLine("Back");
                        Main.CurrenGameState = GameState.Menu;
                        break;
                    case SelectedOptionBtn.Save:
                        Console.WriteLine("Save");
                        Main.CurrenGameState = GameState.Menu;
                        break;
                    case SelectedOptionBtn.Mute:
                        if (!isMuted)
                        {
                            muteBtn.Text = "Unmute";
                            SoundItem.Mute();
                            isMuted = true;
                        }
                        else
                        {
                            muteBtn.Text = "Mute";
                            SoundItem.UnMute();
                            isMuted = false;
                        }
                        break;
                    case SelectedOptionBtn.Mode:
                        if (!hardMode)
                        {
                            modeBtn.Text = "Hard";
                            World.boss.MaxHealth = Globals.BOSS_HARDMODE_HEALTH;
                            World.boss.Health = Globals.BOSS_HARDMODE_HEALTH;
                            World.player.MAX_HEALTH = 3;
                            World.player.Health = 3;
                            Globals.HARDMODE = true;
                            hardMode = true;
                        }
                        else
                        {
                            modeBtn.Text = "Normal";
                            World.boss.MaxHealth = Globals.BOSS_HEALTH;
                            World.boss.Health = Globals.BOSS_HEALTH;
                            World.player.MAX_HEALTH = Globals.PLAYER_HEALTH;
                            World.player.Health = Globals.PLAYER_HEALTH;
                            Globals.HARDMODE = false;
                            hardMode = false;
                        }
                        break;
                    case SelectedOptionBtn.Credits:
                        Console.WriteLine("Credits");
                        Main.CurrenGameState = GameState.Credits;
                        break;
                    case SelectedOptionBtn.Tutorial:
                        Console.WriteLine("Tutorial");
                        Main.CurrenGameState = GameState.Tutorial;
                        break;
                    default:
                        break;
                }
            }
        }

        public override void Draw()
        {
            foreach (var b in background)
            {
                b.Draw();
            }

            bg_card.Draw();

            foreach (var label in labels)
            {
                label.Draw();
            }

            foreach (var button in buttons)
            {
                button.Draw();
            }
        }
    }
}
