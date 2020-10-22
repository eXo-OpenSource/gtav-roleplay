using AutoMapper;
using Exo.Rp.Core.Inventory;
using Exo.Rp.Core.Inventory.Inventories;
using Exo.Rp.Core.Shops;
using Exo.Rp.Core.Shops.Types;
using Exo.Rp.Core.Teams;
using Exo.Rp.Core.Teams.State;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.Vehicles.Types;

namespace Exo.Rp.Core.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        private static Mapper _mapper;

        public static Mapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Teams
                cfg.CreateMap<Team, Lspd>();

                // Vehicles
                cfg.CreateMap<Vehicle, TemporaryVehicle>();
                cfg.CreateMap<Vehicle, PlayerVehicle>();
                cfg.CreateMap<Vehicle, TeamVehicle>();

                // Inventories
                cfg.CreateMap<Inventory.Inventory, PermanentInventory>();
                cfg.CreateMap<Inventory.Inventory, PlayerInventory>();
                cfg.CreateMap<Inventory.Inventory, TeamInventory>();
                cfg.CreateMap<Inventory.Inventory, TemporaryInventory>();
                cfg.CreateMap<Inventory.Inventory, VehicleInventory>();

                // Shops
                cfg.CreateMap<Shop, VehicleShop>();
                cfg.CreateMap<Shop, ItemShop>();
                cfg.CreateMap<Shop, TuningShop>();
            });

            return new Mapper(configuration);
        }
    }
}