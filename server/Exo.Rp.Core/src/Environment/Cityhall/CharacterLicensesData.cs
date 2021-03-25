using System.Collections.Generic;

namespace Exo.Rp.Core.Environment
{
    public class CharacterLicenseData
    {
        public Dictionary<int, int> Licenses { get; set; }

        public CharacterLicenseData() {
            Licenses = new Dictionary<int, int>();
        }
    }
}