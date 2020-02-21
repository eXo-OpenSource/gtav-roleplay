using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AutoMapper;
using models.Enums;
using server.Database;
using server.Extensions;
using server.Inventory.Inventories;
using server.Updateable;
using server.Util.Log;
using server.Vehicles.Types;
using IPlayer = server.Players.IPlayer;

namespace server.Vehicles
{ 
    internal class VehicleManager : IManager, IUpdateable
    {
        private static readonly Logger<VehicleManager> Logger = new Logger<VehicleManager>();

        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly Dictionary<int, PlayerVehicle> _playerVehicles;
        private readonly Dictionary<int, TeamVehicle> _teamVehicles;
        private readonly List<TemporaryVehicle> _temporaryVehicles;
        private readonly List<Vehicle> _vehicles;
        private int _teamporaryVehicleId = 50000;
        private Rgba _defaultColor = new Rgba(0, 0, 0, 255);

        public VehicleManager(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;

            _vehicles = new List<Vehicle>();
            _playerVehicles = new Dictionary<int, PlayerVehicle>();
            _teamVehicles = new Dictionary<int, TeamVehicle>();
            _temporaryVehicles = new List<TemporaryVehicle>();
            SpawnVehicles();
        }

        private void SpawnVehicles()
        {
            foreach (var vehicle in _databaseContext.VehicleModel.Local.ToList())
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

        private void AddVehicle<T>(Vehicle vehicle, bool spawn = true) 
            where T : Vehicle
        {
            _vehicles.Add(_mapper.Map<T>(vehicle));
            if (spawn) vehicle.Spawn();
        }

        public T GetVehicleFromHandle<T>(IVehicle vehicle)
            where T : Vehicle
        {
            return _vehicles.FirstOrDefault(x => x.handle == vehicle) as T;
        }

        public T GetVehicle<T>(Vehicle vehicle)
            where T : Vehicle
        {
            return (T) _vehicles.First(x => x == vehicle);
        }

        public void SavePlayerVehicles(IPlayer player)
        {
            foreach (var veh in _playerVehicles.Values.Where(veh => veh.OwnerId == player.GetId()))
                veh.Save();
        }

        public List<Vehicle> GetPlayerVehicles(IPlayer player)
        {
            return _vehicles.Where(veh => veh.OwnerType == OwnerType.Player && veh.OwnerId == player.GetCharacter().Id).ToList();
        }

        public PlayerVehicle CreatePlayerVehicle(IPlayer owner, VehicleModel model, Position position, float rotation)
        {
            var nVehicle = new PlayerVehicle()
            {
                OwnerType = OwnerType.Player,
                OwnerId =  owner.GetCharacter().Id,
                Plate = owner.GetCharacter().LastName,
                Model = model,
                Color1 = new Rgba(0, 0, 0, 255).ToInt32(),
                Color2 = new Rgba(0, 0, 0, 255).ToInt32(),
                PosX = position.X,
                PosY = position.Y,
                PosZ = position.Z,
                Pos = position,
                RotZ = rotation,
                Inventory = new VehicleInventory()
                {
                    OwnerType = OwnerType.Vehicle,
                    Type = InventoryType.Vehicle
                }
            };
            AddVehicle<PlayerVehicle>(nVehicle);
            _databaseContext.InventoryModel.Local.Add(nVehicle.Inventory);
            _databaseContext.VehicleModel.Local.Add(nVehicle);
            return nVehicle;
        }

        public TemporaryVehicle CreateTemporaryVehicle(VehicleModel hash, Position position, float heading,
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
                Model = hash,
                Color1 = color1.ToInt32(),
                Color2 = color2.ToInt32(),
                PosX = position.X,
                PosY = position.Y,
                PosZ = position.Z,
                Pos = position,
                RotZ = heading,
                Inventory = new VehicleInventory()
                {
                    OwnerType = OwnerType.Vehicle,
                    Type = InventoryType.Vehicle
                }
            };
            AddVehicle<TemporaryVehicle>(veh, true);
            _teamporaryVehicleId++;
            return veh;
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}