using System;
using System.Drawing;
using StarWars.Interfaces;

namespace StarWars
{
    class Asteroid : GameObject, ICloneable, IRegenerable
    {
        private static readonly Image _AsteroidImage = Image.FromFile(@"src\asteroid.png");
        public Point RegenerationPoint => new Point(100, new Random().Next(0, _Position.Y));

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

        public override void Update()
        {
            _Position.X += _Speed.X;
            _Position.Y += _Speed.Y;
            if (_Position.X < 0 || _Position.X > Game.Width - _Size.Width)
                _Speed.X *= -1;
            if (_Position.Y < 0 || _Position.Y > Game.Height - _Size.Height)
                _Speed.Y *= -1;
        }

        public void Regenerate()
        {
            _Position = RegenerationPoint;
        }

        public object Clone()
        {
            return new Asteroid(_Position, _Speed, _Size) { Power = Power };
        }
    }
}
