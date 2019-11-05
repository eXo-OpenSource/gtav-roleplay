using System;
using System.Collections.Generic;
using AutoMapper;
using server.Database;
using server.Enums;
using server.Shops.Types;
using System.Linq;
using server.AutoMapper;

namespace server.Shops
{
    internal class ShopManager
    {
        private static readonly List<Shop> Shops = new List<Shop>();

        private static Mapper mapper;

        static ShopManager()
        {
            if (!ContextFactory.Instance.ShopModel.Local.Any()) return;
            Console.Write("Shop loaded:");

            foreach (var shopModel in ContextFactory.Instance.ShopModel.Local)
                switch (shopModel.ShopType)
                {
                    case ShopType.Vehicle:
                        Console.Write("Vehicle Shop loaded");
                        Add<VehicleShop>(shopModel, true);
                        break;
                    case ShopType.Item:
                        Add<ItemShop>(shopModel);
                        break;
                    case ShopType.Tuning:
                        Add<TuningShop>(shopModel);
                        break;
                    case ShopType.Food:
                        break;
                    default:
                        Add<Shop>(shopModel);
                        break;
                }
        }

        private static void Add<T>(Shop shop, bool autoLoad = false) 
            where T : Shop
        {
            Shops.Add(AutoMapperConfiguration.GetMapper().Map<T>(shop));

            var shopT = shop as T;
            if (autoLoad)
                shopT?.Load();
        }

        public static T Get<T>(int shopId) 
            where T : Shop
        {
            return Shops.Find(x => x.Id == shopId) as T;
        }

    }
}