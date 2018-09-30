using System;
using System.Drawing;

namespace StarWars.GameObjects
{
    class MedKit : GameObject
    {
        public int Energy { get; set; } = 10;

        public MedKit(int Y) : base(new Point(150, Y), new Point(-5, 0), new Size(20, 20)) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.White, Rect);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
