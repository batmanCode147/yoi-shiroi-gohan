#region Impressum
// Yoi Shiroi Gohan MMP1
// Manuel Obertlik
// Multimedia Technology
// FH Salzburg 1910601028
#endregion

#region Includes
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
    public enum ShootingMode
    {
        Normal,
        BigGun
    }
    public class Player : ICollidable
    {
        private AnimationManager animationManager;
        private Animation animation;

        private SoundItem hit_sound;
        private SoundItem die_sound;
        private SoundItem jump_sound;

        private Vector2 velocity;
        private float movement;
        private bool wasJumping;
        private float jumpTime;

        public Vector2 Position { get; set; }
        public Vector2 Dimension { get; set; }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        public Rectangle Bounds { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Dimension.X, (int)Dimension.Y); } }
        public bool IsOnGround { get; set; }
        public bool IsJumping { get; set; }

        //Shooting
        public bool IsShooting { get; set; }
        public List<ICollidable> bullets;
        public ShootingMode CurrentShootingMode { get; set; }

        //Timer
        private TimeSpan deathDelay = TimeSpan.Zero;
        private TimeSpan shootModeTimer = TimeSpan.Zero;
        private TimeSpan shootTimer = TimeSpan.Zero;

        //Health
        private List<GameObject> hearts;
        public int Health { get; set; }
        public int MAX_HEALTH = Globals.PLAYER_HEALTH;
        public int MaxHealth { get { return MAX_HEALTH; } }
        public bool IsAlive { get; set; } = true;
        public Anim PlayerState { get; set; }

        public Player(Vector2 position, Vector2 dimension)
        {
            animationManager = new AnimationManager();
            animation = new Animation("Images\\player_idle_jump-Sheet", dimension, 80f, true);

            //TODO: make the PlayAnimation recive Playerstate
            PlayerState = Anim.Idle;
            animationManager.PlayAnimation(animation, Anim.Idle);

            hit_sound = new SoundItem("Audio\\hit_damage", 1f, false);
            die_sound = new SoundItem("Audio\\player_die", 0.5f, false);
            jump_sound = new SoundItem("Audio\\jump", 0.1f, true);

            Position = position;
            Dimension = dimension;
            velocity = Vector2.Zero;

            bullets = new List<ICollidable>();
            CurrentShootingMode = ShootingMode.Normal;

            Health = MAX_HEALTH;
            hearts = new List<GameObject>();

            for (int i = 0; i < Health; i++)
            {
                hearts.Add(new GameObject("Images\\heart", new Vector2(20 + (i * 60), 10), new Vector2(60, 60)));
            }
        }

        public void Update()
        {
            UpdateState();

            if (IsAlive)
                GetInput();

            ApplyPhysics();

            ShootUpdate();

            if (CurrentShootingMode == ShootingMode.BigGun)
                ShootTimer();

            movement = 0.0f;
            IsOnGround = false;
            IsShooting = false;

            if (Health <= 0)
            {
                //TODO: Die Sound not working right
                die_sound.PlaySound();

                deathDelay += Globals.gameTime.ElapsedGameTime;
                PlayerState = Anim.Dead;
                IsAlive = false;

                if (deathDelay > TimeSpan.FromSeconds(5))
                {
                    OnDie();
                }
            }
        }

        public void Draw()
        {
            animationManager.Draw(Position);

            foreach (var b in bullets)
            {
                Projectile bullet = (Projectile)b;
                bullet.Draw();
            }

            for (int i = 0; i < Health; i++)
            {
                hearts[i].Draw();
            }
        }

        #region Animation
        private void UpdateState()
        {
            switch (PlayerState)
            {
                case Anim.Idle:
                    animationManager.PlayAnimation(animation, Anim.Idle);
                    break;
                case Anim.Jump:
                    animationManager.PlayAnimation(animation, Anim.Jump);
                    break;
                case Anim.Dead:
                    animationManager.PlayAnimation(animation, Anim.Dead);
                    break;
                case Anim.Chill:
                    animationManager.PlayAnimation(animation, Anim.Chill);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Input
        private void GetInput()
        {
            //TODO: Make this use one of 2 inputmanagers for multiplayer support
            InputManager input = Globals.inputManager;

            movement = input.GamePadState.ThumbSticks.Left.X * Globals.MOVE_STICK_SCALE;

            if (Math.Abs(movement) < 0.5f)
            {
                movement = 0.0f;
            }

            if (input.ButtonDown(Buttons.DPadLeft) || input.KeyDown(Keys.A))
            {
                movement = -1.0f;
            }
            else if (input.ButtonDown(Buttons.DPadRight) || input.KeyDown(Keys.D))
            {
                movement = 1.0f;
            }

            IsJumping = input.ButtonDown(Globals.JUMP_BUTTON) || input.KeyDown(Keys.Space);

            IsShooting = input.ButtonDown(Globals.SHOOT_BUTTON) || input.KeyDown(Keys.RightAlt);

            if (input.ButtonPressed(Buttons.Start) || input.KeyPressed(Keys.P))
                Main.CurrenGameState = GameState.Pause;
        }
        #endregion

        #region Physics
        private void ApplyPhysics()
        {
            float dt = (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 prevPosition = Position;

            velocity.X += movement * Globals.MOVE_ACCELERATION * dt;
            velocity.Y = MathHelper.Clamp(velocity.Y + Globals.GRAVITY_ACCELERATION * dt, -Globals.MAX_FALL_SPEED, Globals.MAX_FALL_SPEED);
            velocity.Y = DoJump(velocity.Y);

            if (IsOnGround)
                velocity.X *= Globals.GROUND_DRAG;
            else
                velocity.X *= Globals.AIR_DRAG;

            velocity.X = MathHelper.Clamp(velocity.X, -Globals.MAX_MOVE_SPEED, Globals.MAX_MOVE_SPEED);

            Position += velocity * dt;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

            CollisionManager.WorldConstraints();

            if (Position.X == prevPosition.X)
                velocity.X = 0.0f;
            if (Position.Y == prevPosition.Y)
                velocity.Y = 0.0f;
        }

        private float DoJump(float velocityY)
        {
            if (IsJumping)
            {
                if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
                {
                    if (jumpTime == 0.0f)
                        jump_sound.PlaySound();

                    jumpTime += (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (0.0f < jumpTime && jumpTime <= Globals.MAX_JUMP_TIME)
                {
                    velocityY = Globals.JUMP_VELOCITY * (1.0f - (float)Math.Pow(jumpTime / Globals.MAX_JUMP_TIME, Globals.JUMP_POWER));
                }
                else
                {
                    jumpTime = 0.0f;
                }
            }
            else
            {
                jump_sound.StopSound();
                jumpTime = 0.0f;
            }

            if (!IsOnGround)
                PlayerState = Anim.Jump;
            else
                PlayerState = Anim.Idle;

            wasJumping = IsJumping;

            return velocityY;
        }
        #endregion

        #region Shooting
        private void ShootUpdate()
        {
            if (IsShooting)
                Shoot();

            foreach (var b in bullets)
            {
                Projectile bullet = (Projectile)b;
                bullet.Update();
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                Projectile bullet = (Projectile)bullets[i];

                if (!bullet.IsVisible)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        private void Shoot()
        {
            Projectile bullet = null;
            shootTimer += Globals.gameTime.ElapsedGameTime;

            switch (CurrentShootingMode)
            {
                case ShootingMode.Normal:
                    bullet = new PlayerBullet("Images\\player_bullet_sheet",
                        new Vector2(Position.X + Dimension.X / 2, Position.Y),
                        new Vector2(16, 30),
                        new Vector2(5, 0),
                        10
                    );
                    break;
                case ShootingMode.BigGun:
                    bullet = new PlayerBigBullet("Images\\player_big_gun_sheet",
                        new Vector2(Position.X + Dimension.X / 2, Position.Y),
                        new Vector2(50, 30),
                        new Vector2(6, 0),
                        7
                    );
                    break;
                default:
                    break;
            }

            if (shootTimer > TimeSpan.FromMilliseconds(200))
            {
                if (bullets.Count < 1)
                    bullets.Add(bullet);

                shootTimer = TimeSpan.Zero;
            }
        }

        public void ShootTimer()
        {
            shootModeTimer += Globals.gameTime.ElapsedGameTime;

            if (shootModeTimer > TimeSpan.FromSeconds(Globals.PLAYER_SHOOTMODE_TIME))
            {
                CurrentShootingMode = ShootingMode.Normal;
                shootModeTimer = TimeSpan.Zero;
            }
        }
        #endregion

        #region Events
        public void OnCollision()
        {
            Health--;
            hit_sound.PlaySound();
        }

        private void OnDie()
        {
            jump_sound.StopSound();
            int hardmode = Globals.HARDMODE ? World.boss.Health / 2 : World.boss.Health;
            //TODO: make this more beautiful you little shit
            GameOverState.player_progress.position = new Vector2(GameOverState.player_progress.position.X - hardmode, GameOverState.player_progress.position.Y);
            Main.CurrenGameState = GameState.GameOver;
        }
        #endregion
    }
}
