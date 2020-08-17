using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using models.Enums;
using server.Database;
using server.Shops.Types;

namespace server.Shops
{
    internal class ShopManager : IManager
    {
        private static readonly List<Shop> Shops = new List<Shop>();

        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public ShopManager(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;

            if (!_databaseContext.ShopModel.Local.Any()) return;
            foreach (var shopModel in _databaseContext.ShopModel.Local)
                switch (shopModel.ShopType)
                {
                    case ShopType.Vehicle:
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

        private void Add<T>(Shop shop, bool autoLoad = false)
            where T : Shop
        {
            Shops.Add(_mapper.Map<T>(shop));

            var shopT = shop as T;
            if (autoLoad)
                shopT?.Load();
        }

        public T Get<T>(int shopId)
            where T : Shop
        {
            return Shops.Find(x => x.Id == shopId) as T;
        }

    }
}