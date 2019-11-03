using System;
using System.Collections.Generic;
using AutoMapper;
using server.Database;
using server.Enums;
using server.Shops.Types;
using System.Linq;

namespace server.Shops
{
    internal class ShopManager
    {
        private static readonly List<Shop> Shops = new List<Shop>();

        static ShopManager()
        {
            if (!ContextFactory.Instance.ShopModel.Local.Any()) return;
            Console.Write("Shop loaded:");

            foreach (var shopModel in ContextFactory.Instance.ShopModel.Local)
                switch (shopModel.ShopType)
                {
                    case ShopType.Vehicle:
                        Console.Write("Vehicle Shop loaded");
                        AddShop<VehicleShop>(shopModel);
                        GetShop<VehicleShop>(shopModel.Id).Load();
                        break;
                    case ShopType.Item:
                        AddShop<ItemShop>(shopModel);
                        break;
                    case ShopType.Tuning:
                        AddShop<TuningShop>(shopModel);
                        break;
                    default:
                        AddShop<Shop>(shopModel);
                        break;
                }
        }

        private static void AddShop<T>(Shop shop) where T : Shop
        {
            //Shops.Add(Mapper.Map<T>(shop));
        }

        public static T GetShop<T>(int shopId) where T : Shop
        {
            return (T)Shops.Find(x => x.Id == shopId);
        }

    }
}