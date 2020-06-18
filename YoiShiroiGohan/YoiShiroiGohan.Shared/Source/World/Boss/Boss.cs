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
using System.Reflection;
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
    public enum BossAttack
    {
        Hover,
        FlyTrough,
        GoBack,
        Spawn
    }

    public enum BossState
    {
        Level1,
        Level2,
        Level3,
        Dead,
        Hit
    }

    public class Boss : ICollidable
    {
        private AnimationManager animationManager;
        private Animation animation;

        private SoundItem fly_sound;
        private SoundItem power_sound;
        private SoundItem die_sound;
        private SoundItem bullet_sound;
        private SoundItem bomb_sound;

        private Vector2 position;
        private Vector2 dimension;
        private Vector2 velocity;
        private Vector2 prevPos;
        private Vector2 startPos = new Vector2(Globals.WindowWidth - 310, 0);
        private float speed = 3f;
        private float amplitute = 6f;

        public Rectangle Bounds
        {
            get
            {
                int hitboxOffset = 30;
                int x = (int)position.X + hitboxOffset;
                int y = (int)position.Y + hitboxOffset;
                int width = (int)dimension.X - hitboxOffset * 2;
                int height = (int)dimension.Y - hitboxOffset * 2;
                return new Rectangle(x, y, width, height);
            }
            set {; }
        }

        public BossAttack CurrentAttack { get; private set; }
        public BossState CurrentState { get; private set; }

        //Shooting
        public List<ICollidable> bombs;
        public List<ICollidable> bullets;

        //Timer
        private TimeSpan flyTimer = TimeSpan.Zero;
        private TimeSpan bombTimer = TimeSpan.Zero;
        private TimeSpan bulletTimer = TimeSpan.Zero;
        private TimeSpan deathDelay = TimeSpan.Zero;
        private TimeSpan spawnDelay = TimeSpan.Zero;

        public bool IsAlive { get; set; } = true;
        public bool IsBegining { get; private set; } = true;

        private float flyAttackRate = 15;
        private float bombRate = 6;
        private float bulletRate = 1;

        public int MaxHealth { get; set; } = Globals.BOSS_HEALTH;
        public int Health { get; set; }

        //private Hitbox hitbox;

        public Boss(Vector2 position, Vector2 dimension)
        {
            fly_sound = new SoundItem("Audio\\fly_sound", 0.5f, false);
            power_sound = new SoundItem("Audio\\boss_power", 0.5f, false);
            die_sound = new SoundItem("Audio\\boss_die", 1f, false);
            bullet_sound = new SoundItem("Audio\\player_shoot", 0.02f, false);
            bomb_sound = new SoundItem("Audio\\boss_bullet", 0.1f, false);

            animationManager = new AnimationManager();
            animation = new Animation("Images\\boss_sprite_sheet", dimension, 100f, true);
            animationManager.PlayAnimation(animation, Anim.Idle);

            this.position = position;
            this.dimension = dimension;
            this.velocity = Vector2.Zero;
            CurrentAttack = BossAttack.Spawn;
            CurrentState = BossState.Level1;

            bombs = new List<ICollidable>();
            bullets = new List<ICollidable>();

            Health = MaxHealth;

            //hitbox = new Hitbox("Images\\hitbox", Bounds);
        }

        public void Update()
        {
            UpdateBossState();

            if (IsAlive)
            {
                flyTimer += Globals.gameTime.ElapsedGameTime;
                bombTimer += Globals.gameTime.ElapsedGameTime;
                bulletTimer += Globals.gameTime.ElapsedGameTime;

                prevPos = position;

                Shooter(bombs);
                Shooter(bullets);
            }

            UpdateAttackState();

            if (Health <= 0)
            {
                deathDelay += Globals.gameTime.ElapsedGameTime;

                die_sound.PlaySound();
                CurrentState = BossState.Dead;
                bombs.Clear();
                bullets.Clear();
                IsAlive = false;

                if (deathDelay > TimeSpan.FromSeconds(Globals.BOSS_DEATH_DELAY))
                {
                    OnDie();
                }
            }

            prevPos += velocity;
            position = prevPos;
            //hitbox.position = new Vector2(Bounds.X, Bounds.Y);
        }

        public void Draw()
        {
            animationManager.Draw(position);
            //hitbox.Draw();

            foreach (var b in bombs)
            {
                BossBomb bomb = (BossBomb)b;
                bomb.Draw();
            }

            foreach (var b in bullets)
            {
                BossBullet bullet = (BossBullet)b;
                bullet.Draw();
            }

        }

        #region BossStates
        private void UpdateBossState()
        {
            switch (CurrentState)
            {
                case BossState.Hit:
                    animationManager.PlayAnimation(animation, Anim.Hit);
                    break;
                case BossState.Level1:
                    animationManager.PlayAnimation(animation, Anim.Idle);
                    break;
                case BossState.Level2:
                    animationManager.PlayAnimation(animation, Anim.Jump);
                    flyAttackRate = 10;
                    bombRate = 3;
                    bulletRate = 0.5f;
                    break;
                case BossState.Level3:
                    animationManager.PlayAnimation(animation, Anim.Dead);
                    flyAttackRate = 5;
                    bombRate = 2;
                    bulletRate = 0.1f;
                    break;
                case BossState.Dead:
                    animationManager.PlayAnimation(animation, Anim.Chill);
                    break;
                default:
                    break;
            }

            if (Health <= MaxHealth / 3)
            {
                CurrentState = BossState.Level3;
            }
            else if (Health <= (MaxHealth / 3) * 2)
            {
                CurrentState = BossState.Level2;
            }
            else if (Health > 0)
            {
                CurrentState = BossState.Level1;
            }
        }

        private void UpdateAttackState()
        {
            float dt = (float)Globals.gameTime.TotalGameTime.TotalSeconds;

            switch (CurrentAttack)
            {
                case BossAttack.Hover:
                    Hover(dt);
                    break;
                case BossAttack.FlyTrough:
                    power_sound.PlaySound();
                    FlyThrough();
                    break;
                case BossAttack.GoBack:
                    GetBack();
                    break;
                case BossAttack.Spawn:
                    Spawn();
                    break;
                default:
                    break;
            }

            Timer();
        }
        #endregion

        #region Timer
        private void Timer()
        {
            if (flyTimer > TimeSpan.FromSeconds(flyAttackRate))
            {
                CurrentAttack = BossAttack.FlyTrough;
                flyTimer = TimeSpan.Zero;
            }

            if (CurrentAttack != BossAttack.Spawn)
            {
                if (bombTimer > TimeSpan.FromSeconds(bombRate))
                {
                    ShootBombs();
                    bombTimer = TimeSpan.Zero;
                }

                if (bulletTimer > TimeSpan.FromSeconds(bulletRate))
                {
                    ShootBullets();
                    bulletTimer = TimeSpan.Zero;
                }
            }
        }
        #endregion

        #region Attacks

        private void Spawn()
        {
            spawnDelay += Globals.gameTime.ElapsedGameTime;

            if (spawnDelay > TimeSpan.FromSeconds(6))
            {
                CurrentAttack = BossAttack.GoBack;
                IsBegining = false;
            }
        }

        private void Hover(float dt)
        {
            prevPos.Y += (float)Math.Sin(dt * speed) * amplitute;
        }

        private void FlyThrough()
        {
            if (flyTimer > TimeSpan.FromMilliseconds(800))
            {
                fly_sound.PlaySound();
                velocity.Y = Globals.random.Next(0, 2) == 0 ? 0.2f : -0.2f;
                velocity.X -= 0.8f;

                if (prevPos.X + dimension.X < -100)
                {
                    velocity.X = 0f;
                    velocity.Y = 0f;
                    prevPos.X = Globals.WindowWidth - 310;
                    prevPos.Y = Globals.WindowHeight;
                    CurrentAttack = BossAttack.GoBack;
                }
            }
        }

        private void GetBack()
        {
            // TODO: make this pretty(BERNI!)
            velocity = Vector2.Zero;

            if (prevPos.X > startPos.X)
                prevPos.X -= 5f;
            if (prevPos.X < startPos.X)
                prevPos.X += 5f;
            if (prevPos.Y > startPos.Y)
                prevPos.Y -= 5f;
            if (prevPos.Y < startPos.Y)
                prevPos.Y += 5f;

            if (Vector2.Distance(prevPos, startPos) < 50)
            {
                CurrentAttack = BossAttack.Hover;
            }
        }
        #endregion

        #region Events
        public void OnCollision()
        {
            CurrentState = BossState.Hit;
        }

        private void OnDie()
        {
            Main.CurrenGameState = GameState.Win;
        }
        #endregion

        #region Shooting
        private void Shooter(List<ICollidable> projectiles)
        {
            foreach (var b in projectiles)
            {
                Projectile proj = (Projectile)b;
                proj.Update();
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                Projectile proj = (Projectile)projectiles[i];

                if (!proj.IsVisible)
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        private void ShootBombs()
        {
            BossBomb bomb = new BossBomb("Images\\boss_bomb_sheet",
                new Vector2(position.X + dimension.X / 2, position.Y + dimension.Y / 2),
                new Vector2(100, 100),
                new Vector2(0, 0),
                3
            );

            if (bombs.Count < 1)
            {
                bombs.Add(bomb);
                bomb_sound.PlaySound();
            }
        }

        private void ShootBullets()
        {
            BossBullet bullet = new BossBullet("Images\\boss_fireball_sheet",
                new Vector2(position.X + dimension.X / 2, position.Y + dimension.Y / 2),
                new Vector2(64, 32),
                new Vector2(-6, 0),
                3
            );

            if (bullets.Count < 1)
            {
                bullets.Add(bullet);
                bullet_sound.PlaySound();
            }
        }
    }
    #endregion
}
