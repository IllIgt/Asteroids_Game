using System;
using System.Collections.Generic;
using System.Drawing;

namespace StarWars
{
    class Asteroid : GameObject, ICloneable, IComparable<Asteroid>, IEquatable<Asteroid>
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

        public object Clone()
        {
            return new Asteroid(_Position, _Speed, _Size) { Power = Power };
        }

        public int CompareTo(Asteroid other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            return Comparer<int>.Default.Compare(Power, other.Power);
        }

        public bool Equals(Asteroid other)
        {
            if (other == null) return false;
            return Power == other.Power
                && _Position == other._Position
                && _Speed == other._Speed
                && _Size == other._Size;
        }
    }
}
