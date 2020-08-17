using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using models.Enums;
using Newtonsoft.Json;
using server.BankAccounts;
using Ped = server.Peds.Ped;

namespace server.Shops
{
	[Table("Shops")]
	public partial class Shop
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ShopType ShopType { get; set; }

		// TODO: Specify owner types for relations
		public OwnerType OwnerType { get; set; }
		public int OwnerId { get; set; }

		[Column(TypeName = "int(11)")]
		public BlipId Blip { get; set; }
		public string BlipText { get; set; }

		[ForeignKey("BankAccountId")]
		public BankAccount BankAccount { get; set; }

		[ForeignKey("PedId")]
		public Ped PedModel { get; set; }

		[NotMapped]
		public VehicleShopOptions VehicleShopOptions
		{
			get
			{
				if (ShopType != ShopType.Vehicle) return null;
				if (OptionsSerialized != null)
					return JsonConvert.DeserializeObject<VehicleShopOptions>(OptionsSerialized);

				VehicleShopOptions =  new VehicleShopOptions
				{
					SpawnPosX = 0,
					SpawnPosY = 0,
					SpawnPosZ = 0,
					SpawnRotZ = 0
				};
				return VehicleShopOptions;
			}
			set
			{
				if (ShopType == ShopType.Vehicle) OptionsSerialized = JsonConvert.SerializeObject(value);
			}
		}

		[NotMapped]
		public ItemShopOptions ItemShopOptions
		{
			get
			{
				if (ShopType == ShopType.Item)
				{
					if (OptionsSerialized != null)
						return JsonConvert.DeserializeObject<ItemShopOptions>(OptionsSerialized);
					return new ItemShopOptions {Items = new Dictionary<int, int>()};
				}

				return null;
			}
			set
			{
				if (ShopType == ShopType.Item) OptionsSerialized = JsonConvert.SerializeObject(value);
			}
		}

		[NotMapped]
		public TuningShopOptions TuningShopOptions
		{
			get
			{
				if (ShopType != ShopType.Tuning) return null;
				if (OptionsSerialized != null)
					return JsonConvert.DeserializeObject<TuningShopOptions>(OptionsSerialized);
				return new TuningShopOptions
				{
					SpawnPosX = 0,
					SpawnPosY = 0,
					SpawnPosZ = 0,
					SpawnRotZ = 0
				};
			}
			set
			{
				if (ShopType == ShopType.Tuning) OptionsSerialized = JsonConvert.SerializeObject(value);
			}
		}

		[Column("Options")] public string OptionsSerialized { get; set; }
	}
}
