using AutoMapper;
using server.Inventory;
using server.Inventory.Inventories;
using server.Shops;
using server.Shops.Types;
using server.Teams;
using server.Teams.State;
using server.Vehicles;
using server.Vehicles.Types;

namespace server.AutoMapper
{
    static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            /*Mapper.Initialize(cfg =>
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
            });*/
        }
    }
}
