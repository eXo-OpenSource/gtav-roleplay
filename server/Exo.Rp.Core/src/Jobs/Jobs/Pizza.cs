using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Streamer;
using server.Vehicles;
using server.Streamer.Entities;
using server.Streamer.Private;
using Object = AltV.Net.Elements.Entities.IWorldObject;

namespace server.Jobs.Jobs
{
	public class Pizza
	{
		public double MaxCapacity { get; set; }
		public double Capacity { get; set; }
		public PrivateEntity DeliveryBlip { get; set; }
		public Colshape.Colshape DeliveryCol { get; set; }
		public PrivateEntity IntakeBlip { get; set; }
		public Colshape.Colshape IntakeCol { get; set; }
	}
}
