using System.IO;
using System.Reflection;
using System.Text;
using Exo.Rp.Core.Inventory;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Teams;
using Exo.Rp.Core.Util.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TeamModel = Exo.Rp.Core.Teams.Team;
using ShopModel = Exo.Rp.Core.Shops.Shop;
using BankAccountModel = Exo.Rp.Core.BankAccounts.BankAccount;
using PedModel = Exo.Rp.Core.Peds.Ped;
using WorldObjectsModel = Exo.Rp.Core.World.WorldObject;
using VehicleShopVehicleModel = Exo.Rp.Core.Vehicles.VehicleShopVehicle;
using VehicleModel = Exo.Rp.Core.Vehicles.Vehicle;
using ItemModel = Exo.Rp.Core.Inventory.Items.Item;
using InventoryModel = Exo.Rp.Core.Inventory.Inventory;
using TeamMemberModel = Exo.Rp.Core.Teams.TeamMember;
using FaceFeaturesModel = Exo.Rp.Core.Players.Characters.FaceFeatures;
using LicensesModel = Exo.Rp.Core.Players.Characters.Licenses;
using CharacterModel = Exo.Rp.Core.Players.Characters.Character;
using AccountModel = Exo.Rp.Core.Players.Accounts.Account;

namespace Exo.Rp.Core.Database
{
    public class DatabaseContext : DbContext, IService
    {
        // If a new entity is added it need to be load "DatabaseCore.OnResourceStartHandler()"
        public DbSet<AccountModel> AccountModel { get; set; }
        public DbSet<CharacterModel> CharacterModel { get; set; }

        public DbSet<VehicleModel> VehicleModel { get; set; }
        public DbSet<TeamModel> TeamModel { get; set; }
        public DbSet<DepartmentModel> TeamDepartmentModel { get; set; }
        public DbSet<TeamMemberModel> TeamMemberModel { get; set; }
        public DbSet<TeamMemberPermissionModel> TeamMemberPermissionModel { get; set; }
        public DbSet<BankAccountModel> BankAccountModel { get; set; }
        public DbSet<LicensesModel> LicensesModel { get; set; }
        public DbSet<ShopModel> ShopModel { get; set; }
        public DbSet<VehicleShopVehicleModel> VehicleShopVehicleModel { get; set; }
        public DbSet<PedModel> PedModel { get; set; }
        public DbSet<InventoryModel> InventoryModel { get; set; }
        public DbSet<ItemModel> ItemModel { get; set; }
        public DbSet<InventoryItemsModel> InventoryItemsModel { get; set; }
        public DbSet<WorldObjectsModel> WorldObjectsModels { get; set; }

        public DbSet<FaceFeaturesModel> FaceFeaturesModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            if (SettingsManager.ServerSettings.Database.QueryLog)
            {
                optionsBuilder
                    .UseLoggerFactory(DatabaseCore.GetLoggerFactory(LogLevel.Information))
                    .UseMySql(ContextFactory.ConnectionString);
            }
            else
            {
                optionsBuilder
                    .UseLoggerFactory(DatabaseCore.GetLoggerFactory(LogLevel.Error))
                    .UseMySql(ContextFactory.ConnectionString);
            }
        }
    }
}