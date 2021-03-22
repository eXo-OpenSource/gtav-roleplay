using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AutoMapper;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Environment;
using Exo.Rp.Core.Extensions;
using Exo.Rp.Core.Inventory.Inventories;
using Exo.Rp.Core.Vehicles.Types;
using Exo.Rp.Models.Enums;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;
using Exo.Rp.Core.Environment.CarRentTypes;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Vehicles
{
    internal class VehicleManager : IManager
    {
        private readonly ILogger<VehicleManager> _logger;

        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly Dictionary<int, PlayerVehicle> _playerVehicles;
        private readonly Dictionary<int, TeamVehicle> _teamVehicles;
        private readonly List<TemporaryVehicle> _temporaryVehicles;
        private readonly List<RentVehicle> _rentVehicles;
        private readonly List<Vehicle> _vehicles;
        private int _teamporaryVehicleId = 50000;
        private int _rentedVehicleId = 100000;
        private Rgba _defaultColor = new Rgba(30, 135, 235, 255);

        public VehicleManager(DatabaseContext databaseContext, IMapper mapper, ILogger<VehicleManager> logger)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _logger = logger;

            _vehicles = new List<Vehicle>();
            _playerVehicles = new Dictionary<int, PlayerVehicle>();
            _teamVehicles = new Dictionary<int, TeamVehicle>();
            _temporaryVehicles = new List<TemporaryVehicle>();
            _rentVehicles = new List<RentVehicle>();
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
                    case OwnerType.RentedCar:
                        AddVehicle<RentVehicle>(vehicle);
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
            if (spawn) vehicle.Spawn();
            _vehicles.Add(_mapper.Map<T>(vehicle));

        }

        private void AddVehicleEx<T>(TemporaryVehicle vehicle, bool spawn = true)
        where T : TemporaryVehicle
        {
            if (spawn) vehicle.Spawn();
            _vehicles.Add(_mapper.Map<T>(vehicle));
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

        internal object GetVehicle<T>(IVehicle networkVehicle)
        {
            throw new NotImplementedException();
        }

        public PlayerVehicle CreatePlayerVehicle(IPlayer owner, VehicleModel model, Position position, float rotation, FuelType fuelType = FuelType.Gasoline, float fuel = 80.00f, float maxFuel = 80.00f)
        {
            var nVehicle = new PlayerVehicle()
            {
                OwnerType = OwnerType.Player,
                OwnerId =  owner.GetCharacter().Id,
                Plate = owner.GetCharacter().LastName,
                Model = model,
                Color1 = new Rgba(0, 0, 0, 255).ToInt32(),
                Color2 = new Rgba(0, 0, 0, 255).ToInt32(),
                FuelType = fuelType,
                Fuel = fuel,
                MaxFuel = maxFuel,
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
            Rgba? color1T, Rgba? color2T, string plate = "Temporary", FuelType fuelType = FuelType.Gasoline, float fuel = 6.00f, float maxFuel = 80.00f)
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
                FuelType = fuelType,
                Fuel = fuel,
                MaxFuel = maxFuel,
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

        public RentVehicle CreateRentedVehicle(IPlayer tempOwner, VehicleModel hash, Position position, float heading, int time,
            Rgba? color1T, Rgba? color2T, RentalGroup rentalGroup, bool isActive = false, string plate = "Rented")
        {
            if (color1T == null) color1T = _defaultColor;
            if (color2T == null) color2T = _defaultColor;
            var color1 = (Rgba)color1T;
            var color2 = (Rgba)color2T;

            var veh = new RentVehicle()
            {
                Id = _rentedVehicleId,
                OwnerType = OwnerType.RentedCar,
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
                RespawnPosX = position.X,
                RespawnPosY = position.Y,
                RespawnPosZ = position.Z,
                RespawnPos = position,
                RespawnRotZ = heading,
                IsActive = isActive,
                RentalGroup = rentalGroup,
                Inventory = new VehicleInventory()
                {
                    OwnerType = OwnerType.Vehicle,
                    Type = InventoryType.Vehicle
                }
            };

            /*Task.Delay(time).ContinueWith(_ =>
            {
                CarRent.RemoveVeh(tempOwner);
                veh.handle.Remove();
            });*/
            
            AddVehicle<RentVehicle>(veh, true);
            _rentedVehicleId++;
            return veh;
        }
    }
}