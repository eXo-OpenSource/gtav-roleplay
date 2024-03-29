using System.Collections.Generic;
using Exo.Rp.Core.Jobs.Jobs;

namespace Exo.Rp.Core.Jobs
{
    internal class JobManager : IManager
    {
        private readonly Dictionary<int, Job> _jobs;

        public JobManager()
        {
            _jobs = new Dictionary<int, Job>
            {
                {1, new WasteCollector(1)},
                {2, new LawnCaretaker(2)},
                {3, new Farmer(3)},
                {4, new Miner(4)},
                {5, new PizzaDelivery(5)},
                {6, new WoodCutter(6)}
            };
        }

        public Job GetJob(int id)
        {
            return _jobs.ContainsKey(id) ? _jobs[id] : null;
        }
    }
}