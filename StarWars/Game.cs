using StarWars.GameObjects;
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
        private static Ship __Ship;
        private static MedKit __MedKit;
        private static Logger logger;

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
            logger = new Logger();
            var console_logger = new ConsoleMessageLogger();
            var file_logger = new FileMessageLogger("game.log");
            logger.AddObserver(console_logger.LogMessage);
            logger.AddObserver(file_logger.FileLogMessage);


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
                logger.LoggerProcess("Asteroid was created");
            }
            __Bullet = new Bullet(new Point(0, 200), new Size(4, 1));
            logger.LoggerProcess("Bullet was created");
            __MedKit = new MedKit(400);
            __Ship = new Ship(400);
            logger.LoggerProcess("Ship was created");
            __Ship.ShipDie += OnShipDie;
            __Ship.ShipMedicate += OnShipMedicate;
        }

        private static void OnShipMedicate(object sender, EventArgs e)
        {
            __Ship.Energy += 10;
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

            __MedKit?.Draw();

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
                    logger.LoggerProcess("Asteroid was destroyed");
                    __Bullet = null;
                    System.Media.SystemSounds.Hand.Play();
                    continue;
                }

                if (__Ship != null && asteroid.Collision(__Ship))
                {
                    var rnd = new Random();
                    var damage = rnd.Next(1, 50);
                    __Ship.Energy -= damage;
                    logger.LoggerProcess($"ship was damaged by {damage} current Energy = {__Ship.Energy}");
                    if (__Ship.Energy <= 0)
                    {
                        __Ship.Die();
                        logger.LoggerProcess("Ship was destroyed. Game Over");
                    }
                }
            }

            __Bullet?.Update();

            __MedKit?.Update();

            if (__Ship != null && __MedKit != null && __MedKit.Collision(__Ship))
            {
                logger.LoggerProcess($"{__Ship.Energy} before");
                __MedKit = null;
                __Ship.Medicate();
                logger.LoggerProcess($"{__Ship.Energy} after");
            }

        }


    }
}
