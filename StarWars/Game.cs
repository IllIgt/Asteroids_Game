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

        public static BufferedGraphics Buffer { get; private set; }
        
        public static int Width { get; private set; }
        public static int Height { get; private set; }


        public static void Init(Form form)
        {
            Width = form.Width;
            Height = form.Height;
            __Context = BufferedGraphicsManager.Current;

            var graphics = form.CreateGraphics();
            Buffer = __Context.Allocate(graphics, new Rectangle(0, 0, Width, Height));
            __Timer.Tick += OnTimerTick;
            __Timer.Enabled = true;
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

            foreach (var asteroid in __Asteroids)
                asteroid.Draw();

            __Bullet.Draw();

            Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __GameObjects)
            {
                game_object.Update();
            }

            foreach (var asteroid in __Asteroids)
            {
                asteroid.Update();
            }

            __Bullet.Update();
        }


    }
}
