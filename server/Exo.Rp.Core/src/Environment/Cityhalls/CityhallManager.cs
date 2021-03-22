using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo.Rp.Core.Environment
{
    internal class CityhallManager : IManager
    {
        private readonly Dictionary<int, Cityhall> cityhalls;

        public CityhallManager()
        {
            cityhalls = new Dictionary<int, Cityhall>
            {
                { 1, new LosSantos(1) },
            };
        }
    }
}
