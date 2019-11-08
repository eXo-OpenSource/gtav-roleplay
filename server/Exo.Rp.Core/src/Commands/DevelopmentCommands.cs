﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using AltV.Net;
using AltV.Net.Enums;
using models.Enums;
using Newtonsoft.Json;
using server.Admin;
using server.Database;
using server.Extensions;
using server.Jobs;
using server.Shops;
using server.Shops.Types;
using server.Teams.State;
using server.Util;
using server.Vehicles;
using IPlayer = server.Players.IPlayer;
using Player = server.Players.Player;

namespace server.Commands
{
    internal class DevelopmentCommands : IScript
    {
        private readonly Jail _jail = new Jail();

        //[Command("fly")]
        public void Fly(IPlayer player)
        {
            if (!player.HasPermission(AdminLevel.Moderator)) return;

            if (player.Model == (uint) PedModel.Crow)
                player.GetCharacter().ResetSkin();
            else
                player.GetCharacter().SetTemporarySkin(PedModel.Crow);
        }

        //[Command("save")]
        public void Save(IPlayer player)
        {
            player.GetCharacter().Save();
            player.SendInformation("Dein Account wurde gespeichert!");
        }

        //[Command("veh")]
        public void CreateVehicle(IPlayer player, VehicleModel model)
        {
            if (!Enum.IsDefined(typeof(VehicleModel), model))
            {
                player.SendError("Fahrzeug nicht gefunden!");
                return;
            }

            var veh = Core.GetService<VehicleManager>().CreateTemporaryVehicle(model, player.Position, player.Rotation.Roll,
                General.GetRandomColor(), General.GetRandomColor(), "Admin");

            //NAPI.Chat.SendChatMessageToPlayer(player, "Fahrzeug gespawnt: " + vehicleName + "!");
            player.SetIntoVehicle(veh.handle, -1);
        }

        //[Command("nveh")]
        /*public void CreateVehicleNew(IPlayer player, string vehicleName)
        {
            var newVehicles = new List<string>
            {
                "entity2",
                "cheburek",
                "jester3",
                "caracara",
                "hotring",
                "seasparrow",
                "flashgt",
                "ellie",
                "michelli",
                "fagaloa",
                "dominator3",
                "tyrant",
                "tezeract",
                "gb200",
                "issi3",
                "taipan",
                "bruiser",
                "bruiser2",
                "bruiser3",
                "brutus",
                "brutus2",
                "brutus3",
                "cerberus",
                "cerberus2",
                "cerberus3",
                "clique",
                "deathbike",
                "deathbike2",
                "deathbike3",
                "deveste",
                "deviant",
                "dominator4",
                "dominator5",
                "dominator6",
                "impaler",
                "impaler2",
                "impaler3",
                "impaler4",
                "imperator",
                "imperator2",
                "imperator3",
                "issi4",
                "issi5",
                "issi6",
                "italigto",
                "monster3",
                "monster4",
                "monster5",
                "rcbandito",
                "scarab",
                "scarab2",
                "scarab",
                "schlagen",
                "slamvan4",
                "slamvan5",
                "slamvan6",
                "toros",
                "tulip",
                "vamos",
                "zr380",
                "zr3802",
                "zr3803"
            };


            if (!newVehicles.Contains(vehicleName))
            {
                player.SendError("Fahrzeug nicht gefunden!");
                return;
            }

            var veh = VehicleManager.CreateTemporaryVehicle(
                (VehicleModel) NAPI.Util.GetHashKey(vehicleName), player.Position, player.Heading,
                General.GetRandomColor(), General.GetRandomColor(), "Admin");


            //NAPI.Chat.SendChatMessageToPlayer(player, "Fahrzeug gespawnt: " + vehicleName + "!");
            player.SetIntoVehicle(veh.handle, -1);
        }*/

        //[Command("weapon")]
        public void WeaponCommand(IPlayer player, WeaponModel hash)
        {
            if (!Enum.IsDefined(typeof(WeaponModel), hash))
            {
                //NAPI.Chat.SendChatMessageToPlayer(player, "Waffe nicht gefunden!");
                return;
            }

            player.GiveWeapon((uint)hash, 500, true);
        }

        //[Command("eval", GreedyArg = true)]
        public void EvalCommand(IPlayer sender, string cmd)
        {
            //sender.Eval(cmd);
        }

        //[Command("getpos", Alias = "gp")]
        public void GetPosition(IPlayer player)
        {
            if (player.IsInVehicle)
            {
                var veh = player.Vehicle;
                var model = ((VehicleModel) player.Vehicle.Model).ToString();
                player.SendChatMessage("#b#Fahrzeug-Position (" + model + "):");
                var pos = veh.Position;
                var rot = veh.Rotation;
                OutputPosition(player, pos, rot);
            }
            else
            {
                player.SendChatMessage("#b#Spieler-Position:");
                var pos = player.Position;
                var rot = player.Rotation;
                OutputPosition(player, pos, rot);
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

        //[Command("seat")]
        public void ChangeSeat(IPlayer player, int seat)
        {
            if (player.IsInVehicle == false) return;

            player.SetIntoVehicle(player.Vehicle, seat);
        }


        //[Command("skin")]
        public void Skin(IPlayer player, PedModel skin)
        {
            player.Model = (uint)skin;
            player.SendInformation($"Du hast dir den Skin {skin.ToString()} gegeben!.");
        }

 
        //[Command("inv")]
        public void InventoryTest(IPlayer player)
        {
            player.GetCharacter().GetInventory().SyncInventory(player);
        }

        //[Command("loadipl")]
        public void LoadIpl(IPlayer player, string ipl)
        {
            player.Emit("loadIPL", JsonConvert.SerializeObject(new List<string> {ipl}));
            player.SendInformation("Trying to load IPL: " + ipl);
        }

        //[Command("unloadipl")]
        public void UnloadIpl(IPlayer player, string ipl)
        {
            player.Emit("unloadIPL", JsonConvert.SerializeObject(new List<string> {ipl}));
            player.SendInformation("Trying to unload IPL: " + ipl);
        }

        //[Command("money")]
        public void Money(IPlayer player, string func, int amount = 0, bool bank = false)
        {
            if (func == "get") player.SendChatMessage("Geld: $" + player.GetCharacter().GetMoney(bank));
            else if (func == "give")
            {
                player.GetCharacter().GiveMoney(amount, "Admin", bank, false);
                player.SendChatMessage("Geld: $" + player.GetCharacter().GetMoney(bank));
            }
            else if (func == "take")
            {
                player.GetCharacter().TakeMoney(amount, "Admin", bank, false);
                player.SendChatMessage("Geld: $" + player.GetCharacter().GetMoney(bank));
            }
        }

        //[Command("jail")]
        public void Jail(IPlayer player, string target)
        {
            var test = _jail.AddPrisoner(Player.FindPlayer(player, target), new TimeSpan(0, 0, 10));
            player.SendChatMessage(test.ToString());
        }

        //[Command("waypoint")]
        public void GoToWaypoint(IPlayer player)
        {
            player.Emit("gotoWayPoint");
            player.SendInformation("Du hat dich an deine Waypoint Position geportet!");
        }

        //[Command("place")]
        public void PlaceObject(IPlayer player, Objects obj)
        {
            if (obj == Objects.WasteBin)
            {
                player.SendInformation("Platziere die Mülltonne:");
                player.Emit("ObjectPlacer:Place", Objects.WasteBin.ToString(), "JobTrash:OnWasteBinPlaced");
                return;
            }

            player.SendError("Ungültiges Objekt!");
        }

        //[Command("placeobject", Alias = "po")]
        public void PlaceObject(IPlayer player, uint hash)
        {
            player.Emit("placeObject", hash, "Debug:onObjectPlaced");
        }

        //[Command("jobpoints")]
        public void JobPoints(IPlayer player)
        {
            Core.GetService<JobManager>().GetJob(1).GivePlayerUpgradePoints(player, 50);
        }

        //[Command("del")]
        public void Delete(IPlayer player)
        {

            foreach (var veh in Alt.GetAllVehicles()) veh.Remove();

            foreach (var col in Alt.GetAllColShapes()) col.Remove();

            foreach (var blip in Alt.GetAllBlips()) blip.Remove();
        }

        //[Command("dc")]
        public void Disconnect(IPlayer player)
        {
            Delete(player);
            player.Kick("Disconnect");
        }

        //[Command("autologin")]
        public void Autologin(IPlayer player, bool state)
        {
            player.GetAccount().Autologin = state;
            player.GetAccount().HardwareId = player.HardwareIdHash;
            player.GetAccount().SocialClubId = player.SocialClubId;
            player.SendChatMessage(state ? "Autologin aktiviert!" : "Autologin deaktiviert!");
        }

        //[Command("addshopveh")]
        public void AddShopVehicle(IPlayer player, int shopId, int price)
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

        [Event("Debug:onObjectPlaced")]
        public void OnObjectPlaced(IPlayer player, string pos, string rot, int number)
        {
            player.SendChatMessage("#b#Object-Position:");
            var vPos = pos.DeserializeVector();
            var vRot = rot.DeserializeVector();

            //NAPI.Object.CreateObject(number, vPos, vRot, 255, 0);
            OutputPosition(player, vPos, vRot);
        }

        //[Command("clearvehicles")]
        public void ClearVehicles(IPlayer player)
        {
            foreach (var veh in Alt.GetAllVehicles())
            {
                veh.Remove();
            }
            //NAPI.Chat.SendChatMessageToAll($"Alle Fahrzeuge wurden von {player.Name} gelöscht.");
        }

        //[Command("out")]
        public void Test(IPlayer player)
        {
            player.Emit("PlayerCameraZoomOut");
        }

        //[Command("in")]
        public void Test2(IPlayer player)
        {
            player.Emit("PlayerCameraZoomIn");
        }

        //[Command("death")]
        public void Death(IPlayer player)
        {
            player.Emit("PlayerDeath", "~r~Wasted", "Du bist gestorben!");
            Thread.Sleep(5000);
            player.Emit("PlayerDeathEnd");
        }

        
        //[Command("deathstop")]
        public void DeathEnd(IPlayer player)
        {
            player.Emit("PlayerDeathEnd");
        }
    }
}