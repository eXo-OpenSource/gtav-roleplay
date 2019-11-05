using System.Collections.Generic;

namespace server.Jobs
{
    public class CharacterJobData
    {
        public Dictionary<int, Dictionary<int, int>> Upgrades { get; set; } // JobId | CategoryId | UpgradeId
        public Dictionary<int, int> Points { get; set; } // JobId | Points
    }
}
