using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using models.Enums;
using server.Streamer.Entities;
using server.Streamer.Private;
using server.Util.Log;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IPlayer = server.Players.IPlayer;


namespace server.Jobs.Jobs
{
	internal class PizzaDelivery : Job
	{
		public PizzaDelivery(int jobId) : base(jobId)
		{
			Name = "Pizzalieferant";
			Description = "Teile Pizza in der Stadt aus!";
			PedPosition = new Position(-1529.1692f, -908.7824f, 10.155273f);
			Init();
		}

		private static readonly Logger<PizzaDelivery> Logger = new Logger<PizzaDelivery>(); 

		private Pizza Pizza;


		private Random randomizeDelivery = new Random();

		private readonly Position vehSpawnPoint = new Position(-1524.422f, -913.38464f, 10.155273f);

		private readonly float vehSpawnRot = 250f;

		private readonly Position intakeSpot = new Position(-1526.4132f, -911.0242f, 10.155273f);

		private readonly Position[] deliverySpot =
		{
			new Position(-1506.4352f, -930.46155f, 10.155273f)
		};

		public override void StartJobForPlayer(IPlayer player)
		{
			base.StartJobForPlayer(player);
			player.SendSuccess($"Arbeit als {Name} gestartet!");
			player.SendInformation("Steige auf die Faggio auf!");
			player.Emit("JobPizza:StartJob");

			var veh = CreateJobVehicle(player, VehicleModel.Faggio, vehSpawnPoint, vehSpawnRot);

			Pizza = new Pizza
			{
				MaxCapacity = 1,
				Capacity = 0,
				IntakeBlip = new PrivateBlip(intakeSpot, 0, 300) { Sprite = 1, Name = "Chef" },
				IntakeCol = (Colshape.Colshape)Alt.CreateColShapeSphere(intakeSpot, 1.9f)
			};

			Core.GetService<PrivateStreamer>().AddEntity(Pizza.IntakeBlip);
			Pizza.IntakeBlip.AddVisibleEntity(player.Id);

			Pizza.IntakeCol.OnColShapeEnter += OnIntakeMarkerHit;

			CreateRandomDelivery(player);
		}

		public void CreateRandomDelivery(IPlayer player)
		{
			if (!(Pizza.Capacity == 0)) {
				player.SendError("Du hast bereits einen Auftrag!");
				return;
			}

			int randomDeliverySpot = randomizeDelivery.Next(0, deliverySpot.Length);

			Pizza.DeliveryBlip = new PrivateBlip((Position)deliverySpot.GetValue(randomDeliverySpot), 0, 300) { Sprite = 1, Name = "Abgabeort" };
			Pizza.DeliveryCol = (Colshape.Colshape)Alt.CreateColShapeSphere((Position)deliverySpot.GetValue(randomDeliverySpot), 1.9f);
			Core.GetService<PrivateStreamer>().AddEntity(Pizza.DeliveryBlip);
			Pizza.DeliveryBlip.AddVisibleEntity(player.Id);

			Pizza.DeliveryCol.OnColShapeEnter += OnDeliveryMarkerHit;
		}

		public void OnIntakeMarkerHit(Colshape.Colshape col, IEntity entity)
		{
			if (!(entity is IPlayer player)) return;
			if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
				!player.GetCharacter().IsJobActive() || player.IsInVehicle) return;
			player.SendInformation("Warten auf den Chef...");

			Task.Delay(5000).ContinueWith(_ => {
				player.SendInformation("Du hast einen neuen Auftrag erhalten!");
				player.SendInformation("Fahre nun zum nächsten Kunden!");
				CreateRandomDelivery(player);
			});
		}

		public void OnDeliveryMarkerHit(Colshape.Colshape col, IEntity entity)
		{
			if (!(entity is IPlayer player)) return;
			if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
				!player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

			player.SetSyncedMetaData("JobPizza:PlaceObject", true);
			player.PlayAnimation("amb@medic@standing@kneel@idle_a", "idle_b",
				(int)AnimationFlags.Loop);

			Task.Delay(2000).ContinueWith(_ => {
				Pizza.Capacity++;
				player.SendSuccess("Pizza erfolgreich abgegeben!");
				player.SendInformation("Fahre nun zurück zur Pizzeria!");
				Pizza.DeliveryBlip.RemoveVisibleEntity(player.Id);
				Pizza.DeliveryCol.Remove();
				player.StopAnimation();
			});
		}

		public override void StopJob(IPlayer player)
		{
			base.StopJob(player);
			player.SendInformation($"Arbeit als {Name} beendet!");
			if (!IsJobLeader(player)) return;
			DestroyJobVehicle(player);
		}
	}
}
