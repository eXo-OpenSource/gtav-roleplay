using System.Collections.Generic;
using Exo.Rp.Core.Environment.CarRentTypes;
using Exo.Rp.Core.Environment;

namespace Exo.Rp.Core.Environment
{
    public class EnvironmentManager : IManager
    {
        public List<object> environment = new List<object>();

        public EnvironmentManager()
        {
            environment.Add(new CarRent());
            environment.Add(new LsCityhall(1));
            environment.Add(new LsDrivingschool(1));
        }
    }
}