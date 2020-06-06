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
    public class Globals
    {
        // Window Size
        public const int WindowWidth = 1024;
        public const int WindowHeight = 768;

        public static ContentManager Content;
        public static SpriteBatch spriteBatch;
        public static GameTime gameTime;
        public static InputManager inputManager = new InputManager();

        //TODO: Ask about Random not being fucking random...
        public static Random random = new Random();

        #region PLAYER SETTINGS
        public static int PLAYER_HEALTH = 6;
        public const int PLAYER_SHOOTMODE_TIME = 10;
        public const int PLAYER_BIG_BULLET_DAMAGE = 5;
        public const int PLAYER_BULLET_DAMAGE = 1;
        //Physics
        public const float MOVE_ACCELERATION = 15000f;
        public const float MAX_MOVE_SPEED = 5000f;
        public const float GROUND_DRAG = 0.75f;
        public const float AIR_DRAG = 0.8f;
        // ALternative Jump Settings
        //const float MAX_JUMP_TIME = 0.4f;//0.4f;
        //const float JUMP_VELOCITY = -3500;//-7000f;
        //const float GRAVITY_ACCELERATION = 6000f;//6000f;
        //const float MAX_FALL_SPEED = 4000f;//6000f;
        //const float JUMP_POWER = 0.35f;//0.16f;
        public const float MAX_JUMP_TIME = 0.3f;
        public const float JUMP_VELOCITY = -4500f;
        public const float GRAVITY_ACCELERATION = 7000f;
        public const float MAX_FALL_SPEED = 6000f;
        public const float JUMP_POWER = 0.25f;
        public const float MOVE_STICK_SCALE = 1.0f;
        public const Buttons JUMP_BUTTON = Buttons.A;
        public const Buttons SHOOT_BUTTON = Buttons.LeftTrigger;
        #endregion

        #region BOSS SETTIGS
        public static bool HARDMODE = false;
        public static int BOSS_HEALTH = 500;
        public static int BOSS_HARDMODE_HEALTH = 1000;
        public const int BOSS_DEATH_DELAY = 5;
        #endregion

        #region GAME SETTINGS
        public static float MASTER_VOLUME = 1f;
        public static int POWERUP_SPEED = 5;
        public static int PLATFORM_SPEED = 4;
        public static int POWERUP_SPAWNRATE = 15;
        #endregion
    }
}
