using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars
{
    class ObjectOutOfWindowException : Exception
    {
        public ObjectOutOfWindowException(string message) 
            : base(message) 
        {
        }
    }
}
