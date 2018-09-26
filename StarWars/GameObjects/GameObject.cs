using System;
using System.Drawing;
using System.Windows.Forms;

namespace StarWars
{
    internal abstract class GameObject : ICollision
    {
        protected Point _Position;
        protected Point _Speed;
        protected Size _Size;

        public Rectangle Rect => new Rectangle(_Position, _Size);

        protected GameObject(Point Position, Point Speed, Size Size)
        {
            _Position = Position;
            _Speed = Speed;
            _Size = Size;
        }

        public bool Collision(ICollision obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return Rect.IntersectsWith(obj.Rect);
        }

        public abstract void Draw();
        public abstract void Update();
    }
}
