using System.Drawing;


namespace StarWars
{
    interface ICollision
    {
        Rectangle Rect { get; }
        bool Collision(ICollision obj);
    }
}
