using System;
using System.Collections.Generic;
using System.Text;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using models.Enums;
using server.Admin;
using server.BankAccounts;
using server.Database;
using server.Inventory.Inventories;
using IPlayer = server.Players.IPlayer;
using Player = server.Players.Player;
using Team = server.Teams.Team;
using Vehicle = server.Vehicles.Vehicle;

namespace server.Commands
{
    public class AdminCommands : IScript
    {
        [Command("test", GreedyArg = true)]
        public static void CommandTest(IPlayer player, string args)
        {
            Alt.Log($"Test Command {player.Name}: {args}");
        }

        [Command("gethere", GreedyArg = true)]
        public static void GetHere(IPlayer player, string target)
        {
            if (!player.HasPermission(AdminLevel.Supporter)) return;

            var targetPlayer = Player.FindPlayer(player, target);
            if (targetPlayer != null)
            {
                var pos = player.Position;
                targetPlayer.Position = pos;

                player.SendNotification($"Du hast ~b~{targetPlayer.Name}~s~ zu dir geportet!");
                targetPlayer.SendNotification($"~b~{player.Name}~s~ hat dich zu sich geportet!");
            }
            else
            {
                player.SendError("Spieler nicht gefunden!");
            }
        }

        [Command("goto", GreedyArg = true)]
        public static void GoTo(IPlayer player, string target)
        {
            if (!player.HasPermission(AdminLevel.Supporter)) return;

            var targetPlayer = Player.FindPlayer(player, target);
            if (targetPlayer != null)
            {
                var pos = targetPlayer.Position;
                player.Position = pos;
                player.SendNotification($"Du hast dich zu ~b~{targetPlayer.Name}~s~ geportet!");
                targetPlayer.SendNotification($"~b~{player.Name}~s~ hat dich zu dir geportet!");
            }
            else
            {
                player.SendNotification($"Der Spieler ~b~{target}~s~ ist nicht online oder existiert nicht!");
            }
        }

        [Command("rkick", GreedyArg = true)]
        public static void KickPlayerFromServer(IPlayer player, string target, string reason)
        {
            if (!player.HasPermission(AdminLevel.Moderator)) return;

            var targetPlayer = Player.FindPlayer(player, target);
            if (targetPlayer != null)
            {
                targetPlayer.Kick(reason);
                //NAPI.Notification.SendNotificationToAll(
                //    $"~b~{player.Name}~s~ hat ~r~{targetPlayer.Name}~s~ vom Server gekickt! Grund: {reason}");
            }
            else
            {
                player.SendNotification($"Der Spieler ~b~{target}~s~ ist nicht online oder existiert nicht!");
            }
        }

        [Command("tp")]
        public static void TeleportToPosition(IPlayer player, string target)
        {
            if (!player.HasPermission(AdminLevel.TicketSupporter)) return;

            var positions = new Dictionary<string, TpPoint>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "LSPD",
                    new TpPoint
                    {
                        Name = "LSPD",
                        Category = "Faction",
                        Position = new Position(441.0374f, -983.301f, 30.68959f)
                    }
                },
                {
                    "FIB",
                    new TpPoint
                    {
                        Name = "FIB",
                        Category = "Faction",
                        Position = new Position(102.7933f, -744.4446f, 45.75473f)
                    }
                },
                {
                    "WeazelNews",
                    new TpPoint
                    {
                        Name = "WeazelNews",
                        Category = "Faction",
                        Position = new Position(-601.1564f, -932.6656f, 23.86415f)
                    }
                },
                {
                    "Müllabfuhr",
                    new TpPoint
                    {
                        Name = "Müllabfuhr",
                        Category = "Job",
                        Position = new Position(-561.5714f, 322.0399f, 84.40409f)
                    }
                },
                {
                    "Rasenpfleger",
                    new TpPoint
                    {
                        Name = "Rasenpfleger",
                        Category = "Job",
                        Position = new Position(-1351.391f, 137.5016f, 56.26399f)
                    }
                },
                {
                    "Farmer",
                    new TpPoint
                    {
                        Name = "Farmer",
                        Category = "Job",
                        Position = new Position(2324.784f, 5053.414f, 45.62836f)
                    }
                },
                {
                    "Stadthalle",
                    new TpPoint
                    {
                        Name = "Stadthalle",
                        Category = "Other",
                        Position = new Position(242.33208f, -399.01352f, 47.925564f)
                    }
                }
            };
            if (positions.TryGetValue(target, out var point))
            {
                player.Position = point.Position;
                player.SendInformation($"Du hast dich zum Punkt \"~b~{point.Name}~s~\" geportet!");
            }
            else
            {
                player.SendError("Teleport-Punkt nicht gefunden!");
                var builder = new StringBuilder();
                foreach (var pos in positions) builder.Append(pos.Key).Append(',').Append(' ');
                var result = builder.ToString();
                result = result.TrimEnd(' ').TrimEnd(',');

                player.SendInformation("~b~Moegliche Punkte sind:~s~\n" + result);
            }
        }

        [Command("getdim")]
        public static void GetDimension(IPlayer player)
        {
            player.SendChatMessage("Du bist in Dimension: " + player.Dimension);
        }

        [Command("gotocords", Alias = "gtc")]
        public static void GoToCords(IPlayer player, float x, float y, float z)
        {
            if (!player.HasPermission(AdminLevel.Supporter)) return;

            player.Position = new Position(x, y, z);
            player.SendNotification($"Du hast dich zu ~b~{x}, {y}, {z}~s~ geportet!");
        }

        [Command("createteam")]
        public static void CreateTeam(IPlayer player, string name, TeamType type)
        {
            //TODO: Not working yet
            if (!player.HasPermission(AdminLevel.Supporter)) return;
            var team = new Team
            {
                Name = name,
                TeamType = type,
                BankAccount = new BankAccount
                {
                    Money = 0,
                    OwnerType = OwnerType.Team,
                },
                Inventory = new TeamInventory()
                {
                    OwnerType = OwnerType.Team,
                    Type = InventoryType.Team
                }

            };
            Core.GetService<DatabaseContext>().TeamModel.Local.Add(team);
            Core.GetService<DatabaseContext>().BankAccountModel.Local.Add(team.BankAccount);
            Core.GetService<DatabaseContext>().InventoryModel.Local.Add(team.Inventory);

            player.SendInformation("Team wurde erfolgreich gespeichert!");
        }

        /* [Command("weather")]
        public void SetWeather(IPlayer player, string weather)
        {
            if (weather == "list")
            {
                player.SendChatMessage("Verfügbare Wetter sind:");
                foreach (Weather weatherValue in (Weather[])Enum.GetValues(typeof(Weather)))
                {
                    player.SendChatMessage(weatherValue.ToString());
                }

                return;
            }
            NAPI.World.SetWeather(weather);
        }*/

        [Command("addvehicle", Alias = "addv")]
        public static void CreateVehicle(IPlayer player, OwnerType ownerType, int ownerId)
        {
            if (!player.HasPermission(AdminLevel.Administrator)) return;

            if (player.IsInVehicle)
            {
                var vehicle = player.Vehicle;
                var data = new Vehicle
                {
                    Model = (VehicleModel) vehicle.Model,
                    OwnerType = ownerType,
                    OwnerId = ownerId,
                    PosX = vehicle.Position.X,
                    PosY = vehicle.Position.Y,
                    PosZ = vehicle.Position.Z,
                    RotZ = vehicle.Rotation.Yaw,
                    Color1 = vehicle.PrimaryColor,
                    Color2 = vehicle.SecondaryColor,
                    Plate = "new",
                    Locked = true,
                    Inventory = new VehicleInventory()
                    {
                        OwnerType = OwnerType.Vehicle,
                        Type = InventoryType.Vehicle
                    }
                };
                Core.GetService<DatabaseContext>().VehicleModel.Local.Add(data);
                Core.GetService<DatabaseContext>().InventoryModel.Local.Add(data.Inventory);
                player.SendInformation("Fahrzeug wurde erfolgreich gespeichert!");

            }
            else
            {
                player.SendError("Du sitzt in keinem Fahrzeug!");
            }
        }
    }
}