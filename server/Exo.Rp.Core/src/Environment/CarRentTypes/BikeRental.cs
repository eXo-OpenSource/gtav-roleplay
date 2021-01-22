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
using Exo.Rp.Core.Environment;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using Vehicle = Exo.Rp.Core.Vehicles.Vehicle;

namespace Exo.Rp.Core.Environment.CarRentTypes
{
    public partial class BikeRental : RentalGroup
    {
        public BikeRental() : base()
        {
            id = Count;
            name = "Spoke-Fahrradverleih";
            markerPosition = new Position[]
            {
                new Position(-905.36f, -2337.53f, 6.7f)
            };
            returnPositions = new List<Position[]>
            {
                new Position[] { new Position(1,1,1), new Position(2,2,2) }
            };
            availableVehicles = new Dictionary<string, List<object>>
            {
                { "Bmx",  new List<object> {VehicleModel.Bmx, 1} },
                { "Cruiser", new List<object> {VehicleModel.Cruiser, 1} },
                { "Fixter", new List<object> {VehicleModel.Faggio, 2} },
                { "Scorcher", new List<object> {VehicleModel.Kanjo, 3} },
                { "Tribike", new List<object> {VehicleModel.TriBike, 3} }
            };
            LoadPositions();
            LoadRentSpots();
        }

        public override void SetPosToPlayerData(IPlayer player, bool reset = false)
        {
            if (reset)
            {
                player.DeleteData("rentGroup");
                Alt.Emit("CarRent:UpdateGroup", -1);
            }
            else
            {
                player.SetData("rentGroup", this);
                Alt.Emit("CarRent:UpdateGroup", id);
            }
        }
    }
}
