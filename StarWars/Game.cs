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

            try
            {
                if (form.Width > 1000 || form.Height > 1000 || form.Width < 0 || form.Height < 0)
                    throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Недопустимый размер окна");
                form.Width = 800;
                form.Height = 600;
            }

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
            const int bullet_x = 0;
            const int bullet_y = 200;
       
            if (bullet_x < 0 || bullet_x > 1000 || bullet_y < 0 || bullet_y > 1000)
                throw new ObjectOutOfWindowException("Объект создан за пределами экрана");

            __Bullet = new Bullet(new Point(bullet_x, bullet_y), new Size(4, 1));    
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
                if (asteroid.Collision(__Bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    __Bullet.Regenerate();
                    asteroid.Regenerate();
                }    
            }

            __Bullet.Update();
        }


    }
}
