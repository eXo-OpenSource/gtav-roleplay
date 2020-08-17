using System.Collections.Generic;
using System.Threading.Tasks;
using AltV.Net.Data;
using AltV.Net.Enums;
using models.Enums;
using models.Jobs;
using server.Inventory.Items;
using IPlayer = server.Players.IPlayer;

namespace server.Jobs.Jobs
{
	internal class Farmer : Job
	{
		public const double TreeCooldown = 30; // Seconds

		private readonly Item apple = Core.GetService<ItemManager>().GetItem(ItemModel.Apfel);

		private readonly Position _deliveryMarker = new Position(2315.753f, 5076.539f, 44.3425f);

		private readonly Tree[] trees =
		{
			new Tree(new Position(2390.723f, 4991.62f, 45.23268f)),
			new Tree(new Position(2389.445f, 5005.084f, 45.74849f)),
			new Tree(new Position(2377.074f, 5003.869f, 44.55497f)),
			new Tree(new Position(2377.004f, 5017.008f, 45.50089f))
		};

		private readonly Position[] wheatFieldCorners =
		{
			new Position(2309.235f, 5087.252f, 46.88379f),
			new Position(2259.764f, 5135.268f, 53.92041f),
			new Position(2296.756f, 5172.824f, 59.22077f),
			new Position(2357.246f, 5116.976f, 47.95833f),
			new Position(2325.556f, 5086.558f, 46.30328f)
		};


		private readonly Position wheatMarker = new Position(2338.303f, 5095.162f, 46.55493f);

		private readonly Position wheatTractorPosition = new Position(2311.975f, 5083.859f, 46.42386f);
		private readonly float wheatTractorRotation = 147.6577f;

		public Farmer(int jobId) : base(jobId)
		{
			Name = "Farmer";
			Description = "Bepflanze, Ernte und Verkaufe als Farmer!";
			PedPosition = new Position(2320.17f, 5078.815f, 45.82973f);

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

			player.GetCharacter().CreateMarker(_deliveryMarker,
				colshapeRange: 2,
				colEnter: p =>
				{
					var character = player.GetCharacter();
					if (character.IsJobCurrentAndActive<Farmer>())
					{
						OnDeliveryMarkerHit(player);
					}
				}
			);

			if (GetJobUpgradeValue(player, 1) >= 1)
			{
				var veh = CreateJobVehicle(player, VehicleModel.Tractor2, wheatTractorPosition, wheatTractorRotation);
			}

			player.SendInformation(Name + "-Job gestartet!");
		}

		public override void StopJob(IPlayer player)
		{
			base.StopJob(player);
			player.StopAnimation();
			player.GetCharacter().DeleteMarker(_deliveryMarker);
			player.SendInformation(Name + "-Job beendet!");
		}

		public void OnDeliveryMarkerHit(IPlayer player)
		{
			if (player.GetCharacter().IsJobCurrentAndActive<Farmer>()) return;

			if (player.GetCharacter().GetInventory().GetItemCount(apple) > 0)
			{
				player.SendInformation($"Du hast {player.GetCharacter().GetInventory().GetItemCount(apple)} Äpfel abgegeben.");
				player.GetCharacter().GetInventory().RemoveItem(apple);
			}
			else
			{
				player.SendInformation("Du hast keine Äpfel zum abgeben.");
			}
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
						player.SendInformation($"Äpfel: {player.GetCharacter().GetInventory().GetItemCount(apple)}");
					});
				});
			}
			else
			{
				player.SendInformation($"Dieser Baum hat gerade keine Äpfel, du musst noch {tree.Cooldown()} Sekunden warten!");
			}
		}
	}
}
