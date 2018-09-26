using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Interfaces
{
    interface IRegenerable
    {
        Point RegenerationPoint { get;  }
        void Regenerate();
    }
}
