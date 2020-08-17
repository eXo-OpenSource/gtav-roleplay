using System.ComponentModel.DataAnnotations.Schema;
using server.Util.Log;

namespace server.Inventory
{
	public partial class PermanentInventory : Inventory
	{
		[NotMapped]
		private static readonly Logger<PermanentInventory> Logger = new Logger<PermanentInventory>();

	}
}
