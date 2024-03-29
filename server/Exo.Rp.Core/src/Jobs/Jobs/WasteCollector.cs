using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Extensions;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Streamer.Private;
using Exo.Rp.Models.Enums;
using Exo.Rp.Models.Jobs;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using WorldObject = Exo.Rp.Core.World.WorldObject;

namespace Exo.Rp.Core.Jobs.Jobs
{
    internal class WasteCollector : Job
    {
        private readonly Position _trashSpawnPos;
        private readonly float _trashSpawnRot;
        private Dictionary<int, Position[]> _binPositions;
        private Dictionary<int, WasteBin> _bins;
        private Dictionary<IVehicle, int> _vehicleBins;

        public WasteCollector(int jobId) : base(jobId)
        {
            Name = "Müllabfuhr";
            SpriteId = 318;
            Description = "Fahre mit dem Müllwagen durch Los Santos und sammle Mülltonnen!";
            PedPosition = new Position(-570.5051f, 310.7722f, 84.57085f);
            MaxPlayers = 4;
            _trashSpawnPos = new Position(-563.1276f, 315.7751f, 84.12386f);
            _trashSpawnRot = 0f;
            LoadWasteBins();

            _vehicleBins = new Dictionary<IVehicle, int>();

            JobUpgrades.Add(new JobUpgradeCategoryDto
            {
                Id = 1,
                Name = "LKW-Abfallbehälter",
                Description = "Nehme mehr Mülltonnen in deinem LKW auf",
                Upgrades = new List<JobUpgradeDto>
                {
                    new JobUpgradeDto {Id = 0, Points = 0, Text = "5 Tonnen", Value = 10},
                    new JobUpgradeDto {Id = 1, Points = 50, Text = "10 Tonnen", Value = 10},
                    new JobUpgradeDto {Id = 2, Points = 100, Text = "15 Tonnen", Value = 15},
                    new JobUpgradeDto {Id = 3, Points = 150, Text = "20 Tonnen", Value = 20},
                    new JobUpgradeDto {Id = 4, Points = 200, Text = "25 Tonnen", Value = 25},
                    new JobUpgradeDto {Id = 5, Points = 300, Text = "30 Tonnen", Value = 30}
                }
            });

            JobUpgrades.Add(new JobUpgradeCategoryDto
            {
                Id = 2,
                Name = "Container-Sichtweite",
                Description = "Reichweite der auf der Karte angezeigten Mülltonnen",
                Upgrades = new List<JobUpgradeDto>
                {
                    new JobUpgradeDto {Id = 0, Points = 0, Text = "50 Meter", Value = 50},
                    new JobUpgradeDto {Id = 1, Points = 50, Text = "70 Meter", Value = 70},
                    new JobUpgradeDto {Id = 2, Points = 100, Text = "100 Meter", Value = 100},
                    new JobUpgradeDto {Id = 3, Points = 150, Text = "120 Meter", Value = 120},
                    new JobUpgradeDto {Id = 4, Points = 200, Text = "150 Meter", Value = 150},
                    new JobUpgradeDto {Id = 5, Points = 300, Text = "200 Meter", Value = 200}
                }
            });

            Init();
        }

        public override void StartJobForPlayer(IPlayer player)
        {
            // NAPI.Chat.SendChatMessageToAll("StartJobForPlayer " + player.Name);

            base.StartJobForPlayer(player);
            player.SendInformation(Name + "-Job gestartet!");

            if (!IsJobLeader(player)) return;
            var veh = CreateJobVehicle(player, VehicleModel.Trash, _trashSpawnPos, _trashSpawnRot);
            _vehicleBins.Add(veh.handle, 0);

            veh.handle.SetData("MaxBins", GetJobUpgradeValue(player, 1));

            TriggerEventForJobTeam(player, "JobTrash:SetVehicle", veh.handle.Id, GetJobUpgradeValue(player, 1));
            TriggerEventForJobTeam(player, "Progress:Text", $"Müllwagen-Füllstand - max. {GetJobUpgradeValue(player, 1)}");
            TriggerEventForJobTeam(player, "Progress:Set", 0);
            TriggerEventForJobTeam(player, "Progress:Active", true);
            if (GetJobTeam(player) == null) return;
            if (_bins == null) LoadWasteBins(); // Only for debug. Bins should load always later

            foreach (var jobPlayer in GetJobTeam(player))
            {
                var coopPlayer = Core.GetService<PlayerManager>().GetClient(jobPlayer.Key);

                foreach (var bin in _bins.Values)
                {
                    bin.Blip.AddVisibleEntity(coopPlayer.Id);
                }
                //coopPlayer.Emit("JobTrash:UpdateBlips", JsonConvert.SerializeObject(GetFullWastebinPositions()),
                //    GetJobUpgradeValue(player, 2));
            }
        }

        public override void StopJob(IPlayer player)
        {
            base.StopJob(player);
            player.SendInformation(Name + "-Job beendet!");
            player.Emit("Progress:Active", false);
            foreach (var bin in _bins.Values)
            {
                bin.Blip.RemoveVisibleEntity(player.Id);
            }
            if (!IsJobLeader(player)) return;
            DestroyJobVehicle(player);
        }

        public void OnVehicleMarkerHit(IPlayer player, IVehicle vehicle)
        {
            if (player.GetData("WasteBin", out WasteBin bin))
            {
                if (bin.BinObject != null)
                {
                    bin.Destroy();
                    player.DeleteData("WasteBin");
                    player.StopAnimation();
                    _vehicleBins[vehicle] = _vehicleBins[vehicle] + 1;
                    player.SendSuccess($"Sack #{_vehicleBins[vehicle]} eingeladen!");

                    vehicle.GetData("MaxBins", out int maxBins);
                    var percent = Math.Round(_vehicleBins[vehicle] / (float)maxBins, 2);
                    TriggerEventForJobTeam(player, "Progress:Set", (float)percent);
                    GivePlayerUpgradePoints(player, 1);
                    return;
                }
            }

            //player.SendError("Hole erst einen Müllcontainer!");
        }

        #region WasteBins

        public void LoadWasteBins()
        {
            _binPositions = new Dictionary<int, Position[]>();
            _bins = new Dictionary<int, WasteBin>();

            var worldObjects = Core.GetService<DatabaseContext>().WorldObjectsModels.Local.Where(x => x.Type == WorldObjects.WasteBin);
            foreach (var model in worldObjects)
                _binPositions.Add(model.Id,
                    new[] { model.Position.DeserializeVector(), model.Rotation.DeserializeVector() });

            foreach (var pos in _binPositions) CreateWasteBin(pos.Key, pos.Value[0], pos.Value[1]);
        }

        public void AddWastebin(IPlayer player, Position pos, Position rot)
        {
            var nModel = new WorldObject()
            {
                Position = pos.Serialize(),
                Rotation = rot.Serialize(),
                Type = WorldObjects.WasteBin,
                PlacedBy = player.GetId(),
                Date = DateTime.Now
            };
            var factory = Core.GetService<DatabaseContext>().WorldObjectsModels.Local;
            factory.Add(nModel);

            foreach (var model in factory.OrderByDescending(o => o.Id))
            {
                CreateWasteBin(model.Id+1, pos, rot);
                player.SendSuccess($"Muelltonne mit ID {model.Id + 1} hinzugefuegt!");
                return;
            }

            player.SendError("Muelltonne konnte nicht gespeichert werden!");
        }


        public void CreateWasteBin(int id, Position pos, Position rot)
        {
            var nBin = new WasteBin
            {
                BinObject = new StreamObject(new Position(pos.X, pos.Y, pos.Z), 0, 520){Model = Alt.Hash("hei_prop_heist_binbag")},
                Col = (Colshape.Colshape) Alt.CreateColShapeSphere(pos, 1.9f),
                Full = true,
                Blip = new PrivateBlip( pos, 0, 300){Sprite = 364, Name = "Mülltonne"}
            };
            Core.GetService<PrivateStreamer>().AddEntity(nBin.Blip);
            Core.GetService<PublicStreamer>().AddObject(nBin.BinObject);

            nBin.Col.SetData("WasteBin", nBin);
            nBin.Col.OnColShapeEnter += OnBinColEnter;
            _bins.Add(id, nBin);
        }

        public void OnBinColEnter(Colshape.Colshape colshape, IEntity entity)
        {
            if(!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
                !player.GetCharacter().IsJobActive() || player.IsInVehicle) return;
            if (player.HasData("WasteBin"))
            {
                player.SendError("Du hast bereits einen Muellsack dabei!");
                return;
            }

            player.StartScenario("PROP_HUMAN_BUM_BIN");
            colshape.GetData("WasteBin", out WasteBin binData);
            binData.Col.Remove();

            Task.Delay(3000).ContinueWith(_ =>
            {
                player.StopAnimation();
                //binData.BinObject.SetData("attachToEntity", entity.Id);
                binData.BinObject.AttachToBone(new AttachOptions
                {
                    Entity = entity.Id,
                    BoneID = 57005,
                    X = 0.12f,
                    Y = 0,
                    Z = 0,
                    XRot = 25,
                    YRot = 270,
                    ZRot = 180,
                    VertexIndex = 1
                });
                Core.GetService<PrivateStreamer>().RemoveEntity(binData.Blip);
                //player.AttachObject(binData.BinObject, 6286, new Position(0f, 0.9f, -0.9f), new Position(0, 0, 180));

                player.SetData("WasteBin", binData);
                //player.SendInformation("WasteColHit");
                player.PlayAnimation("anim@heists@narcotics@trash", "walk",
                    (int) (AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl));
            });
        }

        public Dictionary<int, string> GetFullWastebinPositions()
        {
            var binData = new Dictionary<int, string>();

            foreach (var bin in _bins)
            {
                if (bin.Value.Full == false) continue;
                var pos = new Position(bin.Value.BinObject.Position.X, bin.Value.BinObject.Position.Y,
                    bin.Value.BinObject.Position.Z).Serialize();
                binData.Add(bin.Key, pos);
            }

            return binData;
        }

        #endregion
    }
}