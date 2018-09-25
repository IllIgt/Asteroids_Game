using System.Drawing;
using System.Windows.Forms;

namespace StarWars
{
    internal abstract class GameObject
    {
        protected Point _Position;
        protected Point _Speed;
        protected Size _Size;

        protected GameObject(Point Position, Point Speed, Size Size)
        {
            _Position = Position;
            _Speed = Speed;
            _Size = Size;
        }

        public abstract void Draw();

        public virtual void Update()
        {
            _Position.X += _Speed.X;
            _Position.Y += _Speed.Y;
            if (_Position.X < 0 || _Position.X > Game.Width - _Size.Width)
                _Speed.X *= -1;
            if (_Position.Y < 0 || _Position.Y > Game.Height - _Size.Height) 
                _Speed.Y *= -1;
        }
    }
}
