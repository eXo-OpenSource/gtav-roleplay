using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using models.Enums;
using server.Util.Log;
using IPlayer = server.Players.IPlayer;

namespace server.Jobs.Jobs
{
    internal class LawnCaretaker : Job
    {
        private static readonly Logger<LawnCaretaker> Logger = new Logger<LawnCaretaker>();

        private readonly Position[] EmptyPoints =
        {
            new Position(-1350.036f, 140.7969f, 56.26493f)
        };

        private readonly Position[] WayPoints =
        {
            new Position(-1335.306f, 120.917f, 56.66932f),
            new Position(-1328.986f, 131.1318f, 57.05902f),
            new Position(-1331.685f, 142.3194f, 57.45047f)
        };

        private int CurrentWayPoint;
        private bool Emptying;

        private LawnMower Mower;

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
                MaxCapacity = 10, // TODO: MaxCapacity should depend on Job Level
                Rtb = 0
            };

            player.PlayAnimation("veh@boat@speed@fps@", "sit_slow",
                (int) (AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl));

            player.SetData("LawnMower", Mower);
            CreateMarker(player, (Position) WayPoints.GetValue(CurrentWayPoint));
        }

        public void OnMarkerColEnter(IPlayer player)
        {
            if (player.GetCharacter() != null && player.GetCharacter().GetJob() == this &&
				player.GetCharacter().IsJobActive())
            {
                /// Successfull job
                if (Emptying)
                {
                    Mower.DoRtb();
                    Emptying = false;
					player.SendSuccess("Du hast erfolgreich dein Rasenmäher entleert!");
                }
                else
                {
                    Mower.Capacity++;
				}

				DeleteMarker(player);

				CurrentWayPoint++;
                if (CurrentWayPoint >= WayPoints.Length) CurrentWayPoint = 0;
                if (Mower.Capacity + 1 <= Mower.MaxCapacity)
                    CreateMarker(player, (Position) WayPoints.GetValue(CurrentWayPoint));
					player.SendInformation("Nächster Marker wurde erstellt!");

				if (Mower.Capacity >= Mower.MaxCapacity)
                {
					CreateMarker(player, (Position) EmptyPoints.GetValue(0));
                    Emptying = true;
                    if (Emptying)
                        Logger.Debug("emptying = true");
                    else
                        Logger.Debug("emptying = false");

					player.SendError("Dein Rasenmäher ist voll! Geh ihn leeren.");
                }
                else
                {
					player.SendInformation("Rasenmäher-Kapazität: " + Mower.Capacity);
                }
            }
        }

        public override void StopJob(IPlayer player)
        {
            base.StopJob(player);
            player.StopAnimation();
            Mower.Destroy();
            DeleteMarker(player);

            Emptying = false;
            CurrentWayPoint = 0;

			player.Emit("JobLawn:StobJob");
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
