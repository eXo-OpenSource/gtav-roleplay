using System.Collections.Generic;

namespace Exo.Rp.Core.Jobs
{
    public class CharacterJobData
    {
        public Dictionary<int, Dictionary<int, int>> Upgrades { get; set; } // JobId | CategoryId | UpgradeId
        public Dictionary<int, int> Points { get; set; } // JobId | Points

        public CharacterJobData()
        {
            Upgrades = new Dictionary<int, Dictionary<int, int>>();
            Points = new Dictionary<int, int>();
        }
    }
}