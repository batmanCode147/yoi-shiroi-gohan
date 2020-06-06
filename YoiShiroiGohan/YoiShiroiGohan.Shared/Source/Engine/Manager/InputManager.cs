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
using System.Runtime.CompilerServices;
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
    public class InputManager
    {
        private KeyboardState keyState, prevKeyState;
        private GamePadState padState, prevPadState;
        private int playerIndex;

        public KeyboardState KeyboardState { get { return keyState; } set { keyState = value; } }
        public GamePadState GamePadState { get { return padState; } set { padState = value; } }
        public int PlayerIndex { get { return playerIndex; } set { playerIndex = value; } }

        public InputManager(int playerIndex = 0)
        {
            //FEATURE: Multiplayer Support
            this.playerIndex = playerIndex;
        }

        public void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            prevPadState = padState;
            padState = GamePad.GetState((PlayerIndex)playerIndex);
        }

        #region Keyboard
        public bool KeyPressed(Keys key)
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyReleased(Keys key)
        {
            if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyState.IsKeyDown(key))
                return true;
            else
                return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
        #endregion

        #region GamePad
        public bool ButtonPressed(Buttons button)
        {
            if (padState.IsButtonDown(button) && prevPadState.IsButtonUp(button))
                return true;
            return false;
        }

        public bool ButtonPressed(params Buttons[] buttons)
        {
            foreach (Buttons button in buttons)
            {
                if (padState.IsButtonDown(button) && prevPadState.IsButtonUp(button))
                    return true;
            }
            return false;
        }

        public bool ButtonReleased(Buttons button)
        {
            if (padState.IsButtonUp(button) && prevPadState.IsButtonDown(button))
                return true;
            return false;
        }

        public bool ButtonReleased(params Buttons[] buttons)
        {
            foreach (Buttons button in buttons)
            {
                if (padState.IsButtonUp(button) && prevPadState.IsButtonDown(button))
                    return true;
            }
            return false;
        }

        public bool ButtonDown(Buttons button)
        {
            if (padState.IsButtonDown(button))
                return true;
            return false;
        }

        public bool ButtonDown(params Buttons[] buttons)
        {
            foreach (Buttons button in buttons)
            {
                if (padState.IsButtonDown(button))
                    return true;
            }
            return false;
        }
        #endregion
    }
}
