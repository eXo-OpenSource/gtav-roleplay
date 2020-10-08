using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.Vehicles.Types;
using Exo.Rp.Models.Jobs;
using Exo.Rp.Models.Popup;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;
using Newtonsoft.Json;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using Vehicle = Exo.Rp.Core.Vehicles.Vehicle;

namespace Exo.Rp.Core.Jobs
{
    public class Job
    {
        private static readonly ILogger<Job> Logger = new Logger<Job>();

        public string Description;
        public int JobId;
        public Dictionary<IPlayer, Dictionary<int, string>> JobPlayers;
        public List<JobUpgradeCategoryDto> JobUpgrades;
        public Dictionary<IPlayer, TemporaryVehicle> JobVehicles;
        public int MaxPlayers = 1;
        public int SpriteId;
        public string Name;
        public Position PedPosition;

        private string InteractionId;

        public Job(int jobId)
        {
            JobId = jobId;
            JobVehicles = new Dictionary<IPlayer, TemporaryVehicle>();
            JobPlayers = new Dictionary<IPlayer, Dictionary<int, string>>();
            JobUpgrades = new List<JobUpgradeCategoryDto>();
        }

        public void Init()
        {
            Core.GetService<PublicStreamer>().AddGlobalBlip(new StaticBlip
            {
                Color = 4,
                Name = Name,
                X = PedPosition.X,
                Y = PedPosition.Y,
                Z = PedPosition.Z,
                SpriteId = SpriteId,
            });

            var col = (Colshape.Colshape) Alt.CreateColShapeSphere(PedPosition, 3);
            col.OnColShapeEnter += OnColEnter;
            col.OnColShapeExit += OnColExit;
        }

        public void OnColEnter(Colshape.Colshape colshape, IEntity entity)
        {
            if(!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            InteractionId = player.GetCharacter()
                .ShowInteraction("Job: " + Name, "onJobPedInteraction", interactionData: interactionData);
        }

        public void OnColExit(Colshape.Colshape colshape, IEntity entity)
        {
            if(!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            player.GetCharacter().HideInteraction(InteractionId);
        }


        public void ShowJobMenu(IPlayer player, string subMenu = null)
        {
            var players = new Dictionary<int, string>(){};
            var leaderId = 0;

            if (player.GetCharacter().GetJob() == this && player.GetCharacter().IsJobActive())
            {
                var leader = GetJobLeader(player);
                leaderId = leader.GetId();
                players = JobPlayers[leader];
            }


            /*var data = new JobMenuDataDto
            {
                CurrentJobId = player.GetCharacter().GetJob()?.JobId ?? 0,
                CurrentJobName = player.GetCharacter().GetJob()?.Name ?? "-keiner-",
                Id = JobId,
                Name = Name,
                Description = Description,
                MaxPlayers = MaxPlayers,
                Players = players,
                LeaderId = leaderId,
                Upgrades = JobUpgrades,
                JobPoints = GetPlayerUpgradePoints(player),
                PlayerUpgrades = GetPlayerUpgrades(player)
            };*/
            var data = new PopupMenuDto
            {
                Title = "Job",
                Items = new List<PopupItemDto>
                {
                    new PopupColLabelDto
                    {
                        LeftText = "Momentaner Job",
                        RightText = player.GetCharacter().GetJob()?.Name ?? "-keiner-"
                    },
                    player.GetCharacter().GetJob() != null ? new PopupButtonDto
                    {
                        Name = $"{Name}-Job kündigen",
                        Callback = "Job:DeclineJob",
                        CallbackArgs = new List<object>{JobId},
                        Color = "red"
                    } : null,
                    new PopupLabelDto
                    {
                        Name = ""
                    },
                    new PopupHeaderDto
                    {
                        LeftText = Name,
                        RightText = $"bis zu {MaxPlayers} Spieler"
                    }
                }
            };
            if (JobId == player.GetCharacter().GetJob()?.JobId)
            {
                if (!player.GetCharacter().IsJobActive())
                {
                    data.Items.Add(new PopupButtonDto
                    {
                        Name = "Alleine Arbeiten",
                        Callback = "Job:StartJobSingle",
                        CallbackArgs = new List<object> {JobId}
                    });

                    if (MaxPlayers > 1)
                    {
                        data.Items.Add(new PopupButtonDto
                        {
                            Name = "Team zusammenstellen",
                            Callback = "Job:OpenCoop",
                            CallbackArgs = new List<object>{JobId}
                        });
                    }
                }
                else
                {
                    data.Items.Add(new PopupButtonDto
                    {
                        Name = "Arbeit beenden",
                        Color = "red",
                        Callback = "Job:StopJob",
                        CallbackArgs = new List<object>{JobId}
                    });
                }
            } else if (player.GetCharacter().GetJob() == null)
            {
                data.Items.Add(new PopupButtonDto
                {
                    Name = "Job annehmen",
                    Callback = "Job:AcceptJob",
                    CallbackArgs = new List<object>{JobId}
                });
            }

            player.Emit("Popup:Show", data);

            player.Emit("outputIPlayerConsole", JsonConvert.SerializeObject(data));
        }


        public virtual void StartJobForPlayer(IPlayer player)
        {
            player.Emit("Job:StartJob", JobId);
            player.GetCharacter().SetJobActive(true);
        }

        public void AddPlayerToJob(IPlayer player, IPlayer leader, bool multiplayer)
        {
            if (!JobPlayers.ContainsKey(leader))
            {
                JobPlayers.Add(leader, new Dictionary<int, string>());
                AddPlayerToJob(leader, leader, multiplayer);
                if(multiplayer) AddPlayerToJob(player, leader, true);
            }

            if (JobPlayers[leader].ContainsKey(player.GetAccount().Id)) return;

            JobPlayers[leader].Add(player.GetAccount().Id, player.Name);

            if (multiplayer)
                player.SendInformation("Du wurdest zum " + Name + "-Team von " +
                                        leader.GetCharacter().GetNormalizedName() + " hinzugefügt!");
        }


        public virtual void RemovePlayerFromJob(IPlayer player)
        {
            player.SetSyncedMetaData("Job:Active", false);
            player.Emit("Job:StopJob", JobId);
        }

        public virtual void StopJob(IPlayer player)
        {
            RemovePlayerFromJob(player);
            if (!JobPlayers.ContainsKey(player)) return;
            if (JobPlayers[player].Count <= 0) return;
            foreach (var jobPlayer in JobPlayers[player])
            {
                var coopPlayer = Core.GetService<PlayerManager>().GetClient(jobPlayer.Key);
                RemovePlayerFromJob(coopPlayer);
                coopPlayer.SendInformation("Du wurdest aus dem " + Name + "-Team von " +
                                            player.GetCharacter().GetNormalizedName() + " entfernt!");
            }
        }

        public virtual Vehicle CreateJobVehicle(IPlayer player, VehicleModel vehModel, Position pos, float rot)
        {
            var veh = Core.GetService<VehicleManager>().CreateTemporaryVehicle(vehModel, pos, rot, Rgba.Zero, Rgba.Zero);
            //player.SetIntoVehicle(veh.handle, -1);
            JobVehicles.Add(player, veh);
            player.Emit("disableVehicleCollission", veh.handle, 10000);
            return veh;
        }

        public virtual void DestroyJobVehicle(IPlayer player)
        {
            if (JobVehicles.ContainsKey(player))
            {
                JobVehicles[player].handle.Remove();
                JobVehicles.Remove(player);
            }
        }

        #region JobUpgrades

        public bool SetJobUpgrade(IPlayer player, int categoryId, int upgradeId)
        {
            if (player.GetCharacter() == null) return false;
            var jobData = player.GetCharacter().JobData;
            Logger.Debug("Start Upgrade!");
            if (jobData.Upgrades == null) jobData.Upgrades = new Dictionary<int, Dictionary<int, int>>();

            if (!jobData.Upgrades.ContainsKey(JobId))
            {
                jobData.Upgrades.Add(JobId, new Dictionary<int, int>());
                Logger.Debug("Added Category!");
            }

            if (jobData.Upgrades[JobId].ContainsKey(categoryId))
            {
                jobData.Upgrades[JobId][categoryId] = upgradeId;
                player.GetCharacter().Save();
                Logger.Debug("Set UpgradeId to Category!");
                return true;
            }

            jobData.Upgrades[JobId].Add(categoryId, upgradeId);
            Logger.Debug("Added Category and UpgradeId!");
            player.GetCharacter().Save();
            return true;
        }

        public Dictionary<int, int> GetPlayerUpgrades(IPlayer player)
        {
            if (player.GetCharacter() == null) return new Dictionary<int, int>();
            var jobData = player.GetCharacter().JobData;
            return jobData != null && jobData.Upgrades.ContainsKey(JobId)
                ? jobData.Upgrades[JobId]
                : new Dictionary<int, int>();
        }

        public int GetJobUpgradeId(IPlayer player, int categoryId)
        {
            if (player.GetCharacter() == null) return 0;
            var jobData = player.GetCharacter().JobData;
            if (jobData != null && jobData.Upgrades.ContainsKey(JobId))
            {
                if (jobData.Upgrades[JobId].ContainsKey(categoryId))
                    return jobData.Upgrades[JobId][categoryId];
            }
            else if(jobData != null)
            {
                var startUpgrades = new Dictionary<int, int>();
                foreach (var category in JobUpgrades)
                {
                    startUpgrades.Add(category.Id, 0);
                }
                jobData.Upgrades.Add(JobId, startUpgrades);
                player.GetCharacter().JobData = jobData;
                return GetJobUpgradeId(player, categoryId);
            }

            Logger.Debug("UpgradeId not Found!");

            return 0;
        }

        public int GetJobUpgradePoints(IPlayer player, int categoryId, int upgradeId)
        {
            foreach (var category in JobUpgrades)
            {
                if (category.Id != categoryId) continue;
                foreach (var upgrade in category.Upgrades)
                    if (upgrade.Id == upgradeId)
                        return upgrade.Points;
            }

            Logger.Debug("UpgradePoints not Found!");

            return 0;
        }

        public int GetPlayerUpgradePoints(IPlayer player)
        {
            if (player.GetCharacter() == null) return 0;
            var jobData = player.GetCharacter().JobData;
            if (jobData != null && jobData.Points != null && jobData.Points.ContainsKey(JobId))
                return jobData.Points[JobId];

            return 0;
        }

        public void GivePlayerUpgradePoints(IPlayer player, int points)
        {
            if (player.GetCharacter() == null) return;
            var jobData = player.GetCharacter().JobData ?? new CharacterJobData();

            if (jobData.Points == null) jobData.Points = new Dictionary<int, int>();

            if (jobData.Points.ContainsKey(JobId))
                jobData.Points[JobId] = jobData.Points[JobId] + points;
            else
                jobData.Points.Add(JobId, points);

            player.GetCharacter().JobData = jobData;
            player.SendSuccess("Du hast " + points + " " + Name + "-Punkt/e erhalten!");
        }

        public int GetJobUpgradeValue(IPlayer player, int categoryId)
        {
            if (player.GetCharacter() == null) return 0;
            var upgradeId = GetJobUpgradeId(player, categoryId);
            foreach (var category in JobUpgrades)
            {
                if (category.Id != categoryId) continue;
                foreach (var upgrade in category.Upgrades)
                    if (upgrade.Id == upgradeId)
                        return upgrade.Value;

                return category.Upgrades.First().Value;
            }

            return 0;
        }

        #endregion

        #region Multiplayer

        public bool IsJobLeader(IPlayer player)
        {
            return JobPlayers.ContainsKey(player);
        }

        public IPlayer GetJobLeader(IPlayer player)
        {
            var sourceId = player.GetId();
            foreach (var jobTeam in JobPlayers)
                foreach (var jobPlayer in jobTeam.Value)
                    if (sourceId == jobPlayer.Key)
                        return jobTeam.Key;

            return null;
        }

        public void TriggerEventForJobTeam(IPlayer player, string eventName, params object[] data)
        {
            var leader = GetJobLeader(player);
            if (leader == null || !JobPlayers.ContainsKey(leader)) return;
            foreach (var jobPlayer in JobPlayers[leader])
            {
                var coopPlayer = Core.GetService<PlayerManager>().GetClient(jobPlayer.Key);
                coopPlayer?.Emit(eventName, data);
            }
        }


        public Dictionary<int, string> GetJobTeam(IPlayer player)
        {
            var leader = GetJobLeader(player);
            if (leader == null || !JobPlayers.ContainsKey(leader)) return null;
            return JobPlayers[leader];
        }

        public void StartJob(IPlayer leader)
        {
            if (!JobPlayers.ContainsKey(leader)) return;
            StartJobForPlayer(leader); // Start Job for Leader at first
            if (JobPlayers[leader].Count > 1)
                foreach (var jobPlayer in JobPlayers[leader])
                    if (jobPlayer.Key != leader.GetAccount().Id)
                        StartJobForPlayer(Core.GetService<PlayerManager>().GetClient(jobPlayer.Key));
        }

        #endregion
    }
}