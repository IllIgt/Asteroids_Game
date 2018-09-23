using System.Drawing;
using System.Windows.Forms;

namespace StarWars
{
    class GameObject
    {
        protected Point _Position;
        protected Point _Speed;
        protected Size _Size;

        public GameObject(Point Position, Point Speed, Size Size)
        {
            _Position = Position;
            _Speed = Speed;
            _Size = Size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, new Rectangle(_Position, _Size));
        }

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

    class Star : GameObject
    {
        public Star(Point Position, Point Speed, Size Size)
            :base(Position, Speed, Size)
        {

        }

        public override void Draw()
        {
            var g = Game.Buffer.Graphics;
            g.DrawLine(Pens.White,
                _Position.X, _Position.Y,
                _Position.X + _Size.Width, _Position.Y + _Size.Height);
            g.DrawLine(Pens.White,
                _Position.X + _Size.Width, _Position.Y,
                _Position.X, _Position.Y + _Size.Height);
    
        }

        public override void Update()
        {
            _Position.X -= _Speed.X;
            if (_Position.X < 0)
                _Position.X = Game.Width + _Size.Width;
        }
    }

    class Asteroid : GameObject
    {
        public Asteroid(Point Position, Point Speed, Size Size)
            : base(Position, Speed, Size)
        {

        }

        public override void Draw()
        {
            var g = Game.Buffer.Graphics;
            Image acteroidImage = Image.FromFile(@"..\..\asteroid.png");
            g.DrawImage(acteroidImage, new Point[] {
                new Point(_Position.X, _Position.Y),
                new Point(_Position.X + 20, _Position.Y),
                new Point(_Position.X, _Position.Y - 20)
            });
        }
    }
}
