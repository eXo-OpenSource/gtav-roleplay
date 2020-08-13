using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
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

		private readonly Position[] intakeSpot =
		{
			new Position(-1527.389f, -910.33844f, 10.155273f)
		};

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
				MaxCapacity = 5,
				Capacity = 0,
			};

			CreateRandomDelivery(player);
		}

		public void CreateRandomDelivery(IPlayer player)
		{
			int randomDeliverySpot = randomizeDelivery.Next(0, deliverySpot.Length);

			Pizza.Col = (Colshape.Colshape) Alt.CreateColShapeSphere((Position) deliverySpot.GetValue(randomDeliverySpot), 1.9f);
			Pizza.Blip = new PrivateBlip((Position) deliverySpot.GetValue(randomDeliverySpot), 0, 300) { Sprite = 1, Name = "Abgabeort" };
			Core.GetService<PrivateStreamer>().AddEntity(Pizza.Blip);
			Pizza.Blip.AddVisibleEntity(player.Id);

			Pizza.Col.OnColShapeEnter += OnMarkerHit;
		}

		public void OnMarkerHit(Colshape.Colshape col, IEntity entity)
		{
			if (!(entity is IPlayer player)) return;
			if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
				!player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

			Task.Delay(300).ContinueWith(_ => {
				player.SendSuccess("Pizza erfolgreich abgegeben!");
				player.SendInformation("Fahre nun zurück zur Pizzeria!");
				Pizza.Blip.RemoveVisibleEntity(player.Id);
				player.Emit("JobPizza:PlaceObject");
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
