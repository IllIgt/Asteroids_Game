using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class FullTimeWorker : Worker
    {
        public override String Type { get; } = "Full Time";

        public override double Calculate()
        {
            return Pay;
        }
    }
}
