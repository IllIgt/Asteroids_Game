using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    class PartTimeWorker : Worker
    {
        public override String Type { get; } = "Part Time";

        public override double Calculate()
        {
            return 20.8 * 8 * Pay;
        }
    }
}
