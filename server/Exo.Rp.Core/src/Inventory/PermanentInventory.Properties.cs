using System.ComponentModel.DataAnnotations.Schema;
using Exo.Rp.Core.Util.Log;

namespace Exo.Rp.Core.Inventory
{
    public partial class PermanentInventory : Inventory
    {
        [NotMapped]
        private static readonly Logger<PermanentInventory> Logger = new Logger<PermanentInventory>();

    }
}