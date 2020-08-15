using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using models.Enums;
using server.Players.Characters;
using server.Streamer.Entities;
using server.Streamer.Private;
using server.Util.Log;
using server.Vehicles;
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
			new Position(-1152.0028076171875f,-913.3242797851562f, 6.795074939727783f),
			new Position(-1154.706787109375f,-931.0313720703125f, 2.7938404083251953f),
			new Position(-1204.7479248046875f,-945.4142456054688f, 3.760294198989868f),
			new Position(-1076.195556640625f,-1027.097412109375f, 4.544915199279785f),
			new Position(-1103.927490234375f,-1059.921875f, 2.7320327758789062f),
			new Position(-1082.522705078125f,-1139.193359375f, 2.1585984230041504f),
			new Position(-1063.4395751953125f,-1160.269287109375f, 2.745879650115967f),
			new Position(-985.9324340820312f,-1121.68212890625f, 4.54555606842041f),
			new Position(-952.2551879882812f,-1077.439697265625f, 2.6759419441223145f),
			new Position(-1041.7869873046875f,-1025.885009765625f, 2.746367931365967f),
			new Position(-1090.1552734375f,-926.4697875976562f, 3.1039276123046875f),
			new Position(-1031.434814453125f,-903.0101928710938f, 3.6962907314300537f),
			new Position(-1711.9129638671875f,-492.9061279296875f, 41.619361877441406f),
			new Position(-1710.71337890625f,-494.0269470214844f, 41.61927795410156f),
			new Position(-1715.360595703125f,-447.0412292480469f, 42.64917755126953f),
			new Position(-1678.33740234375f,-401.308837890625f, 47.52849578857422f),
			new Position(-1590.41796875f,-413.010498046875f, 43.070064544677734f),
			new Position(-1585.635986328125f,-464.0638122558594f, 37.2751579284668f),
			new Position(-1288.5814208984375f,-430.42608642578125f, 35.15846252441406f),
			new Position(-1163.7169189453125f,-349.1763000488281f, 36.63737106323242f),
			new Position(-1213.3021240234375f,-406.6300354003906f, 34.14012908935547f),
			new Position(-1246.6240234375f,-1183.0050048828125f, 7.657731056213379f),
			new Position(-1226.135498046875f,-1207.40966796875f, 8.271016120910645f),
			new Position(-1246.9476318359375f,-1358.26904296875f, 7.820460319519043f),
			new Position(-1109.330078125f,-1482.4716796875f, 4.926892280578613f),
			new Position(-1085.57763671875f,-1503.79638671875f, 5.70627498626709f),
			new Position(-1057.83203125f,-1540.8349609375f, 5.105527400970459f),
			new Position(-1114.684814453125f,-1577.625f, 4.543093681335449f),
			new Position(-1023.4681396484375f,-1614.369873046875f, 5.08709716796875f),
			new Position(-951.9983520507812f,-1552.8548583984375f, 5.177528381347656f),
			new Position(-935.6129150390625f,-1523.1470947265625f, 5.243705749511719f),
			new Position(-968.8291625976562f,-1431.1435546875f, 7.758563041687012f),
			new Position(-1285.3060302734375f,-1253.225341796875f, 4.524023532867432f),
			new Position(-1349.6925048828125f,-1161.5120849609375f, 4.507063388824463f),
			new Position(-1788.0009765625f,-671.9785766601562f, 10.65199089050293f),
			new Position(-1592.47314453125f,-277.6814270019531f, 52.68074417114258f),
			new Position(-1533.3323974609375f,-275.5545349121094f, 49.737735748291016f),
			new Position(-1533.4342041015625f,-326.8149108886719f, 47.91114807128906f),
			new Position(-1352.0936279296875f,-128.61785888671875f, 50.11932373046875f),
			new Position(-1004.4727172851562f,-308.0685119628906f, 38.59397506713867f),
			new Position(-881.775146484375f,-439.23944091796875f, 39.59988021850586f),
			new Position(-654.989501953125f,-414.27459716796875f, 35.46840286254883f),
			new Position(-763.5548706054688f,-753.435791015625f, 27.868600845336914f),
			new Position(-723.2784423828125f,-855.2688598632812f, 23.00007438659668f),
			new Position(-728.48486328125f,-879.8848266601562f, 22.710912704467773f),
			new Position(-766.3800048828125f,-916.9979858398438f, 21.294954299926758f),
			new Position(-812.6202392578125f,-980.1105346679688f, 14.272429466247559f),
			new Position(-831.5071411132812f,-862.6524047851562f, 20.689666748046875f),
			new Position(-741.3700561523438f,-982.2064819335938f, 17.438560485839844f),
			new Position(-668.0057373046875f,-971.435791015625f, 22.34084129333496f),
			new Position(-699.1883544921875f,-1032.1417236328125f, 16.419015884399414f),
			new Position(-712.1751098632812f,-1028.5399169921875f, 16.418920516967773f),
			new Position(-666.8862915039062f,-1104.3663330078125f, 14.651654243469238f),
			new Position(-895.3531494140625f,-1162.7625732421875f, 5.021313667297363f),
			new Position(-987.0642700195312f,-1199.3726806640625f, 6.047657489776611f),
			new Position(-1011.3754272460938f,-1224.02001953125f, 5.953282356262207f),
			new Position(-1202.2550048828125f,-1308.3671875f, 4.916404724121094f),
			new Position(-1366.1270751953125f,56.764774322509766f, 54.09846115112305f),
			new Position(-1503.1502685546875f,-220.3467559814453f, 51.399803161621094f),
			new Position(-1390.1824951171875f,-330.7039794921875f, 40.698341369628906f),
			new Position(-1153.2159423828125f,-797.7539672851562f, 15.500484466552734f),
			new Position(-1180.7197265625f,-752.9400024414062f, 19.50999641418457f),
			new Position(-1564.1729736328125f,-406.470458984375f, 42.384010314941406f),
			new Position(-1643.06494140625f,-411.6952819824219f, 42.07831573486328f),
			new Position(-1597.6470947265625f,-422.13531494140625f, 41.40530776977539f),
			new Position(-1200.189453125f,-156.7574005126953f, 40.0858154296875f),
			new Position(-1197.164794921875f,-258.8152770996094f, 37.76252746582031f),
			new Position(-927.53759765625f,-949.5904541015625f, 2.7451064586639404f),
			new Position(-948.194580078125f,-910.5018920898438f, 2.7453672885894775f),
			new Position(-869.68212890625f,-1103.503173828125f, 6.445570468902588f),
			new Position(-1256.3265380859375f,-1330.891357421875f, 4.080746173858643f),
			new Position(-1135.109130859375f,-1468.3836669921875f, 4.622586727142334f)
		};

		public override void StartJobForPlayer(IPlayer player)
		{
			base.StartJobForPlayer(player);
			player.SendSuccess($"Arbeit als {Name} gestartet!");
			player.SendInformation("Sprich mit dem Chef am Fenster!");
			player.Emit("JobPizza:StartJob");

			var veh = CreateJobVehicle(player, VehicleModel.Faggio, vehSpawnPoint, vehSpawnRot);

			Pizza = new Pizza
			{
				MaxCapacity = 1,
				Capacity = 0,
				IntakeCol = (Colshape.Colshape)Alt.CreateColShapeSphere(intakeSpot, 1f)
			};

			Pizza.IntakeCol.OnColShapeEnter += OnIntakeMarkerHit;
		}

		public void CreateRandomDelivery(IPlayer player)
		{
			Pizza.Capacity++;

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
				!player.GetCharacter().IsJobActive() || player.IsInVehicle || !(Pizza.Capacity == 0)) return;

			var interactionData = new InteractionData
			{
				SourceObject = new PizzaDelivery(JobId),
				CallBack = null
			};

			player.GetCharacter().ShowInteraction("Auftrag starten", "JobPizza:StartMission", interactionData: interactionData);
		}

		public void OnDeliveryMarkerHit(Colshape.Colshape col, IEntity entity)
		{
			if (!(entity is IPlayer player)) return;
			if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
				!player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

			player.SetSyncedMetaData("JobPizza:PlacePizza", true);
			player.PlayAnimation("amb@medic@standing@kneel@idle_a", "idle_b",
				(int)AnimationFlags.Loop);

			Task.Delay(2000).ContinueWith(_ => {
				Pizza.Capacity--;
				player.SendSuccess("Pizza ausgeliefert!");
				player.SendInformation("Fahre nun zurück zur Pizzeria!");
				Pizza.DeliveryBlip.RemoveVisibleEntity(player.Id);
				Pizza.DeliveryCol.Remove();
				player.StopAnimation();
				Pizza.Pay += Pizza.PayPerPizza;
			});
		}

		public override void StopJob(IPlayer player)
		{
			base.StopJob(player);
			player.SendInformation($"Arbeit als {Name} beendet!");
			if (!IsJobLeader(player)) return;
			DestroyJobVehicle(player);
			player.GetCharacter().GiveMoney(Pizza.Pay, "This Pizza Lohn");
			player.SendSuccess($"Du hast {Pizza.Pay} erhalten!");
		}
	}
}
