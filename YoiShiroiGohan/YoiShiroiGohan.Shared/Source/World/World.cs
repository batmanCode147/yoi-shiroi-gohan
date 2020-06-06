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
    public class World
    {
        public List<Background> backgrounds;
        public static Ground ground;
        public static Player player;
        public static Boss boss;
        public static List<ICollidable> plates;
        public static PowerUp powerup;

        public World()
        {
            // Backgrounds
            backgrounds = new List<Background>();
            backgrounds.Add(new Background("Images\\background_clouds", new Vector2(0, 0), new Vector2(Globals.WindowWidth, Globals.WindowHeight), 0.1f));
            backgrounds.Add(new Background("Images\\background_clouds", new Vector2(Globals.WindowWidth, 0), new Vector2(Globals.WindowWidth, Globals.WindowHeight), 0.1f));

            // Ground
            ground = new Ground("Images\\ground_cloud", new Vector2(0, Globals.WindowHeight - 100), new Vector2(Globals.WindowWidth, 100));

            // Entities
            boss = new Boss(new Vector2(Globals.WindowWidth + 200, 0), new Vector2(300, 500));
            player = new Player(new Vector2(Globals.WindowWidth/2, 200), new Vector2(60, 50));

            // Plattforms
            plates = new List<ICollidable>();
            plates.Add(new Platform("Images\\plate_cloud", new Vector2(50, 500), new Vector2(200, 50)));
            plates.Add(new Platform("Images\\plate_cloud", new Vector2(300, 350), new Vector2(200, 50)));
            plates.Add(new Platform("Images\\plate_cloud", new Vector2(550, 200), new Vector2(200, 50)));
            plates.Add(new Platform("Images\\plate_cloud", new Vector2(570, 570), new Vector2(200, 50)));
            plates.Add(new Platform("Images\\plate_cloud", new Vector2(800, 350), new Vector2(200, 50)));
            plates.Add(new Platform("Images\\plate_cloud", new Vector2(1050, 200), new Vector2(200, 50)));
            plates.Add(new Platform("Images\\plate_cloud", new Vector2(1020, 570), new Vector2(200, 50)));

            // Powerups
            powerup = new PowerUp("Images\\powerup_ricebowl", 
                        new Vector2(Globals.WindowWidth + 300, Globals.random.Next(100, Globals.WindowHeight-100)),
                        new Vector2(60, 60),
                        Powerup.BigGun
            );

            CollisionManager.playerBullets = player.bullets;
            CollisionManager.bossBombs = boss.bombs;
            CollisionManager.bossBullets = boss.bullets;
            CollisionManager.powerup = powerup;
            CollisionManager.plates = plates;
            CollisionManager.boss = boss;
            CollisionManager.player = player;
            CollisionManager.ground = ground;
        }

        public virtual void Update()
        {
            foreach (var background in backgrounds)
            {
                background.Update();
            }

            ground.Update();

            boss.Update();

            player.Update();

            foreach (var p in plates)
            {
                Platform plate = (Platform)p;
                plate.Update();
            }

            powerup.Update();

            CollisionManager.Update();
        }

        public virtual void Draw()
        {
            foreach (var background in backgrounds)
            {
                background.Draw();
            }

            player.Draw();

            foreach (var p in plates)
            {
                Platform plate = (Platform)p;
                plate.Draw();
            }

            boss.Draw();

            ground.Draw();

            powerup.Draw();
        }
    }
}
