using System.Collections.Generic;
using System.Diagnostics.Tracing;
using AltV.Net;
using models.Enums;
using models.Popup;
using server.Jobs;
using server.Players;
using server.Players.Characters;
using server.UI;
using server.Util.Log;

namespace server.Events.Jobs
{
    internal class JobEvents : IScript
    {
        private static readonly Logger<JobEvents> Logger = new Logger<JobEvents>();

        [AltV.Net.Event("onJobPedInteraction")]
        public void OnJobPedInteraction(IPlayer player)
        {
            if (player.GetCharacter().GetInteractionData() == null) return;

            var job = (Job) player.GetCharacter().GetInteractionData().SourceObject;
            job.ShowJobMenu(player);
        }

        [AltV.Net.Event("Job:AcceptJob")]
        public void AcceptJob(IPlayer player, int jobId)
        {
            if (player.GetCharacter().GetJob() != null)
            {
                player.SendError("Du hast bereits einen Job!");
                return;
            }

            var job = Core.GetService<JobManager>().GetJob(jobId);
            player.GetCharacter().SetJob(job);
            player.SendSuccess("Job angenommen!");

            player.Emit("Popup:Close");
            job.ShowJobMenu(player);
        }

        [AltV.Net.Event("Job:DeclineJob")]
        public void DeclineJob(IPlayer player, int jobId)
        {
            if (player.GetCharacter().GetJob() == null)
            {
                player.SendError("Du hast keinen Job zum kündigen!");
                return;
            }

            var job = Core.GetService<JobManager>().GetJob(jobId);

            player.GetCharacter().SetJob(null);
            player.SendSuccess("Job gekündigt!");

            player.Emit("Popup:Close");
            job.ShowJobMenu(player);
        }

        [AltV.Net.Event("Job:StartJobSingle")]
        public void StartJob(IPlayer player, int jobId)
        {
            if (player.GetCharacter().GetJob() == null)
            {
                player.SendError("Du hast keinen Job!");
                return;
            }

            Core.GetService<JobManager>().GetJob(jobId).AddPlayerToJob(player, player, false);
            Core.GetService<JobManager>().GetJob(jobId).StartJob(player);
            player.Emit("Popup:Close");
        }

        [AltV.Net.Event("Job:StartJobMultiplayer")]
        public void StartJobMultiplayer(IPlayer player, int jobId)
        {
            if (player.GetCharacter().GetJob() == null)
            {
                player.SendError("Du hast keinen Job!");
                return;
            }

            Core.GetService<JobManager>().GetJob(jobId).StartJob(player);
            player.Emit("Popup:Close");
        }

        [AltV.Net.Event("Job:StopJob")]
        public void StopJob(IPlayer player, int jobId)
        {
            if (player.GetCharacter().GetJob() == null)
            {
                player.SendError("Du hast keinen Job!");
                return;
            }

            Core.GetService<JobManager>().GetJob(jobId).StopJob(player);
            player.Emit("Popup:Close");
        }

        [AltV.Net.Event("Job:AcceptCoop")]
        public void AcceptCook(IPlayer player)
        {
            if (player.GetCharacter().GetInteractionData() == null) return;

            var job = (Job) player.GetCharacter().GetInteractionData().SourceObject;
            var leader = (IPlayer)player.GetCharacter().GetInteractionData().CallBack;
            if(player.GetCharacter().GetJob() != job) player.GetCharacter().SetJob(job);
            job.AddPlayerToJob(player, leader, true);
            leader.Emit("Popup:Close");
            OpenCoopMenu(leader, job.JobId);
        }
        [AltV.Net.Event("Job:AskCoop")]
        public void AskCoop(IPlayer leader, int jobId, int playerId)
        {
            if (leader.GetCharacter().GetJob() == null)
            {
                leader.SendError("Du hast keinen Job!");
                return;
            }

            var job = Core.GetService<JobManager>().GetJob(jobId);
            var player = Core.GetService<PlayerManager>().GetClient(playerId);

            if (player != null)
            {
                var interactionData = new InteractionData
                {
                    SourceObject = job,
                    CallBack = leader
                };
                player.GetCharacter()
                    .ShowInteraction("Job-Einladung", "Job:AcceptCoop", "Drücke E um die Einladung anzunehmen"
                        , interactionData: interactionData);
                /*QuestionDialogue.Create(leader, player,
                    "Multiplayer Job Anfrage",
                    $"Möchtest du in das {job.Name}-Team von {leader.GetCharacter().GetNormalizedName()}"  +
                    " aufgenommen werden?",
                    (leader1, target1) =>
                    {
                        job.AddPlayerToJob(player, leader, true);
                        leader.Emit("Popup:Close");
                        OpenCoopMenu(leader, jobId);
                    },
                    (leader1, target) => leader.SendError($"{player.Name} hat deine Job-Anfrage abgelehnt!")
                );*/
            }
            else
                leader.SendError("Spieler nicht gefunden!");
        }

        [AltV.Net.Event("Job:OpenCoop")]
        public void OpenCoopMenu(IPlayer player, int jobId)
        {
            var job = Core.GetService<JobManager>().GetJob(jobId);
            var currentPlayers = job.JobPlayers.ContainsKey(player) ? job.JobPlayers[player].Count : 1;
            var data = new PopupMenuDto
            {
                Title = "Job",
                Items = new List<PopupItemDto>
                {
                    new PopupHeaderDto
                    {
                        LeftText = "Aktuell im Team:",
                        RightText = $"{currentPlayers}/{job.MaxPlayers}"
                    }
                }
            };
            if (!job.JobPlayers.ContainsKey(player))
            {
                data.Items.Add(new PopupLabelDto
                {
                    Name = $"{player.Name} (Leader)"
                });
            }
            else
            {
                foreach (var jobPlayer in job.JobPlayers[player])
                {
                    data.Items.Add(new PopupLabelDto
                    {
                        Name = $"{jobPlayer.Value} " + (jobPlayer.Key == player.GetId() ? "(Leader)" : "")
                    });
                }
            }
            data.Items.Add(new PopupButtonDto
            {
                Name = "Gemeinsam arbeiten",
                Color = "green",
                Callback = "Job:StartJobMultiplayer",
                CallbackArgs = new List<object>{jobId}
            });
            if (!job.JobPlayers.ContainsKey(player) || job.JobPlayers[player].Count < job.MaxPlayers)
            {
                data.Items.Add(new PopupHeaderDto
                {
                    LeftText = "Spieler hinzufügen", RightText = ""
                });
                foreach (Player online in Alt.GetAllPlayers())
                {
                    if(online == player) continue;
                    data.Items.Add(new PopupButtonDto
                    {
                        Name = online.Name,
                        Callback = "Job:AskCoop",
                        CallbackArgs = new List<object>{jobId, online.GetAccount().Id}
                    });
                }
            }

            player.Emit("Popup:Show", data);
        }

        [AltV.Net.Event("Job:BuyUpgrade")]
        public void BuyUpgrade(IPlayer player, int jobId, int categoryId, int upgradeId)
        {
            if (player.GetCharacter().GetJob() == null) return;
            var job = Core.GetService<JobManager>().GetJob(jobId);
            var needed = job.GetJobUpgradePoints(player, categoryId, upgradeId);
            if (needed <= 0)
            {
                player.SendError("Du besitzt dieses Upgrade bereits!");
                return;
            }

            if (job.GetPlayerUpgradePoints(player) >= needed)
            {
                job.SetJobUpgrade(player, categoryId, upgradeId);
                Logger.Info($"BUY Upgrade For Job {jobId} Cat: {categoryId} Upgrade: {upgradeId}");

                player.SendSuccess("Upgrade gekauft!");
                player.Emit("Popup:Close");
                job.ShowJobMenu(player, "upgrade");
            }
            else
            {
                player.SendError("Du hast nicht genug Job-Punkte!");
            }
        }
    }
}