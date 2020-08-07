using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using models.Enums;
using server.Util.Log;
using System;
using IPlayer = server.Players.IPlayer;

namespace server.Jobs.Jobs
{
	internal class LawnCaretaker : Job
	{
		private static readonly Logger<LawnCaretaker> Logger = new Logger<LawnCaretaker>();

		private readonly Position[] emptyPoints =
		{
			new Position(-1350.036f, 140.7969f, 56.26493f)
		};

		private readonly Position[] wayPoints =
		{
			new Position(-1339.97900390625f, 128.44232177734375f, 56.586299896240234f),
			new Position(-1342.1256103515625f, 120.63739776611328f, 56.333518981933594f),
			new Position(-1341.8896484375f, 112.47796630859375f, 56.17890930175781f),
			new Position(-1336.6939697265625f, 111.00300598144531f, 56.375389099121094f),
			new Position(-1337.2674560546875f, 117.20549774169922f, 56.42686462402344f),
			new Position(-1337.5408935546875f, 123.6284408569336f, 56.68465042114258f),
			new Position(-1337.6915283203125f, 130.25852966308594f, 56.74169158935547f),
			new Position(-1337.827392578125f, 136.88717651367188f, 57.060882568359375f),
			new Position(-1337.9866943359375f, 143.25820922851562f, 57.13908386230469f),
			new Position(-1331.5291748046875f, 146.17196655273438f, 57.63683319091797f),
			new Position(-1330.45947265625f, 137.3451690673828f, 57.28607177734375f),
			new Position(-1330.3765869140625f, 127.48796844482422f, 57.012516021728516f),
			new Position(-1330.12939453125f, 118.95426177978516f, 56.81400680541992f),
			new Position(-1329.9169921875f, 113.57298278808594f, 56.65199279785156f),
			new Position(-1325.1444091796875f, 112.74716186523438f, 56.7157096862793f),
			new Position(-1325.005859375f, 119.9388198852539f, 56.82463073730469f),
			new Position(-1325.905029296875f, 128.69366455078125f, 56.98401641845703f),
			new Position(-1326.5360107421875f, 134.97293090820312f, 57.38465881347656f),
			new Position(-1327.139404296875f, 141.09716796875f, 57.67431640625f),
			new Position(-1322.2615966796875f, 143.50210571289062f, 57.89585876464844f),
			new Position(-1320.5179443359375f, 135.3581085205078f, 57.632755279541016f),
			new Position(-1320.04150390625f, 126.99481201171875f, 57.222198486328125f),
			new Position(-1319.610107421875f, 116.57067108154297f, 56.67833709716797f),
			new Position(-1312.3017578125f, 112.4655990600586f, 56.78456497192383f),
			new Position(-1306.4449462890625f, 112.8510971069336f, 56.68569564819336f),
			new Position(-1306.155029296875f, 121.02104949951172f, 57.190635681152344f),
			new Position(-1306.155029296875f, 125.72126770019531f, 57.43873596191406f),
			new Position(-1306.159423828125f, 136.42059326171875f, 58.464839935302734f),
			new Position(-1298.9354248046875f, 139.89393615722656f, 58.37882995605469f),
			new Position(-1297.3616943359375f, 131.94784545898438f, 57.68803787231445f),
			new Position(-1297.494384765625f, 123.16761779785156f, 57.1310920715332f),
			new Position(-1297.74609375f, 115.4525146484375f, 56.641868591308594f)
		};

		private int currentWayPoint;
		private bool emptying;

		private LawnMower Mower;

		private Random rndMoney = new Random();

		public LawnCaretaker(int jobId) : base(jobId)
		{
			Name = "Rasenpfleger";
			Description = "Mähe den Rasen!";
			PedPosition = new Position(-1352.778f, 125.5773f, 56.23866f);
			Init();
		}

		public override void StartJobForPlayer(IPlayer player)
		{
			player.Emit("JobLawn:StartJob");
			player.SetSyncedMetaData("lawnJob.syncMower", "attach");
			base.StartJobForPlayer(player);
			player.SendInformation(Name + "-Job gestartet!");

			Mower = new LawnMower
			{
				LawnMowerObject = false,
				Capacity = 0,
				MaxCapacity = 5, // Should depend on job-level | 5 in debug - change later to 25
				Rtb = 0
			};

			player.PlayAnimation("veh@boat@speed@fps@", "sit_slow",
				(int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl));

			player.SetData("LawnMower", Mower);
			CreateMarker(player, (Position) wayPoints.GetValue(currentWayPoint));
		}

		public void OnMarkerColEnter(IPlayer player)
		{
			if (player.GetCharacter() != null && player.GetCharacter().GetJob() == this && player.GetCharacter().IsJobActive())
			{
				if (emptying)
				{
					Mower.DoRtb();
					emptying = false;
					int randomMoney = rndMoney.Next(200, 300);
					player.SendSuccess("Du hast erfolgreich dein Rasenmäher entleert!");
					player.SendSuccess($"Du erhältst ${randomMoney}!");
					player.GetCharacter().GiveMoney(rndMoney.Next(200, 300), "Rasenmäherjob", false);
				}
				else
				{
					Mower.Capacity++;
				}

				DeleteMarker(player);

				currentWayPoint++;
				if (currentWayPoint >= wayPoints.Length) currentWayPoint = 0;
				if (Mower.Capacity + 1 <= Mower.MaxCapacity)
					CreateMarker(player, (Position) wayPoints.GetValue(currentWayPoint));

				if (Mower.Capacity >= Mower.MaxCapacity)
				{
					CreateMarker(player, (Position) emptyPoints.GetValue(0));
					emptying = true;

					player.SendError("Dein Rasenmäher ist voll! Geh ihn leeren.");
				}
				else
				{
					player.SendInformation("Du hast den Rasen gemäht!");
				}
			}
		}

		public override void StopJob(IPlayer player)
		{
			base.StopJob(player);
			base.RemovePlayerFromJob(player);
			player.StopAnimation();
			player.SetSyncedMetaData("lawnJob.syncMower", "detach");
			Mower.Destroy();
			DeleteMarker(player);

			emptying = false;
			currentWayPoint = 0;

			player.Emit("JobLawn:StopJob");
			player.SendInformation(Name + "-Job beendet!");
		}

		private void CreateMarker(IPlayer player, Position waypoint)
		{
			player.Emit("JobLawn:SetWaypoint", waypoint.X, waypoint.Y, waypoint.Z);
		}

		private void DeleteMarker(IPlayer player)
		{
			player.Emit("JobLawn:DelWaypoint");
		}
	}
}
