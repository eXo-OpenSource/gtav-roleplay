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
    internal class Farmer : Job
    {
        public const double TreeCooldown = 30; // Seconds
        public const double WheatCooldown = 60; // Seconds

        public const int MaxWheat = 20;
        public static int CurrentWheat = 0;

        private string TreeInteractionId;
        private string DeliveryInteractionId;
        private string WheatDeliveryInteractionId;

        public readonly Item apple = Core.GetService<ItemManager>().GetItem(ItemModel.Apfel);
        public readonly Item wheat = Core.GetService<ItemManager>().GetItem(ItemModel.Weizen);

        private Dictionary<int, Tree> appleTrees;
        private Dictionary<int, Wheat> wheatCorners;

        private Colshape.Colshape appleDeliveryMarker;
        private PrivateEntity appleDeliveryBlip;
        private Colshape.Colshape wheatDeliveryMarker;
        private PrivateEntity wheatDeliveryBlip;

        private readonly Position appleDeliveryPosition = new Position(2315.753f, 5076.539f, 44.3425f);
        private readonly Position wheatDeliveryPosition = new Position(2338.303f, 5095.162f, 46.55493f);

        private readonly Position wheatTractorPosition = new Position(2311.975f, 5083.859f, 46.42386f);
        private readonly float wheatTractorRotation = 147.6577f;

        public Farmer(int jobId) : base(jobId)
        {
            Name = "Farmer";
            Description = "Bepflanze, Ernte und Verkaufe als Farmer!";
            PedPosition = new Position(2320.17f, 5078.815f, 45.82973f);
            SpriteId = 496;

            appleTrees = new Dictionary<int, Tree>();
            wheatCorners = new Dictionary<int, Wheat>();

            JobUpgrades.Add(new JobUpgradeCategoryDto
            {
                Id = 1,
                Name = "Farmabteilungen",
                Description = "Arbeite an verschiedenen Stationen",
                Upgrades = new List<JobUpgradeDto>
                {
                    new JobUpgradeDto {Id = 0, Points = 0, Text = "Apfelbäume", Value = 0},
                    new JobUpgradeDto {Id = 1, Points = 50, Text = "Weizen", Value = 1},
                    new JobUpgradeDto {Id = 2, Points = 100, Text = "Weizen", Value = 2},
                    new JobUpgradeDto {Id = 3, Points = 150, Text = "Weizen", Value = 3}
                }
            });

            Init();
        }

        public override void StartJobForPlayer(IPlayer player)
        {
            base.StartJobForPlayer(player);

            player.Emit("Farmer:OpenGUI");
            player.SendInformation(Name + "-Job gestartet!");
        }

        public override void StopJob(IPlayer player)
        {
            base.StopJob(player);
            player.StopAnimation();
            player.SendInformation(Name + "-Job beendet!");
            player.Emit("Progress:Active", false);

            if (player.GetData("FarmerJob:JobType", out int result) && result == 1)
            {
                for (int i = 1; i < Wheat.wheatFieldCornerPositions.Length; i++)
                {
                    wheatCorners[i].Blip.RemoveVisibleEntity(player.Id);
                    wheatCorners[i].Destroy(player);
                    wheatCorners.Remove(i);
                }
            } else
            {
                appleDeliveryMarker.Remove();
                appleDeliveryBlip.RemoveVisibleEntity(player.Id);
                Core.GetService<PrivateStreamer>().RemoveEntity(appleDeliveryBlip);
                for (int i = 1; i < Tree.treePositions.Length; i++)
                {
                    appleTrees[i].Blip.RemoveVisibleEntity(player.Id);
                    appleTrees[i].Destroy(player);
                    appleTrees.Remove(i);
                }
            }
        }

        public void StartJobType(IPlayer player, int jobId)
        {
            if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
                !player.GetCharacter().IsJobActive()) return;

            if (jobId == 1)
            {
                player.Emit("Progress:Text", $"Traktor-Füllstand - max. {MaxWheat}");
                player.Emit("Progress:Set", 0);
                player.Emit("Progress:Active", true);

                var veh = CreateJobVehicle(player, VehicleModel.Tractor2, wheatTractorPosition, wheatTractorRotation);
                player.SetIntoVehicle(veh.handle, -1);

                CreateWheatDeliveryMarker(player, wheatDeliveryPosition);
                player.SendNotification("Fahre die auf der Karte markierten Weizenfelder ab!");
         
                for (int i = 0; i < Wheat.wheatFieldCornerPositions.Length; i++)
                {
                    wheatCorners.Add(i, new Wheat(Wheat.wheatFieldCornerPositions[i]));
                    wheatCorners[i].Blip.AddVisibleEntity(player.Id);
                }
            } else
            {
                CreateAppleDeliveryMarker(player, appleDeliveryPosition);
                player.SendNotification("Pflücke die auf der Karte markierten Äpfel ab!");

                for (int i = 0; i < Tree.treePositions.Length; i++)
                {
                    appleTrees.Add(i, new Tree(Tree.treePositions[i]));
                    appleTrees[i].Blip.AddVisibleEntity(player.Id);
                }

            }

        }

        public void CreateAppleDeliveryMarker(IPlayer player, Position position)
        {
            appleDeliveryBlip = new PrivateBlip(position, 0, 999) { Sprite = 38, Name = "Apfelankauf" };
            Core.GetService<PrivateStreamer>().AddEntity(appleDeliveryBlip);
            appleDeliveryBlip.AddVisibleEntity(player.Id);

            appleDeliveryMarker = (Colshape.Colshape)Alt.CreateColShapeSphere(position, 4);
            appleDeliveryMarker.OnColShapeEnter += OnAppleDeliveryMarkerHit;
            appleDeliveryMarker.OnColShapeExit += OnAppleDeliveryMarkerExit;
        }

        public void CreateWheatDeliveryMarker(IPlayer player, Position position)
        {
            wheatDeliveryBlip = new PrivateBlip(position, 0, 999) { Sprite = 38, Name = "Weizenankauf" };
            Core.GetService<PrivateStreamer>().AddEntity(wheatDeliveryBlip);
            wheatDeliveryBlip.AddVisibleEntity(player.Id);

            wheatDeliveryMarker = (Colshape.Colshape)Alt.CreateColShapeSphere(position, 4);
            wheatDeliveryMarker.OnColShapeEnter += OnWheatDeliveryMarkerHit;
            wheatDeliveryMarker.OnColShapeExit += OnWheatDeliveryMarkerExit;
        }

        public void OnTreeMarkerHit(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
                !player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            TreeInteractionId = player.GetCharacter().ShowInteraction("Baum pflücken", "JobFarmer:onTreeInteract", interactionData: interactionData);
        }

        public void OnTreeMarkerLeave(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            
            player.GetCharacter().HideInteraction(TreeInteractionId);
        }

        public void OnAppleDeliveryMarkerHit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
                !player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            DeliveryInteractionId = player.GetCharacter().ShowInteraction("Apfelankauf", "JobFarmer:onEnterDeliveryMarker", interactionData: interactionData);
        }

        public void OnAppleDeliveryMarkerExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;

            player.GetCharacter().HideInteraction(DeliveryInteractionId);
        }

        public void OnWheatDeliveryMarkerHit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob() != this ||
                !player.GetCharacter().IsJobActive() || !player.IsInVehicle) return;

            if (player.GetCharacter().GetInventory().GetItemCount(wheat) > 0)
            {
                player.SendSuccess($"Du hast {CurrentWheat} Weizen entladen.");
                player.GetCharacter().GetInventory().RemoveItem(wheat, CurrentWheat);
            }
            else
            {
                player.SendInformation("todo inventory bag");
                player.SendError("Du hast kein Weizen zum Entladen.");
            }
        }

        public void OnWheatDeliveryMarkerExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;

            player.GetCharacter().HideInteraction(WheatDeliveryInteractionId);
        }

        public void OnTreeInteract(IPlayer player, Tree tree)
        {
            if (!player.GetCharacter().IsJobCurrentAndActive<Farmer>()) return;

            if (tree.IsUsable())
            {
                player.PlayAnimation("amb@prop_human_movie_bulb@exit", "exit",
                    (int) (AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody));
                Task.Run(() =>
                {
                    tree.Use();
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        player.StopAnimation();
                        player.GetCharacter().GetInventory().AddItem(apple);
                        player.SendSuccess($"Äpfel: {player.GetCharacter().GetInventory().GetItemCount(apple)}");
                    });
                });
            }
            else
            {
                player.SendError($"Dieser Baum hat gerade keine Äpfel, du musst noch {tree.Cooldown()} Sekunden warten!");
            }
        }
    }
}