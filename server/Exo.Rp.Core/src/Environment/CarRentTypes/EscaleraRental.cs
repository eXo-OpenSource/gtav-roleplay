using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using System.Collections.Generic;
using Exo.Rp.Core.Environment.CarRentTypes;

using IPlayer = Exo.Rp.Core.Players.IPlayer;
namespace Exo.Rp.Core.Environment.CarRentTypes
{
    public partial class EscaleraRental : RentalGroup
    {
        public EscaleraRental() : base()
        {
            id = Count;
            name = "Autovermietung";
            colors = new List<Rgba[]> { new Rgba[] { new Rgba(229, 220, 22, 1), new Rgba(229, 220, 22, 1) } };
            markerPosition = new Position[]
            {
                new Position(-905.36f, -2337.53f, 6.7f)
            };
            returnPositions = new List<Position[]>
            {
                new Position[] {new Position(-918.9037475585938f, -2332.7763671875f, 6.709081649780273f),
                                new Position(-911.166015625f, -2331.48388671875f, 6.769497394561768f)}
            };
            availableVehicles = new Dictionary<string, List<object>>
            {
                { "Scorcher",  new List<object> {VehicleModel.Scorcher, 1} },
                { "Faggio3", new List<object> {VehicleModel.Faggio3, 1} },
                { "Faggio", new List<object> {VehicleModel.Faggio, 2} },
                { "Kanjo", new List<object> {VehicleModel.Kanjo, 3} },
                { "Dilettante", new List<object> {VehicleModel.Dilettante, 3} },
                { "Premier", new List<object> {VehicleModel.Premier, 4} },
                { "Stanier", new List<object> {VehicleModel.Stanier, 4} },
                { "Asterope", new List<object> {VehicleModel.Asterope, 5} },
                { "Tailgater", new List<object> {VehicleModel.Tailgater, 5} },
                { "Landstalker", new List<object> {VehicleModel.Landstalker, 6} },
                { "Raiden", new List<object> {VehicleModel.Raiden, 6} }
            };
            LoadPositions();
            LoadRentSpots();
            Alt.Log("Escalera: " + id);
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
