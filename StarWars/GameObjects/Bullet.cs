using StarWars.Interfaces;
using System.Drawing;


namespace StarWars
{
    class Bullet : GameObject, IRegenerable
    {
        public Point RegenerationPoint => new Point(0, 200);

        public Bullet(Point Position, Size Size) : base(Position, new Point(), Size)
        {
 
        }

        public void Regenerate()
        {
            _Position = RegenerationPoint;
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
