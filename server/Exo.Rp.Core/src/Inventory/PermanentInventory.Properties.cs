using System.ComponentModel.DataAnnotations.Schema;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;

namespace Exo.Rp.Core.Inventory
{
    public partial class PermanentInventory : Inventory
    {
        [NotMapped]
        private static readonly ILogger<PermanentInventory> Logger = new Logger<PermanentInventory>();

    }
}