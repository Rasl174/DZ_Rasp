using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_code_6
{
    class Player { }
    class GunController { }
    class Target { }
    class Unit
    {
        public IReadOnlyCollection<Unit> UnitsToGet { get; private set; }
    }
}
