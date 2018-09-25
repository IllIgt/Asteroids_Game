using System;
using System.Drawing;


namespace StarWars
{
    class Bullet : GameObject
    {
        public Bullet(Point Position, Size Size) : base(Position, new Point(), Size)
        {

        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Orange, new Rectangle(_Position, _Size));
        }

        public override void Update()
        {
            _Position.X += 3;
        }
    }
}
