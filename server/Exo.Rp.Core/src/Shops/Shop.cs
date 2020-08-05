using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net;
using AltV.Net.Elements.Entities;
using IPlayer = server.Players.IPlayer;

namespace server.Shops
{
    public partial class Shop
    {
        [NotMapped]
        protected Colshape.Colshape pedCol;

        public virtual void Load()
        {
            if (Blip > 0)
            {
				Alt.CreateBlip(BlipType.Cont, PedModel.Pos);
				/* var blip = NAPI.Blip.CreateBlip((uint)Blip, PedModel.Pos, 1, (byte)BlipColor.Shop, BlipText,
					 255, 500, false, 0, 0);*/
			}

			// if (PedModel == null) return;

            pedCol = (Colshape.Colshape) Alt.CreateColShapeSphere(PedModel.Pos, 3);
            pedCol.OnColShapeEnter += OnPedColEnter;
            pedCol.OnColShapeExit += OnPedColExit;
			Alt.Log("Loaded");
		}

		public int GetMoney()
        {
            return BankAccount.GetMoney();
        }

        public bool GiveMoney(int amount, string reason, bool silent)
        {
            return BankAccount.GiveMoney(amount);
        }

        public bool TakeMoney(int amount, string reason, bool silent)
        {
            return BankAccount.TakeMoney(amount);
        }

        protected virtual void OnPedColEnter(Colshape.Colshape colshape, IEntity entity)
        {
	        if(!(entity is IPlayer player)) return;
            player.SendChatMessage("Entered Shop Col! " + Name);
        }

        protected virtual void OnPedColExit(Colshape.Colshape colshape, IEntity entity)
        {
	        if(!(entity is IPlayer player)) return;
            player.SendChatMessage("Leaved Shop Col! " + Name);
        }

        public virtual void BuyVehicle(IPlayer player, int vehicleId)
        {
        }

        public virtual void BuyItem(IPlayer player, int itemId)
        {
        }
    }
}
