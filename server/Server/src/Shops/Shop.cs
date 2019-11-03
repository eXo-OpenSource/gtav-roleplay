using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Elements.Entities;
using server.Enums;
using server.Players;

namespace server.Shops
{
    public partial class Shop
    {
        [NotMapped]
        protected ColShape pedCol;

        public virtual void Load()
        {
            if (Blip > 0)
            {
               /* var blip = NAPI.Blip.CreateBlip((uint)Blip, PedModel.Pos, 1, (byte)BlipColor.Shop, BlipText,
                    255, 500, false, 0, 0);*/
            }

            if (PedModel == null) return;

            /*pedCol = NAPI.ColShape.CreateSphereColShape(PedModel.Pos, 3);
            pedCol.OnEntityEnterColShape += OnPedColEnter;
            pedCol.OnEntityExitColShape += OnPedColExit;*/
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

        protected virtual void OnPedColEnter(ColShape colshape, IPlayer player)
        {
            player.SendChatMessage("Entered Shop Col! " + Name);
        }

        protected virtual void OnPedColExit(ColShape colshape, IPlayer player)
        {
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