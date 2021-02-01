using System;
using System.Collections.Generic;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Commands;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.Environment.CarRentTypes;
using Exo.Rp.Core.Translation;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using Vehicle = Exo.Rp.Core.Vehicles.Vehicle;
using AltV.Net.Elements.Args;
using System.Timers;
using System.Text;
using System.Linq;
using AltV.Net.Elements.Pools;
using Exo.Rp.Core.Vehicles.Types;

namespace Exo.Rp.Core.Environment
{
    public class CarRent
    {
        public static List<RentalGroup> rentalGroups;
        public static Dictionary<int, RentalGroup> playerGroups = new Dictionary<int, RentalGroup>();
        // dev commands
        [Command("rentveh")]
        public static void CommandRentVehicle(IPlayer player, string vehicle)
        {
            if (playerGroups.TryGetValue(player.GetId(), out RentalGroup rGroup))
            {
                if (!rGroup.rentedVehicles.ContainsKey(player.GetId()))
                {
                    rGroup.RentVehicle(player, vehicle);
                } else 
                {
                    player.SendChatMessage("CarRent", "Du hast bereits ein Leihwagen!");
                }
            }            
        }

        [Command("unrent")]
        public static void CommandUnrentVehicle(IPlayer player)
        {
            // TODO
            if (player.IsInVehicle)
            {
                var veh = player.Vehicle;
                if (veh.GetData("rentalGroup", out int id))
                {
                    /*CarRent.rentalGroups[id].spawnedVehicles.Remove((RentVehicle)veh);
                    veh.handle.Remove();*/
                    rentalGroups[id].ReturnVehicle(player);
                }
            }
        }
        [Command("owner")]
        public static void GetVehicleData(IPlayer player)
        {
            if (player.IsInVehicle)
            {
                var veh = (RentVehicle)player.Vehicle;
                player.SendChatMessage("CarRent", veh.OwnerId.ToString());
            }
        }
        [Command("rsprent")]
        public static void RespawnRentals(IPlayer player)
        {
            foreach (var group in rentalGroups)
            {
                foreach (var veh in group.spawnedVehicles)
                {
                    veh.Respawn();
                }
            }
        }
        [Command("vehhealth")]
        public static void VehicleHealth(IPlayer player)
        {
            player.SendChatMessage("VehHealth", player.Vehicle?.BodyHealth.ToString());
        }

        [Command("resetrental")]
        public static void ResetRental(IPlayer player)
        {
            foreach (RentalGroup group in rentalGroups)
            {
                foreach (RentVehicle veh in group.spawnedVehicles) veh.handle.Remove();
                group.LoadRentSpots();
            }
        }

        public CarRent()
        {
            // init all rentals
            rentalGroups = new List<RentalGroup>()
            {
                new TouchdownRental(), new EscaleraRental()//, new BikeRental()
            };
        }


        // deprecated
        public static Vehicle SpawnVehicle(IPlayer player, string vehicle, int price)
        {
            if (player.GetData("rentGroup", out RentalGroup rGroup))
            {
                if (price <= player.GetCharacter().GetMoney())
                {
                    int count = rGroup.vehiclePositions.Count - 1;
                    var vehiclePos = rGroup.vehiclePositions[new Random().Next(0, count)];
                    var veh = Core.GetService<VehicleManager>().CreateRentedVehicle(player, (VehicleModel)Enum.Parse(typeof(VehicleModel), vehicle),
                        vehiclePos.Pos, vehiclePos.Rot.Yaw, 3600000, Rgba.Zero, Rgba.Zero, rGroup);
                    rGroup.rentedVehicles.Add(player.GetId(), veh);
                    veh.handle.SetData("rentGroup", rGroup);

                    player.GetCharacter().TakeMoney(price, "Fahrzeugverleih");
                    player.SendSuccess(T._($"Fahrzeug gemietet! (-${price})", player));
                    player.SetIntoVehicle(veh.handle, -1);
                    player.Emit("CarRent:CloseUI");

                    player.SendChatMessage("CarRental", T._($"Fahrzeug ausgeliehen von '{rGroup.name}'", player));

                    /*var timer = new Timer(10000);
                    timer.Elapsed += (args, e) =>
                    {
                        player.GetCharacter().TakeMoney(1, "CarRental");
                        player.SendChatMessage("CarRent", T._("Dir wurden soeben $1 für die Nutztung deines Leihfahrzeuges abgezogen!", player));
                    };
                    timer.Enabled = true;
                    rGroup.timers.Add(player.GetId(), timer);*/
                    return veh;
                }
                else
                {
                    player.SendError(T._("Du hast nicht genug Geld!", player));
                    return null;
                }
            }
            player.SendError(T._("Du hast bereits ein Leihfahrzeug!", player));
            return null;
        }

        public static void RemoveVeh(IPlayer player)
        {
            if (player.GetData("rentGroup", out RentalGroup rGroup))
            {
                if (rGroup.timers[player.GetId()] != null)
                {
                    rGroup.timers[player.GetId()].Dispose();
                }
                player.DeleteData("rentGroup");
                rGroup.rentedVehicles.Remove(player.GetId());
                player.Emit("CarRent:CloseUI");
                player.SendError(T._("Dein Leihfahrzeug ist abgelaufen!", player));

                player.SendChatMessage("CarRent", "Dein Fahrzeug ist abgelaufen!");
            }
        }
    }
}