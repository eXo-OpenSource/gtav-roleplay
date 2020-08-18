using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Models.Enums;
using Exo.Rp.Models.Inventory;
using Exo.Rp.Sdk;

//using shared.Inventory;

namespace Exo.Rp.Core.Inventory
{
    public partial class Inventory
    {
        [NotMapped]
        private static readonly ILogger<Inventory> Logger = new Logger<Inventory>();
        [NotMapped]
        private const int Size = 3;
        [NotMapped]
        private const string Title = "Inventar";
        [NotMapped]
        protected Dictionary<BagNames, BagDto> BagMap = new Dictionary<BagNames, BagDto>();
    }
}