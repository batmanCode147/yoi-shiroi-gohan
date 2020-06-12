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
    public interface ICollidable
    {
        Rectangle Bounds { get; }
        void OnCollision();
    }

    public static class CollisionManager
    {
        public static List<ICollidable> plates;
        public static List<ICollidable> playerBullets;
        public static List<ICollidable> bossBombs;
        public static List<ICollidable> bossBullets;
        public static Ground ground;
        public static PowerUp powerup;

        public static Player player;
        public static Boss boss;

        private static bool isCollided = false;

        // IFRAME 
        private static TimeSpan iframeTimer = TimeSpan.Zero;
        private static double iFrameRate = 0.5;
        private static bool inIFrame = false;

        public static void Update()
        {
            if(inIFrame)
                iframeTimer += Globals.gameTime.ElapsedGameTime;

            if (iframeTimer > TimeSpan.FromSeconds(iFrameRate))
            {
                inIFrame = false;
                iframeTimer = TimeSpan.Zero;
            }

            WorldConstraints();
            PlateCollision();
            BossCollision();
            PlayerCollision();
            PowerUpCollision();
            GroundCollision();
        }

        #region Collisions
        private static void GroundCollision()
        {
            if (CheckCollision(player.Bounds, ground.Bounds) && player.Velocity == Vector2.Zero && player.IsAlive)
            {
                player.PlayerState = Anim.Chill;
            }
        }

        private static void PowerUpCollision()
        {
            if (CheckCollision(player.Bounds, powerup.Bounds) && player.IsAlive)
            {
                switch (powerup.Power)
                {
                    case Powerup.Heart:
                        if (player.Health < player.MaxHealth)
                        { 
                            player.Health++;
                            Console.WriteLine("HEALTH UP");
                        }
                        break;
                    case Powerup.BigGun:
                        player.CurrentShootingMode = ShootingMode.BigGun;
                        break;
                    default:
                        break;
                }

                powerup.OnCollision();
            }
        }

        private static void PlateCollision()
        {
            //FIXME: Fucking Teleport Bug FIX IT MOTHAAAASUUKKAA!!
            foreach (var plate in plates)
            {
                if (IsOnTopCollision(plate.Bounds))
                {
                    if (player.Bounds.Bottom >= plate.Bounds.Top)
                    {
                        player.Position = new Vector2(player.Position.X, plate.Bounds.Y - player.Dimension.Y);
                        player.Velocity = new Vector2(player.Velocity.X, 0f);
                        player.IsOnGround = true;
                    }
                }
            }
        }

        private static void PlayerCollision()
        {
            foreach (var proj in playerBullets)
            {
                if (CheckCollision(boss.Bounds, proj.Bounds) && boss.CurrentAttack != BossAttack.Spawn)
                {
                    Projectile projectile = (Projectile)proj;

                    projectile.OnCollision();
                    boss.OnCollision();
                    boss.Health -= projectile.Damage;
                }
            }

            if (CheckCollision(player.Bounds, boss.Bounds) && !isCollided && player.IsAlive && !inIFrame)
            {
                isCollided = true;
                inIFrame = true;
                player.OnCollision();
            }
            
            if(!CheckCollision(player.Bounds, boss.Bounds))
            {
                isCollided = false;
            }


            //TODO: Test Shootable BossBombs
            foreach (var proj in playerBullets)
            {
                foreach (var item in bossBombs)
                {
                    if (typeof(PlayerBigBullet) == proj.GetType())
                    {
                        if (CheckCollision(proj.Bounds, item.Bounds))
                        {
                            item.OnCollision();
                        }
                    }
                }
            }
        }

        private static void BossCollision()
        {
            foreach (var proj in bossBombs)
            {
                if (CheckCollision(player.Bounds, proj.Bounds) && player.IsAlive && !inIFrame)
                {
                    Projectile projectile = (Projectile)proj;

                    projectile.OnCollision();
                    player.OnCollision();
                    inIFrame = true;
                }
            }

            foreach (var proj in bossBullets)
            {
                if (CheckCollision(player.Bounds, proj.Bounds) && player.IsAlive && !inIFrame)
                {
                    Projectile projectile = (Projectile)proj;

                    projectile.OnCollision();
                    player.OnCollision();
                    inIFrame = true;
                }
            }
        }

        public static void WorldConstraints()
        {
            Rectangle prevBounds = player.Bounds;

            if (prevBounds.Bottom >= Globals.WindowHeight - 90)
            {
                player.Position = new Vector2(player.Position.X, (Globals.WindowHeight - 90 - player.Dimension.Y));
                player.IsOnGround = true;
            }
            if (prevBounds.Left < 0)
            {
                player.Position = new Vector2(0, player.Position.Y);
            }
            if (prevBounds.Right > Globals.WindowWidth)
            {
                player.Position = new Vector2(Globals.WindowWidth - player.Dimension.X, player.Position.Y);
            }
        }
        #endregion

        #region Helper
        public static bool CheckCollision(Rectangle rectA, Rectangle rectB)
        {
            if (rectA.Left < rectB.Right &&
                rectA.Right > rectB.Left &&
                rectA.Top < rectB.Bottom &&
                rectA.Bottom > rectB.Top)
            {
                return true;
            }
            return false;
        }

        public static bool IsOnTopCollision(Rectangle plate)
        {
            if (player.Bounds.Bottom + player.Velocity.Y > plate.Top &&
                player.Bounds.Top < plate.Top &&
                player.Bounds.Right > plate.Left &&
                player.Bounds.Left < plate.Right)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
