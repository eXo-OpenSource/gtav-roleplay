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
using RentVehicle = Exo.Rp.Core.Vehicles.Types.RentVehicle;
using Exo.Rp.Core.Commands;
using System.Timers;
using AltV.Net.Elements.Pools;
using Exo.Rp.Core.Vehicles.Types;
using System.Threading.Tasks;

namespace Exo.Rp.Core.Environment.CarRentTypes
{
    public abstract class RentalGroup
    {
        // dev commands
        [Command("cm")]
        public static void CreateMarker(IPlayer player, string markerId)
        {
            player.Emit("CarRent:CreateMarkerTest", -832.47f, -2351.03f, 14.569f, 1, int.Parse(markerId));
        }

        public static Random Random = new Random();
        public static int Count;

        public int loadedRentSpots;
        public int loadedReturnSpots;
        public string name;
        public int id;
        public List<Rgba[]> colors;
        public Position[] markerPosition;
        public List<RentVehiclePosition> vehiclePositions;
        public List<Position[]> returnPositions;
        public Dictionary<string, List<object>> availableVehicles;
        public List<RentVehicle> spawnedVehicles = new List<RentVehicle>();
        public Dictionary<int, RentVehicle> rentedVehicles = new Dictionary<int, RentVehicle>();
        public List<Colshape.Colshape> colShapes = new List<Colshape.Colshape>();
        public List<Colshape.Colshape> returnColShapes = new List<Colshape.Colshape>();
        public Dictionary<int, Timer> timers = new Dictionary<int, Timer>();

        public virtual void LoadRentSpots()
        {
            foreach (Position rentalPos in markerPosition)
            {
                Core.GetService<PublicStreamer>().AddGlobalBlip(new StaticBlip
                {
                    Color = 5,
                    Name = name,
                    X = rentalPos.X,
                    Y = rentalPos.Y,
                    Z = rentalPos.Z,
                    SpriteId = 198
                });

                Alt.EmitAllClients("CarRent:CreateMarker", rentalPos.X, rentalPos.Y, rentalPos.Z, id);
                var nShape = (Colshape.Colshape) Alt.CreateColShapeCylinder(rentalPos, 1f, 2f);
                nShape.OnColShapeEnter += OnColshapeEnter;
                nShape.OnColShapeExit += OnColshapeLeave;
                colShapes.Add(nShape);
                loadedRentSpots++;
            }
            foreach (Position[] rentalReturnPos in returnPositions)
            {
                var nShape = (Colshape.Colshape)Alt.CreateColShapeRectangle(rentalReturnPos[0].X, rentalReturnPos[0].Y, rentalReturnPos[1].X, rentalReturnPos[1].Y, 3f);
                nShape.OnColShapeEnter += OnReturnColshapeEnter;
                nShape.OnColShapeExit += OnReturnColshapeLeave;
                returnColShapes.Add(nShape);
                loadedReturnSpots++;
            }
            foreach (RentVehiclePosition vehicle in vehiclePositions)
            {
                var color = colors[Random.Next(colors.Count)];
                var veh = Core.GetService<VehicleManager>().CreateRentedVehicle(null, (VehicleModel)Enum.Parse(typeof(VehicleModel), vehicle.Name),
                    vehicle.Pos, vehicle.Rot.Yaw, 3600000, color[0], color[1], this);
                veh.handle.LockState = VehicleLockState.Locked;
                veh.handle.SetData("rentalGroup", id);
                spawnedVehicles.Add(veh);
            }
        }

        public void RentVehicle(IPlayer player, string vehicle)
        {
            var vehicleHash = (VehicleModel)Enum.Parse(typeof(VehicleModel), vehicle); // Find hash
            List<RentVehicle> possibleVehicles = spawnedVehicles.FindAll(x => x.Model == vehicleHash && !x.IsActive); // Find free vehicle
            var rentedVehicle = possibleVehicles[Random.Next(possibleVehicles.Count)]; // pick random from free vehicles
            player.Emit("CarRent:CreateRentMarker", rentedVehicle.PosX, rentedVehicle.PosY, rentedVehicle.PosZ); // create marker over rent vehicle
            rentedVehicle.IsActive = true; // set rented
            rentedVehicle.OwnerId = player.GetId(); // set owner to let player interact
            rentedVehicles.Add(player.GetId(), rentedVehicle); // add to rentedVehicles
        }
        
        public virtual void ReturnVehicle(IPlayer player)
        {
            if (player != null && player.IsInVehicle)
            {
                var playerId = player.GetId();
                if (rentedVehicles.ContainsKey(playerId))
                {
                    var veh = rentedVehicles[playerId];
                    rentedVehicles.Remove(playerId); // remove from rented list
                    // TODO remove player from vehicle
                    player.SetSyncedMetaData("removeFromVehicle", true);
                    player.SendChatMessage("CarRent", "Fahrzeug abgegeben!");
                    Task.Delay(2300).ContinueWith((arg) =>
                    {
                        veh.Respawn();
                    });
                }
            }
        }
        public virtual RentVehicle[] GetUnrentedVehicles()
        {
            List<RentVehicle> res = new List<RentVehicle>();
            foreach (var veh in spawnedVehicles)
            {
                if (!veh.IsActive) res.Add(veh);
            }
            return res.ToArray();
        }

        public virtual void SetPosToPlayerData(IPlayer player, bool reset = false) { }
        public virtual void OnColshapeEnter(Colshape.Colshape col, IEntity entity) 
        {
            if (!(entity is IPlayer player)) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            SetPosToPlayerData(player);
            player.GetCharacter().ShowInteraction("Autovermietung", "CarRent:OnPedInteract", "Drücke E um zu interagieren", interactionData: interactionData);
        }
        public virtual void OnColshapeLeave(Colshape.Colshape col, IEntity entity) 
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;
            SetPosToPlayerData(player, true);
            player.GetCharacter().HideInteraction();
        }
        public virtual void OnReturnColshapeEnter(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.IsInVehicle)
            {
                player.SendChatMessage("CarRent", "/unrent um dein Auto abzugeben!");
            }
        }
        public virtual void OnReturnColshapeLeave(Colshape.Colshape col, IEntity entity)
        {

        }
    }
}
