using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Threading;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Extensions;
using Exo.Rp.Core.Jobs;
using Exo.Rp.Core.Shops;
using Exo.Rp.Core.Shops.Types;
using Exo.Rp.Core.Teams.State;
using Exo.Rp.Core.Translation;
using Exo.Rp.Core.Util;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.World;
using Exo.Rp.Models.Enums;
using static System.Int32;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using Player = Exo.Rp.Core.Players.Player;

namespace Exo.Rp.Core.Commands
{
    internal class DevelopmentCommands : IScript
    {
        private static readonly Jail _jail = new Jail();

        [Command("respawn")]
        public static void RespawnPlayer(IPlayer player)
        {
            player.Spawn(player.Position);
        }

        [Command("ff")]
        public static void FaceFeaturesGui(Player player)
        {
            player.Emit("Ui:ShowFaceFeatures");
            player.HideUi(true);
        }

        [Command("fly", RequiredAdminLevel = AdminLevel.Moderator)]
        public static void Fly(IPlayer player)
        {
            if (player.Model == (uint)PedModel.Crow)
                player.GetCharacter().ResetSkin();
            else
                player.GetCharacter().SetTemporarySkin(PedModel.Crow);
        }

        [Command("skin", Alias = "char", RequiredAdminLevel = AdminLevel.Moderator)]
        public static void Skin(IPlayer player, string hash)
        {
            try {
                uint model;
                if (General.IsHexString(hash)) {
                    model = Convert.ToUInt32(hash, 16);
                } else {
                    model = (uint)Enum.Parse<PedModel>(hash, true);
                }

                if (Enum.IsDefined(typeof(PedModel), model)) {
                    if (player.Model == model)
                        player.GetCharacter().ResetSkin();
                    else
                        player.GetCharacter().SetTemporarySkin((PedModel)model);
                } else {
                    player.SendError(T._("Skin Hash nicht gefunden!", player));
                }
            }
            catch (SystemException e)
            {
                player.SendError(T._("Parsing fehlgeschlagen! {0}.", player, e.Message));
            }
        }

        [Command("save")]
        public static void Save(IPlayer player)
        {
            player.GetCharacter().Save();
            player.SendInformation("Dein Account wurde gespeichert!");
        }

        [Command("vehicle", Alias = "veh")]
        public static void CreateVehicle(IPlayer player, string hash)
        {
            try
            {
                uint model;
                if (General.IsHexString(hash)) {
                    model = Convert.ToUInt32(hash, 16);
                } else {
                    model = (uint)Enum.Parse<VehicleModel>(hash, true);
                }

                if (Enum.IsDefined(typeof(VehicleModel), model))
                {
                    var color = General.GetRandomColor();
                    var veh = Core.GetService<VehicleManager>().CreateTemporaryVehicle((VehicleModel)model, player.Position, player.Rotation.Roll, color, color, "Admin");

                    Alt.Log("Fahrzeug gespawnt: " + veh.Model.ToString() + "!");
                    player.SetIntoVehicle(veh.handle, -1);
                }
                else
                {
                    player.SendError(T._("Fahrzeug wurde nicht gefunden!", player));
                }
            }
            catch (SystemException e)
            {
                player.SendError(T._("Parsing fehlgeschlagen! {0}.", player, e.Message));
            }
        }

        [Command("weather", RequiredAdminLevel = AdminLevel.Administrator)]
        public static void ChangeWeather(IPlayer player, string weatherName)
        {
            if (Enum.IsDefined(typeof(WeatherType), weatherName))
            {
                foreach (var _player in Alt.GetAllPlayers())
                {
                    _player.SetWeather(Enum.Parse<WeatherType>(weatherName));
                }
            } else
            {
                player.SendError(T._("Wetter nicht gefunden!", player));

                var builder = new StringBuilder();
                foreach (var pos in Enum.GetNames(typeof(WeatherType))) builder.Append($"{pos}").Append(',').Append(' ');
                var result = builder.ToString();
                result = result.TrimEnd(' ').TrimEnd(',');
                player.SendInformation("Moegliche Wetter sind: " + result);
            }
        }

        [Command("time", RequiredAdminLevel = AdminLevel.Administrator)]
        public static void ChangeTime(IPlayer player, string _hh, string _mm)
        {
            var hh = Parse(_hh);
            var mm = Parse(_mm);
            if (hh < 0 || hh > 60 || mm < 0 || mm > 60)
            {
                player.SendError($"{hh}:{mm} ist eine ungültige Zeit!");
                return;
            }

            foreach (var _player in Alt.GetAllPlayers())
            {
                var date = DateTime.Now;
                _player.SetDateTime(date.Day, date.Month, date.Year, hh, mm, 0);
            }
        }

        [Command("weapon")]
        public static void WeaponCommand(IPlayer player, string hash)
        {
            try {
                uint model;
                if (General.IsHexString(hash)) {
                    model = Convert.ToUInt32(hash, 16);
                } else {
                    model = (uint)Enum.Parse<WeaponModel>(hash, true);
                }

                if (Enum.IsDefined(typeof(WeaponModel), model))
                {
                    player.GiveWeapon((WeaponModel)model, 500, true);
                }
                else
                {
                    player.SendError(T._("Waffe wurde nicht gefunden!", player));
                }
            }
            catch (SystemException e)
            {
                player.SendError(T._("Parsing fehlgeschlagen! {0}.", player, e.Message));
            }
        }

        [Command("getpos", Alias = "gp")]
        public static void GetPosition(IPlayer player)
        {
            if (player.IsInVehicle)
            {
                var veh = player.Vehicle;
                var pos = veh.Position;
                var rot = veh.Rotation;
                player.SendChatMessage("Fahrzeugposition", $"Position:\n{pos.X}, {pos.Y}, {pos.Z}\nRotation:\n{rot.Roll}, {rot.Pitch} {rot.Yaw}");
                //OutputPosition(player, pos, rot);
            }
            else
            {
                var pos = player.Position;
                var rot = player.Rotation;
                player.SendChatMessage("Spielerposition", $"Position:\n{pos.X}, {pos.Y}, {pos.Z}\nRotation:\n{rot.Roll}, {rot.Pitch} {rot.Yaw}");
                //OutputPosition(player, pos, rot);
            }
        }

        [Command("setpos", Alias = "sp")]
        public static void SetPosition(IPlayer player, string _x, string _y, string _z)
        {
            var x = float.Parse(_x);
            var y = float.Parse(_y);
            var z = float.Parse(_z);

            if (player.IsInVehicle)
            {
                player.Vehicle.Position = new Position(x, y, z);
            }
            else
            {
                player.Position = new Position(x, y, z);
            }
        }

        private static void OutputPosition(IPlayer player, Vector3 pos, Vector3 rot)
        {
            var playerPosString = pos.X + "| " + pos.Y + "| " + pos.Z;
            playerPosString = playerPosString.Replace(",", ".").Replace("|", ",");
            var html = "Position: <input type='text' value='new Vector3(" + playerPosString + ")' style='width:50%;'/>";
            player.Emit("chatHTMLOutput", html);

            var playerRotString = rot.X + "| " + rot.Y + "| " + rot.Z;
            playerRotString = playerRotString.Replace(",", ".").Replace("|", ",");
            html = "Rotation: <input type='text' value='new Vector3(" + playerRotString + ")' style='width:50%;'/>";
            player.Emit("chatHTMLOutput", html);
        }

        [Command("seat")]
        public static void ChangeSeat(IPlayer player, int seat)
        {
            if (player.IsInVehicle == false) return;

            player.SetIntoVehicle(player.Vehicle, seat);
        }

        [Command("inv")]
        public static void InventoryTest(IPlayer player)
        {
            player.GetCharacter().GetInventory().SyncInventory(player);
        }

        [Command("loadipl")]
        public static void LoadIpl(IPlayer player, string ipl)
        {
            player.RequestIpl(new[] {ipl});
            player.SendInformation("Trying to load IPL: " + ipl);
        }

        [Command("unloadipl")]
        public static void UnloadIpl(IPlayer player, string ipl)
        {
            player.RemoveIpl(new[] { ipl });
            player.SendInformation("Trying to unload IPL: " + ipl);
        }

        [Command("money", GreedyArg = false)]
        public static void Money(IPlayer player, string func, string _amount = "0", string _bank = "false")
        {
            var amount = int.Parse(_amount);
            var bank = bool.Parse(_bank);
            switch(func) {
                case "show":
                    player.SendInformation($"${player.GetCharacter().GetMoney(bank)}");
                    break;
                case "take":
                    player.GetCharacter().TakeMoney(amount, "Admin: " + player.GetAccount().ForumId, bank);
                    break;
                case "give":
                    player.GetCharacter().GiveMoney(amount, "Admin: " + player.GetAccount().ForumId, bank);
                    break;
                default:
                    player.SendError(T._("Funktion nicht gefunden!", player));
                    break;
            }
        }

        [Command("jail")]
        public static void Jail(IPlayer player, string target)
        {
            var test = _jail.AddPrisoner(Player.FindPlayer(player, target), new TimeSpan(0, 0, 10));
            player.SendChatMessage(null, test.ToString());
        }

        [Command("waypoint")]
        public static void GoToWaypoint(IPlayer player)
        {
            player.Emit("gotoWayPoint");
            player.SendInformation("Du hat dich an deine Waypoint Position geportet!");
        }

        [ClientEvent("Dev:GotoWaypoint")]
        public static void TeleportToWaypoint(IPlayer client, float x, float y, float z)
        {
            client.Position = new Position(x, y, z);
        }

        [Command("place")]
        public static void PlaceObject(IPlayer player, Objects obj)
        {
            if (obj == Objects.WasteBin)
            {
                player.SendInformation("Platziere die Mülltonne:");
                player.Emit("ObjectPlacer:Place", Objects.WasteBin.ToString(), "JobTrash:OnWasteBinPlaced");
                return;
            }

            player.SendError("Ungültiges Objekt!");
        }

        [Command("placeobject", Alias = "po")]
        public static void PlaceObject(IPlayer player, uint hash)
        {
            player.Emit("placeObject", hash, "Debug:onObjectPlaced");
        }

        [Command("jobpoints")]
        public static void JobPoints(IPlayer player)
        {
            Core.GetService<JobManager>().GetJob(1).GivePlayerUpgradePoints(player, 50);
        }

        [Command("del")]
        public static void Delete(IPlayer player)
        {

            foreach (var veh in Alt.GetAllVehicles()) veh.Remove();

            foreach (var col in Alt.GetAllColShapes()) col.Remove();

            foreach (var blip in Alt.GetAllBlips()) blip.Remove();
        }

        [Command("dc")]
        public static void Disconnect(IPlayer player)
        {
            Delete(player);
            player.Kick("Disconnect");
        }

        [Command("autologin")]
        public static void Autologin(IPlayer player, bool state)
        {
            player.GetAccount().Autologin = state;
            player.GetAccount().HardwareId = player.HardwareIdHash;
            player.GetAccount().SocialClubId = player.SocialClubId;
            player.SendChatMessage(null, state ? "Autologin aktiviert!" : "Autologin deaktiviert!");
        }

        [Command("addshopveh")]
        public static void AddShopVehicle(IPlayer player, int shopId, int price)
        {
            if (!player.IsInVehicle)
            {
                player.SendError("Du sitzt in keinem Fahrzeug!");
                return;
            }

            if (Core.GetService<ShopManager>().Get<VehicleShop>(shopId) == null)
            {
                player.SendError("Shop nicht gefunden!");
                return;
            }

            var veh = player.Vehicle;

            Core.GetService<DatabaseContext>().VehicleShopVehicleModel.Local.Add(new VehicleShopVehicle()
            {
                ModelName = ((VehicleModel)veh.Model).ToString(),
                Pos = veh.Position,
                Rot = veh.Rotation,
                Shop = Core.GetService<ShopManager>().Get<VehicleShop>(shopId),
                Price = price
            });
            player.SendSuccess("Fahrzeug hinzugefügt!");

        }

        [Command("toogleDoor", Alias = "td")]
        public static void ToggleDoorState(IPlayer player, string _hash, string _state)
        {
            var hash = Parse(_hash);
            var state = bool.Parse(_state);
            player.SendInformation($"Trying to set door state of {hash} to {state.ToString()}");

            Core.GetService<DoorManager>().SetDoorState(hash, state);
        }

        [ClientEvent("Debug:onObjectPlaced")]
        public static void OnObjectPlaced(IPlayer player, string pos, string rot, int number)
        {
            player.SendChatMessage(null, "#b#Object-Position:");
            var vPos = pos.DeserializeVector();
            var vRot = rot.DeserializeVector();

            //NAPI.Object.CreateObject(number, vPos, vRot, 255, 0);
            OutputPosition(player, vPos, vRot);
        }

        [Command("clearvehicles")]
        public static void ClearVehicles(IPlayer player)
        {
            var count = 0;
            foreach (IVehicle veh in Alt.GetAllVehicles())
            {
                veh.Remove();
                count++;
            }
            player.SendChatMessage("Command", $"{count} Fahrzeuge gelöscht");
            //NAPI.Chat.SendChatMessageToAll($"Alle Fahrzeuge wurden von {player.Name} gelöscht.");
        }

        [Command("out")]
        public static void Test(IPlayer player)
        {
            player.Emit("PlayerCameraZoomOut");
        }

        [Command("in")]
        public static void Test2(IPlayer player)
        {
            player.Emit("PlayerCameraZoomIn");
        }

        [Command("death")]
        public static void Death(IPlayer player)
        {
            player.Emit("PlayerDeath", "~r~Wasted", "Du bist gestorben!");
            Thread.Sleep(5000);
            player.Emit("PlayerDeathEnd");
        }

        [Command("deathstop")]
        public static void DeathEnd(IPlayer player)
        {
            player.Emit(" ");
        }
    }
}
