using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    abstract class Worker
    {
        public Double Pay { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public abstract String Type { get; }

        public abstract double Calculate();
    }
}
