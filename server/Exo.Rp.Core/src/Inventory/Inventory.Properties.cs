using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using models.Enums;
using models.Inventory;
using server.Util.Log;

//using shared.Inventory;

namespace server.Inventory
{
    public partial class Inventory
    {
        [NotMapped]
        private static readonly Logger<Inventory> Logger = new Logger<Inventory>();
        [NotMapped]
        private const int Size = 3;
        [NotMapped]
        private const string Title = "Inventar";
        [NotMapped]
        protected Dictionary<BagNames, BagDto> BagMap = new Dictionary<BagNames, BagDto>();
    }
}