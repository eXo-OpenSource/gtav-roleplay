using System.Collections.Generic;
using server.Jobs.Jobs;

namespace server.Jobs
{
    internal class JobManager
    {
        private static readonly Dictionary<int, Job> Jobs;

        static JobManager()
        {
            Jobs = new Dictionary<int, Job>
            {
                {1, new WasteCollector(1)},
                {2, new LawnCaretaker(2)},
                {3, new Farmer(3)},
                {4, new Miner(4)}
            };
        }

        public static Job GetJob(int id)
        {
            return Jobs.ContainsKey(id) ? Jobs[id] : null;
        }
    }
}