using System;
using System.Collections.Generic;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Vehicles;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using Vehicle = Exo.Rp.Core.Vehicles.Vehicle;

namespace Exo.Rp.Core.Environment
{
    public class CarRent
    {
        int loadedCarRents = 0;

        private Dictionary<int, Colshape.Colshape> colshapes;
        private static Dictionary<int, Vehicle> vehicles;

        private readonly Position[] rentSpots =
        {
            new Position(-986.8756713867188f, -2690.510986328125f, 14.04065227508545f)
        };

        public CarRent()
        {
            colshapes = new Dictionary<int, Colshape.Colshape>();
            vehicles = new Dictionary<int, Vehicle>();
            LoadCarRents();
        }

        public void LoadCarRents()
        {
            foreach (Position _carRent in rentSpots)
            {
                Core.GetService<PublicStreamer>().AddGlobalBlip(new StaticBlip
                {
                    Color = 5,
                    Name = "Autovermietung",
                    X = _carRent.X,
                    Y = _carRent.Y,
                    Z = _carRent.Z,
                    SpriteId = 198,
                });
                colshapes.Add(loadedCarRents, (Colshape.Colshape)Alt.CreateColShapeSphere(_carRent, 1.9f));
                colshapes[loadedCarRents].OnColShapeEnter += OnColshapeEnter;
                colshapes[loadedCarRents].OnColShapeExit += OnColshapeLeave;
                loadedCarRents++;
            }
        }

        public static Vehicle SpawnVehicle(IPlayer player, string vehicle, int price)
        {
            if (!vehicles.ContainsKey(player.GetId()))
            {
                if (price <= player.GetCharacter().GetMoney()) {
                    var veh = Core.GetService<VehicleManager>().CreateRentedVehicle(player, (VehicleModel)Enum.Parse(typeof(VehicleModel), vehicle),
                        new Position(-986.8756713867188f, -2690.510986328125f, 14.04065227508545f), 0, 3600000, Rgba.Zero, Rgba.Zero);

                    player.GetCharacter().TakeMoney(price, "Fahrzeugverleih");
                    player.SendSuccess($"Fahrzeug gemietet! (-${price})");
                    player.SetIntoVehicle(veh.handle, -1);
                    vehicles.Add(player.GetId(), veh);
                    player.Emit("CarRent:CloseUI");
                    return veh;
                } else
                {
                    player.SendError("Du hast nicht genug Geld!");
                    return null;
                }
            }
            player.SendError("Du hast bereits ein Leihfahrzeug!");
            return null;
        }

        public static void RemoveVeh(IPlayer player)
        {
            if (vehicles.ContainsKey(player.GetId()))
            {
                vehicles.Remove(player.GetId());
                player.Emit("CarRent:CloseUI");
                player.SendError("Dein Leihfahrzeug ist abgelaufen!");
            }
        }

        public void OnColshapeEnter(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;

            var interactionData = new InteractionData {
                SourceObject = this,
                CallBack = null
            };

            player.GetCharacter().ShowInteraction("Rent A Car", "CarRent:OnPedInteract", "Drücke E um zu interagieren", interactionData: interactionData);
        }

        public void OnColshapeLeave(Colshape.Colshape col, IEntity entity)
        {
            if(!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;
            player.GetCharacter().HideInteraction();
        }
    }
}