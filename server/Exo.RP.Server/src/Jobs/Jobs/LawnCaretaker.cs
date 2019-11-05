using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using models.Enums;
using server.Players;
using server.Util.Log;

namespace server.Jobs.Jobs
{
    internal class LawnCaretaker : Job
    {
        private static readonly Logger<LawnCaretaker> Logger = new Logger<LawnCaretaker>();

        private readonly Position[] _emptyPoints =
        {
            new Position(-1350.036f, 140.7969f, 56.26493f)
        };

        private readonly Position[] _waypoints =
        {
            new Position(-1335.306f, 120.917f, 56.66932f),
            new Position(-1328.986f, 131.1318f, 57.05902f),
            new Position(-1331.685f, 142.3194f, 57.45047f)
        };

        private int _currentWaypoint;
        private bool _emptying;

        private ColShape _markerColShape;
        private LawnMower _mower;

        public LawnCaretaker(int jobId) : base(jobId)
        {
            Name = "Rasenpfleger";
            Description = "Mähe den Rasen!";
            PedPosition = new Position(-1352.778f, 125.5773f, 56.23866f);
            Init();
        }

        public override void StartJobForPlayer(IPlayer player)
        {
            base.StartJobForPlayer(player);
            player.SendInformation(Name + "-Job gestartet!");
            _mower = new LawnMower
            {
                LawnMowerObject = false,
                    //NAPI.Object.CreateObject((int) Objects.LawnMover, player.Position, player.Rotation, dimension: 0),
                Capacity = 0,
                MaxCapacity = 10, // TODO: MaxCapacity should depend on Job Level
                Rtb = 0
            };
            //player.AttachObject(_mower.LawnMowerObject, 6286, new Position(0, 1.05, -0.963), new Position(0, 0, 180), fixedRot:true);
            player.PlayAnimation("veh@boat@speed@fps@", "sit_slow",
                (int) (AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl));
            player.SetData("LawnMower", _mower);
            CreateMarker(player, (Position) _waypoints.GetValue(_currentWaypoint));
        }

        public void OnMarkerColEnter(ColShape shape, IPlayer player)
        {
            if (player.GetCharacter() != null && player.GetCharacter().GetJob() == this &&
                player.GetCharacter().IsJobActive())
            {
                /// Successfull job
                if (_emptying)
                {
                    _mower.DoRtb();
                    _emptying = false;
                    player.SendInformation("You successfully emptied your lawn mower!");
                }
                else
                {
                    _mower.Capacity++;
                }

                _currentWaypoint++;

                if (_currentWaypoint >= _waypoints.Length) _currentWaypoint = 0;
                if (_mower.Capacity + 1 <= _mower.MaxCapacity)
                    CreateMarker(player, (Position) _waypoints.GetValue(_currentWaypoint));
                if (_mower.Capacity >= _mower.MaxCapacity)
                {
                    CreateMarker(player, (Position) _emptyPoints.GetValue(0));
                    _emptying = true;
                    if (_emptying)
                        Logger.Debug("emptying = true");
                    else
                        Logger.Debug("emptying = false");
                    player.SendInformation("Your Lawn Mower is full. Please empty it!");
                }
                else
                {
                    player.SendInformation("Lawn Mower capacity: " + _mower.Capacity);
                }
            }
        }

        public override void StopJob(IPlayer player)
        {
            base.StopJob(player);
            player.StopAnimation();
            _mower.Destroy();
            DeleteMarker(player);

            _emptying = false;
            _currentWaypoint = 0;
            _markerColShape = null;

            player.SendInformation(Name + "-Job beendet!");
        }

        private void CreateMarker(IPlayer player, Position waypoint)
        {
            try
            {
              //  _markerColShape.Delete();
            }
            catch
            {
                //
            }

            player.Emit("JobLawn:SetWaypoint", waypoint.X, waypoint.Y, waypoint.Z);
           // _markerColShape = NAPI.ColShape.CreateSphereColShape(waypoint, 2, 0);
            //_markerColShape.OnEntityEnterColShape += OnMarkerColEnter;
        }

        private void DeleteMarker(IPlayer player)
        {
            player.Emit("JobLawn:DelWaypoint");
            try
            {
             //   _markerColShape.Delete();
            }
            catch
            {
                //
            }
        }
    }
}