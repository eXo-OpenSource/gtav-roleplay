using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using server.Util.Settings;


using server.Inventory;
using server.Teams;
using TeamModel = server.Teams.Team;
using ShopModel = server.Shops.Shop;
using BankAccountModel = server.BankAccounts.BankAccount;
using PedModel = server.Peds.Ped;
using WorldObjectsModel = server.WorldObjects.WorldObject;
using VehicleShopVehicleModel = server.Vehicles.VehicleShopVehicle;
using VehicleModel = server.Vehicles.Vehicle;
using ItemModel = server.Inventory.Items.Item;
using InventoryModel = server.Inventory.Inventory;

using TeamMemberModel = server.Teams.TeamMember;
using FaceFeaturesModel = server.Players.Characters.FaceFeatures;

using CharacterModel = server.Players.Characters.Character;
using AccountModel = server.Players.Accounts.Account;

namespace server.Database
{
    public class DatabaseContext : DbContext
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
            if (ContextFactory.ConnectionString == null)
            {
                DatabaseCore.SetDatabaseConnection(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json"), Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "logs"));
            }
            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (SettingsManager.ServerSettings.Database.QueryLog)
            {
                optionsBuilder.UseLoggerFactory(DatabaseCore.GetLoggerFactory(LogLevel.Information)).UseMySQL(ContextFactory.ConnectionString);
            }
            else
            {
                optionsBuilder.UseLoggerFactory(DatabaseCore.GetLoggerFactory(LogLevel.Error)).UseMySQL(ContextFactory.ConnectionString);
            }
        }
    }
}
