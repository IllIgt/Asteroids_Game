using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars
{
    class Ship : GameObject
    {
        public event EventHandler ShipDie;
        protected virtual void OnShipDie(EventArgs e) => ShipDie?.Invoke(this, e);

        public int Energy { get; set; } = 100;

        public Ship(int Y) : base(new Point(10, Y), new Point(5, 5), new Size(10, 10)) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Rect);
        }

        public override void Update()
        {
            base.Update();
        }

        public void Up()
        {
            if (_Position.Y > 0) _Position.Y -= _Speed.Y;
        }

        public void Down()
        {
            if (_Position.Y < Game.Height - _Size.Height) _Position.Y += _Speed.Y;
        }

        public void Die()
        {
            OnShipDie(EventArgs.Empty);
        }
    }
}
