using AltV.Net;
using server.Jobs;
using server.Players;
using server.UI;
using server.Util.Log;

namespace server.Events.Jobs
{
    internal class JobEvents : IScript
    {
        private static readonly Logger<JobEvents> Logger = new Logger<JobEvents>();

        [Event("onJobPedInteraction")]
        public void OnJobPedInteraction(IPlayer player)
        {
            if (player.GetCharacter().GetInteractionData() == null) return;

            var job = (Job) player.GetCharacter().GetInteractionData().SourceObject;
            job.ShowJobMenu(player);
        }

        [Event("Job:AcceptJob")]
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

        [Event("Job:DeclineJob")]
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

        [Event("Job:StartJobSingle")]
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

        [Event("Job:StartJobMultiplayer")]
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

        [Event("Job:StopJob")]
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

        [Event("Job:AskCoop")]
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
                QuestionDialogue.Create(leader, player,
                    "Multiplayer Job Anfrage",
                    $"Möchtest du in das {job.Name}-Team von {leader.GetCharacter().GetNormalizedName()}"  +
                    " aufgenommen werden?",
                    (leader1, target1) =>
                    {
                        job.AddPlayerToJob(player, leader, true);
                        leader.Emit("Popup:Close");
                        job.ShowJobMenu(leader, "coop");
                    },
                    (leader1, target) => leader.SendError($"{player.Name} hat deine Job-Anfrage abgelehnt!")
                );
            else
                leader.SendError("Spieler nicht gefunden!");
        }

        [Event("Job:BuyUpgrade")]
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
