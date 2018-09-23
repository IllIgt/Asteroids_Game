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
            __GameObjects = new GameObject[30];

            for (var i =0; i < __GameObjects.Length/2; i++)
            {
                __GameObjects[i] = new GameObject(
                    new Point(600, i * 20), 
                    new Point(15 - i, 15 - i), 
                    new Size(20, 20));
            }

            for (var i = __GameObjects.Length / 2; i < __GameObjects.Length; i++)
            {
                __GameObjects[i] = new Star(
                    new Point(600, i * 20),
                    new Point(i, 0),
                    new Size(5, 5));
            }
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
            {
                game_object.Draw();
            }

            Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __GameObjects)
            {
                game_object.Update();
            }

        }


    }
}
