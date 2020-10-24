using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Inventory.Items;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Streamer.Private;
using Exo.Rp.Models.Enums;
using Exo.Rp.Models.Jobs;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Jobs.Jobs
{
    internal class WoodCutter : Job
    {
        public WoodCutter(int jobId) : base(jobId)
        {
            Name = "Holzfäller";
            Description = "Hacke Holz, verarbeite und verkaufe es!";
            PedPosition = new Position(-841.7275f, 5401.042f, 34.60437f);
            SpriteId = 496;

            trees = new Dictionary<int, Wood>();
            Init();
        }

        public Dictionary<int, Wood> trees;
        public readonly Item log = Core.GetService<ItemManager>().GetItem(ItemModel.Holzstamm);
        public const double Cooldown = 30;
        public const int MaxLogs = 5;
        public static int CurrentLogs = 0;

        public override void StartJobForPlayer(IPlayer player)
        {
            base.StartJobForPlayer(player);
            player.Emit("WoodCutter:OpenGUI");
            player.SendInformation(Name + "-Job gestartet!");

            player.Emit("Progress:Text", $"Holzstämme - max. {MaxLogs}");
            player.Emit("Progress:Set", CurrentLogs);
            player.Emit("Progress:Active", true);

            for (int i = 0; i < Wood.treePositions.Length; i++)
            {
                trees.Add(i, new Wood(Wood.treePositions[i]));
                trees[i].Blip.AddVisibleEntity(player.Id);
            }
        }

        public override void StopJob(IPlayer player)
        {
            base.StopJob(player);
            player.StopAnimation();
            player.SendInformation(Name + "-Job beendet!");
            player.Emit("Progress:Active", false);

            for (int i = 1; i < Wood.treePositions.Length; i++)
            {
                trees[i].Blip.RemoveVisibleEntity(player.Id);
                trees[i].Destroy(player);
                trees.Remove(i);
            }

        }
        public void OnTreeInteract(IPlayer player, Wood tree)
        {
            if (!player.GetCharacter().IsJobCurrentAndActive<WoodCutter>()) return;

            if (tree.IsUsable())
            {
                player.PlayAnimation("amb@prop_human_movie_bulb@idle_a", "idle_a",
                    (int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody));
                Task.Run(() =>
                {
                    tree.Use();
                    Task.Delay(10000).ContinueWith(_ =>
                    {
                        player.StopAnimation();
                        CurrentLogs++;
                        player.Emit("Progress:Set", Math.Round(CurrentLogs / (float)MaxLogs, 2));
                        player.GetCharacter().GetInventory().AddItem(log);
                        player.SendSuccess($"Holzstämme: {player.GetCharacter().GetInventory().GetItemCount(log)}");
                    });
                });
            }
            else
            {
                player.SendError($"Du kannst an dem Baum nicht mehr hacken, warte noch {tree.Cooldown()} Sekunden.");
            }
        }

    }
}