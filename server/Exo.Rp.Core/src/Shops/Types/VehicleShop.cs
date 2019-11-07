using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using models.Enums;
using models.Shops.Vehicles;
using Newtonsoft.Json;
using server.Database;
using server.Players.Characters;
using server.Vehicles;
using server.Vehicles.Types;
using IPlayer = server.Players.IPlayer;

namespace server.Shops.Types
{
    internal class VehicleShop : Shop
    {
        [NotMapped]
        private Position _spawnPos;
        [NotMapped]
        private float _spawnRot;
        [NotMapped]
        private Dictionary<int, VehicleDataDto> _vehicleData;
        [NotMapped]
        private Dictionary<int, TemporaryVehicle> _vehicles;

        public override void Load()
        {
            base.Load();

            var options = VehicleShopOptions;
            _spawnPos = new Position(options.SpawnPosX, options.SpawnPosY, options.SpawnPosZ);
            _spawnRot = options.SpawnRotZ;
            Console.Write("Load Vehicle Shop: " + Name);
            LoadVehicles();
        }

        protected override void OnPedColEnter(ColShape colshape, IPlayer player)
        {
           // player.SendChatMessage("Vehicle Shop Enter");
            if (player.GetCharacter() == null) return;
            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            player.GetCharacter()
                .ShowInteraction(PedModel.Name, "Shop:VehicleInteraction", interactionData: interactionData);

        }

        protected override void OnPedColExit(ColShape colshape, IPlayer player)
        {
            if (player.GetCharacter() == null) return;
            player.GetCharacter().HideInteraction();
        }

        public void OpenVehicleShopMenu(IPlayer player)
        {
            var data = new BuyMenuDto()
            {
                Id = Id,
                Name = Name,
                Vehicles = _vehicleData
            };
            Console.Write(JsonConvert.SerializeObject(data));
            player.Emit("Shop:VehicleShowMenu", JsonConvert.SerializeObject(data));
        }


        private void LoadVehicles()
        {
            _vehicleData = new Dictionary<int, VehicleDataDto>();
            _vehicles = new Dictionary<int, TemporaryVehicle>();

            if (!Core.GetService<DatabaseContext>().VehicleShopVehicleModel.Local.Any()) return;

            foreach (var vehicleM in Core.GetService<DatabaseContext>().VehicleShopVehicleModel.Local.Where(x => x.ShopId == Id))
            {
                var nVehicle = Core.GetService<VehicleManager>().CreateTemporaryVehicle(vehicleM.Vehicle, vehicleM.Pos, vehicleM.RotZ,
                    new Rgba(255, 255, 0, 255), new Rgba(255, 255, 0, 255), "for Sale");

                var nVehicleData = new VehicleDataDto()
                {
                    Id = vehicleM.Id,
                    Price = vehicleM.Price,
                    Name = vehicleM.ModelName,
                    Handle = nVehicle.handle
                };
                
                _vehicleData.Add(vehicleM.Id, nVehicleData);
                _vehicles.Add(vehicleM.Id, nVehicle);
                
            }
        }

        public override void BuyVehicle(IPlayer player, int vehicleId)
        {
            if (_vehicleData.TryGetValue(vehicleId, out var selectedVehicle) &&
                _vehicles.TryGetValue(vehicleId, out var vehicleHandle))
                if (player.GetCharacter().GetMoney() >= selectedVehicle.Price)
                    if (player.GetCharacter().TransferMoneyToShop(this, selectedVehicle.Price, "Fahrzeug-Kauf",
                        MoneyTransferCategory.Vehicle, MoneyTransferSubCategory.Buy))
                    {
                        var veh = Core.GetService<VehicleManager>().CreatePlayerVehicle(player, (VehicleModel) vehicleHandle.handle.Model,
                            _spawnPos, _spawnRot);
                        //player.SetIntoVehicle(veh.handle, -1);
                        veh.handle.SetSyncedMetaData("vehicle:setCollision", false);
                        /*NAPI.Task.Run(() =>
                        {
                            veh.handle.SetSyncedMetaData("vehicle:setCollision", true);
                        }, 10000);
                        return;*/
                    }

            player.SendError("Das Fahrzeug konnte nicht gekauft werden!");
        }
    }
}