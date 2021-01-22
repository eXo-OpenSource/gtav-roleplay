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
using Exo.Rp.Core.Commands;

namespace Exo.Rp.Core.Environment.CarRentTypes
{
    public partial class TouchdownRental : RentalGroup
    {
        public TouchdownRental() : base()
        {
            id = Count;
            name = "Autovermietung";
            colors = new List<Rgba[]> { new Rgba[] { new Rgba(0, 157, 78, 1), new Rgba(0, 157, 78, 1) } };
            markerPosition = new Position[]
            {
                new Position(-832.47f, -2351.03f, 14.569f)
            };
            returnPositions = new List<Position[]>
            {
                new Position[] {new Position(-823.4036865234375f, -2331.30615234375f, 13.570625305175781f),
                                new Position(-830.0479736328125f, -2334.373779296875f, 13.570625305175781f)}
            };
            availableVehicles = new Dictionary<string, List<object>>
            {
                { "Cruiser",  new List<object> {VehicleModel.Cruiser, 1} },
                { "Fixter", new List<object> {VehicleModel.Fixter, 1} },
                { "Faggio", new List<object> {VehicleModel.Faggio, 2} },
                { "Blista", new List<object> {VehicleModel.Blista, 3} },
                { "Asbo", new List<object> {VehicleModel.Asbo, 3} },
                { "Asea", new List<object> {VehicleModel.Asea, 4} },
                { "Primo", new List<object> {VehicleModel.Blista, 4} },
                { "Serrano", new List<object> {VehicleModel.Serrano, 5} },
                { "Fugitive", new List<object> {VehicleModel.Fugitive, 6} },
                { "Rebla", new List<object> {VehicleModel.Rebla, 6} }
            };
            LoadPositions();
            LoadRentSpots();
            Alt.Log("Touchdown: " + id);
            Count++;
        }

        public override void SetPosToPlayerData(IPlayer player, bool reset = false)
        {
            if (reset)
            {
                CarRent.playerGroups.Remove(player.GetId());
                player.DeleteData("rentGroup");
                Alt.Emit("CarRent:UpdateGroup", -1);
            }
            else
            {
                CarRent.playerGroups.Add(player.GetId(), this);
                player.SetData("rentGroup", this);
                Alt.Emit("CarRent:UpdateGroup", id);
            }
        }
    }
}
