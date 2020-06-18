#region Impressum
// Yoi Shiroi Gohan MMP1
// Manuel Obertlik
// Multimedia Technology
// FH Salzburg 1910601028
#endregion

#region Includes
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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
    public enum Powerup
    { 
        Heart,
        BigGun
    }

    public class PowerUp : GameObject, ICollidable
    {
        private SoundItem powerup_sound;
        private TimeSpan spawnTimer = TimeSpan.Zero;
        private bool setTimer;
        private bool isMoving;

        public Powerup Power { get; set; }
        public int Speed { get; private set; }

        public PowerUp(string path, Vector2 position, Vector2 dimension, Powerup power) : base(path, position, dimension)
        {
            powerup_sound = new SoundItem("Audio\\powerup", 0.25f, false);
            Power = power;
            Speed = Globals.POWERUP_SPEED;
            setTimer = false;            
            isMoving = true;
        }

        public override void Update()
        {
            if (isMoving)
            {
                Move();
            }

            if (setTimer)
            {
                Timer();
            }

            if (position.X < -100)
            {
                Respawn();
                setTimer = true;
                isMoving = false;
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void OnCollision()
        {
            powerup_sound.PlaySound();
            Respawn();
            setTimer = true;
            isMoving = false;
        }

        public void Move()
        { 
            position.X -= Speed;
        }

        private void Respawn()
        {
            Random rand = Globals.random;
            Power = (Powerup)Globals.random.Next(0, 2);

            switch (Power)
            {
                case Powerup.Heart:
                    texture = Globals.Content.Load<Texture2D>("Images\\heart");
                    break;
                case Powerup.BigGun:
                    texture = Globals.Content.Load<Texture2D>("Images\\powerup_ricebowl");
                    break;
            }

            position = new Vector2(Globals.WindowWidth + 300, rand.Next(100, Globals.WindowHeight - 150));
            setTimer = false;
        }

        private void Timer()
        {
            spawnTimer += Globals.gameTime.ElapsedGameTime;

            if (spawnTimer > TimeSpan.FromSeconds(Globals.POWERUP_SPAWNRATE))
            {
                isMoving = true;
                setTimer = false;
                spawnTimer = TimeSpan.Zero;
            }
        }
    }
}
