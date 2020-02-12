using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using models.Jobs;
using Newtonsoft.Json;
using server.Players;
using server.Players.Characters;
using server.Util.Log;
using server.Vehicles;
using server.Vehicles.Types;
using IPlayer = server.Players.IPlayer;
using Vehicle = server.Vehicles.Vehicle;

namespace server.Jobs
{
    public class Job
    {
        private static readonly Logger<Job> Logger = new Logger<Job>();

        public string Description;
        public int JobId;
        public Dictionary<IPlayer, Dictionary<int, string>> JobPlayers;
        public List<JobUpgradeCategoryDto> JobUpgrades;
        public Dictionary<IPlayer, TemporaryVehicle> JobVehicles;
        public int MaxPlayers = 1;
        public string Name;
        public Position PedPosition;

        public Job(int jobId)
        {
            JobId = jobId;
            JobVehicles = new Dictionary<IPlayer, TemporaryVehicle>();
            JobPlayers = new Dictionary<IPlayer, Dictionary<int, string>>();
            JobUpgrades = new List<JobUpgradeCategoryDto>();
        }

        public void Init()
        {
            Alt.CreateBlip(BlipType.Cont, PedPosition);

            var col = Alt.CreateColShapeSphere(PedPosition, 3);
            /*col.OnEntityEnterColShape += OnColEnter;
            col.OnEntityExitColShape += OnColExit;*/
        }

        public void OnColEnter(ColShape colshape, IPlayer player)
        {
            if (player.GetCharacter() == null) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            player.GetCharacter()
                .ShowInteraction("Job: " + Name, "onJobPedInteraction", interactionData: interactionData);
        }

        public void OnColExit(ColShape colshape, IPlayer player)
        {
            if (player.GetCharacter() == null) return;

            player.GetCharacter().HideInteraction();
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
            

            var data = new JobMenuDataDto
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
            };
            player.Emit("Job:ShowJobMenu", JsonConvert.SerializeObject(data), subMenu);

            player.Emit("outputIPlayerConsole", JsonConvert.SerializeObject(data));
        }
        

        public virtual void StartJobForPlayer(IPlayer player)
        {
            player.Emit("Job:StartJob", JobId);
            player.SetSyncedMetaData("Job:Active", true);
        }

        public void AddPlayerToJob(IPlayer player, IPlayer leader, bool multiplayer)
        {
            if (!JobPlayers.ContainsKey(leader))
            {
                JobPlayers.Add(player, new Dictionary<int, string>());
                AddPlayerToJob(player, player, multiplayer);
            }

            if (JobPlayers[leader].ContainsKey(player.GetId())) return;

            JobPlayers[leader].Add(player.GetId(), player.Name);

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
                JobVehicles[player] = null;
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
                if (jobData.Upgrades[JobId].ContainsKey(categoryId))
                    return jobData.Upgrades[JobId][categoryId];

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

        public void TriggerEventForJobTeam(IPlayer player, string eventName, object data, object data2 = null,
            object data3 = null)
        {
            var leader = GetJobLeader(player);
            if (leader == null || !JobPlayers.ContainsKey(leader)) return;
            foreach (var jobPlayer in JobPlayers[leader])
            {
                var coopPlayer = Core.GetService<PlayerManager>().GetClient(jobPlayer.Key);
                coopPlayer.Emit(eventName, data, data2, data3);
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
                    if (jobPlayer.Key != leader.GetId())
                        StartJobForPlayer(Core.GetService<PlayerManager>().GetClient(jobPlayer.Key));
        }

        #endregion
    }
}