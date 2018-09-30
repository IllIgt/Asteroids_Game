using System;
using System.Drawing;
using System.Windows.Forms;


namespace StarWars
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static readonly Timer __Timer = new Timer { Interval = 100 };
        private static GameObject[] __GameObjects;
        private static Asteroid[] __Asteroids;
        private static Bullet __Bullet;
        public static Ship __Ship;

        public static BufferedGraphics Buffer { get; private set; }
        
        public static int Width { get; private set; }
        public static int Height { get; private set; }


        public static void Init(Form form)
        {
            Width = form.Width;
            Height = form.Height;
            form.KeyDown += OnGameFormKeyDown;

            __Context = BufferedGraphicsManager.Current;

            var graphics = form.CreateGraphics();
            Buffer = __Context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            __Timer.Tick += OnTimerTick;
            __Timer.Enabled = true;
        }

        private static void OnGameFormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    __Bullet = new Bullet(__Ship.Rect.Location, new Size(4, 1));
                    break;
                case Keys.Up:
                    __Ship.Up();
                    break;
                case Keys.Down:
                    __Ship.Down();
                    break;
#if DEBUG
                case Keys.W:
                    __Ship.Die();
                    break;
#endif
            }
        }

        public static void Load()
        {
            const int objects_count = 30;
            __GameObjects = new GameObject[objects_count];

            for (var i = 0; i < objects_count; i++)
            {
                __GameObjects[i] = new Star(
                    new Point(600, i * 20),
                    new Point(i, 0),
                    new Size(5, 5));
            }

            const int asteroids_count = 10;
            __Asteroids = new Asteroid[asteroids_count];

            var rnd = new Random();
            for (var i = 0; i < asteroids_count; i++)
            {
                var speed = rnd.Next(3, 50);
                __Asteroids[i] = new Asteroid(
                    new Point(100, rnd.Next(0, Height)),
                    new Point(-speed, speed),
                    new Size(speed, speed));
            }
            __Bullet = new Bullet(new Point(0, 200), new Size(4, 1));
            __Ship = new Ship(400);
            __Ship.ShipDie += OnShipDie;   
        }

        private static void OnShipDie(object sender, EventArgs e)
        {
            __Ship = null;
            __Timer.Enabled = false;
            var g = Buffer.Graphics;
            g.Clear(Color.DarkBlue);
            g.DrawString(
                "Game Over",
                new Font(
                    FontFamily.GenericSansSerif, 
                    60, 
                    FontStyle.Bold | FontStyle.Underline),
                Brushes.White,
                200, 100);


            Buffer.Render();
        }

        private static void OnTimerTick(object Sender, EventArgs e)
        {
            Update();
            Draw();
        }

        public static void Draw()
        {
            var g = Buffer.Graphics;
            g.Clear(Color.Black);

            foreach (var game_object in __GameObjects)
                game_object.Draw();

            for (var i = 0; i < __Asteroids.Length; i++)
                __Asteroids[i]?.Draw();

            __Bullet?.Draw();

            __Ship?.Draw();

            Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __GameObjects)
            {
                game_object.Update();
            }

            for (var i = 0; i < __Asteroids.Length; i++)
            {
                var asteroid = __Asteroids[i];
                if (asteroid == null) continue;
                asteroid.Update();
                if (__Bullet != null && asteroid.Collision(__Bullet))
                {
                    __Asteroids[i] = null;
                    __Bullet = null;
                    System.Media.SystemSounds.Hand.Play();
                    continue;
                }

                if (__Ship != null && asteroid.Collision(__Ship))
                {
                    var rnd = new Random();
                    __Ship.Energy -= rnd.Next(1, 10);
                    if (__Ship.Energy <= 0) __Ship.Die();
                }
            }

            __Bullet?.Update();


        }


    }
}
