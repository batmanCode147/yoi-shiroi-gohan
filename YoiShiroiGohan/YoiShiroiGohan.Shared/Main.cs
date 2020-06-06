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
    public class Main : Game
    {
        GraphicsDeviceManager graphics;

        public static World world;

        MenuState menu;
        OptionState option;
        PauseState pause;
        GameOverState gameOver;
        WinState win;
        CreditsState credits;
        TutorialState tutorial;

        private static GameState currentState;
        public static GameState CurrenGameState { set { currentState = value; } }
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Globals.WindowWidth;
            graphics.PreferredBackBufferHeight = Globals.WindowHeight;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            currentState = GameState.Menu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.Content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.inputManager = new InputManager();

            world = new World();
            menu = new MenuState();
            option = new OptionState();
            pause = new PauseState();
            gameOver = new GameOverState();
            win = new WinState();
            credits = new CreditsState();
            tutorial = new TutorialState();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Globals.inputManager.ButtonPressed(Buttons.Back) || Globals.inputManager.KeyPressed(Keys.Escape))
                Exit();

            Globals.gameTime = gameTime;
            Globals.inputManager.Update();

            switch (currentState)
            {
                case GameState.Menu:
                    menu.Update();
                    break;
                case GameState.Playing:
                    world.Update();
                    break;
                case GameState.Options:
                    option.Update();
                    break;
                case GameState.Pause:
                    pause.Update();
                    break;
                case GameState.Quit:
                    Exit();
                    break;
                case GameState.GameOver:
                    gameOver.Update();
                    break;
                case GameState.Win:
                    win.TurnOnMusic();
                    win.Update();
                    break;
                case GameState.NewGame:
                    NewGame();
                    currentState = GameState.Playing;
                    break;
                case GameState.Credits:
                    credits.Update();
                    break;
                case GameState.Tutorial:
                    tutorial.Update();
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(95,205,228));

            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            switch (currentState)
            {
                case GameState.Menu:
                    menu.Draw();
                    break;
                case GameState.Playing:
                    world.Draw();
                    break;
                case GameState.Options:
                    option.Draw();
                    break;
                case GameState.Pause:
                    pause.Draw();
                    break;
                case GameState.Quit:
                    break;
                case GameState.GameOver:
                    gameOver.Draw();
                    break;
                case GameState.Win:
                    win.Draw();
                    break;
                case GameState.Credits:
                    credits.Draw();
                    break;
                case GameState.Tutorial:
                    tutorial.Draw();
                    break;
                default:
                    break;
            }

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void NewGame()
        {
            world = new World();
        }
    }

    //#region program class
    //#if WINDOWS || LINUX
    //    public static class Program
    //    {
    //        [STAThread]
    //        static void Main()
    //        {
    //            using (var game = new Main())
    //                game.Run();
    //        }
    //    }
    //#endif
    //#endregion
}
