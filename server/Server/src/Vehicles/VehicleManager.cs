using System.Collections.Generic;
using System.Linq;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using server.AutoMapper;
using server.Database;
using server.Enums;
using server.Extensions;
using server.Inventory.Inventories;
using server.Players;
using server.Util.Log;
using server.Vehicles.Types;

namespace server.Vehicles
{
    internal class VehicleManager
    {
        private static readonly Logger<VehicleManager> Logger = new Logger<VehicleManager>();

        private static readonly Dictionary<int, PlayerVehicle> _playerVehicles;
        private static readonly Dictionary<int, TeamVehicle> _teamVehicles;
        private static readonly List<TemporaryVehicle> _temporaryVehicles;
        private static readonly List<Vehicle> _vehicles;
        private static int _teamporaryVehicleId = 50000;
        private static Rgba _defaultColor = new Rgba(0, 0, 0, 255);

        static VehicleManager()
        {
            _vehicles = new List<Vehicle>();
            _playerVehicles = new Dictionary<int, PlayerVehicle>();
            _teamVehicles = new Dictionary<int, TeamVehicle>();
            _temporaryVehicles = new List<TemporaryVehicle>();
            SpawnVehicles();
        }

        private static void SpawnVehicles()
        {
            foreach (var vehicle in ContextFactory.Instance.VehicleModel.Local.ToList())
            {

                switch (vehicle.OwnerType)
                {
                    case OwnerType.Player:
                        AddVehicle<PlayerVehicle>(vehicle);
                        break;
                    case OwnerType.Team:
                        AddVehicle<TeamVehicle>(vehicle);
                        break;
                    default:
                        AddVehicle<Vehicle>(vehicle);
                        break;
                }
            }
        }

        private static void AddVehicle<T>(Vehicle vehicle, bool spawn = true) 
            where T : Vehicle
        {
            _vehicles.Add(AutoMapperConfiguration.GetMapper().Map<T>(vehicle));
            if (spawn) vehicle.Spawn();
        }

        public static T GetVehicleFromHandle<T>(IVehicle vehicle)
            where T : Vehicle
        {
            return _vehicles.FirstOrDefault(x => x.handle == vehicle) as T;
        }

        public static T GetVehicle<T>(Vehicle vehicle)
            where T : Vehicle
        {
            return (T) _vehicles.First(x => x == vehicle);
        }

        public static void SavePlayerVehicles(IPlayer player)
        {
            foreach (var veh in _playerVehicles.Values.Where(veh => veh.OwnerId == player.GetId()))
                veh.Save();
        }

        public static List<Vehicle> GetPlayerVehicles(IPlayer player)
        {
            return _vehicles.Where(veh => veh.OwnerType == OwnerType.Player && veh.OwnerId == player.GetCharacter().Id).ToList();
        }

        public static PlayerVehicle CreatePlayerVehicle(IPlayer owner, VehicleModel model, Position position, float rotation)
        {
            var nVehicle = new PlayerVehicle()
            {
                OwnerType = OwnerType.Player,
                OwnerId =  owner.GetCharacter().Id,
                Plate = owner.GetCharacter().LastName,
                ModelName = model.ToString(),
                Color1 = new Rgba(0, 0, 0, 255).ToInt32(),
                Color2 = new Rgba(0, 0, 0, 255).ToInt32(),
                VehicleModel = model,
                PosX = position.X,
                PosY = position.Y,
                PosZ = position.Z,
                Pos = position,
                RotZ = rotation,
                InventoryModel = new VehicleInventory()
                {
                    OwnerType = OwnerType.Vehicle,
                    Type = InventoryType.Vehicle
                }
            };
            AddVehicle<PlayerVehicle>(nVehicle);
            ContextFactory.Instance.InventoryModel.Local.Add(nVehicle.InventoryModel);
            ContextFactory.Instance.VehicleModel.Local.Add(nVehicle);
            return nVehicle;
        }
        public static TemporaryVehicle CreateTemporaryVehicle(VehicleModel hash, Position position, float heading,
            Rgba? color1T, Rgba? color2T, string plate = "Temporary")
        {
            if (color1T == null) color1T = _defaultColor;
            if (color2T == null) color2T = _defaultColor;
            var color1 = (Rgba) color1T;
            var color2 = (Rgba) color2T;

            var veh = new TemporaryVehicle()
            {
                Id = _teamporaryVehicleId,
                OwnerType = OwnerType.None,
                OwnerId = -1,
                Plate = plate,
                ModelName = hash.ToString(),
                Color1 = color1.ToInt32(),
                Color2 = color2.ToInt32(),
                PosX = position.X,
                PosY = position.Y,
                PosZ = position.Z,
                Pos = position,
                RotZ = heading,
                InventoryModel = new VehicleInventory()
                {
                    OwnerType = OwnerType.Vehicle,
                    Type = InventoryType.Vehicle
                }
            };
            AddVehicle<TemporaryVehicle>(veh, true);
            _teamporaryVehicleId++;
            return veh;
        }
    }
}