using System.Drawing;

namespace StarWars
{
    class Asteroid : GameObject
    {
        private static readonly Image _AsteroidImage = Image.FromFile(@"src\asteroid.png");

        public int Power { get; set; } = 1;

        public Asteroid(Point Position, Point Speed, Size Size)
            : base(Position, Speed, Size)
        {

        }

        public override void Draw()
        {
            var g = Game.Buffer.Graphics;
            g.DrawImage(_AsteroidImage, new Rectangle(_Position, _Size));
        }
    }
}
