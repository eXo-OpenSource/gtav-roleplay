using System;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Models.Enums;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Streamer.Private;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using Exo.Rp.Core.Inventory.Items;

namespace Exo.Rp.Core.Jobs.Jobs
{
    internal class Wheat
    {
        public Wheat(Position pos) {
            Center = pos;
            Col = (Colshape.Colshape)Alt.CreateColShapeSphere(Center, 2);
            Col.OnColShapeEnter += OnEnterCol;
            Col.OnColShapeExit += OnExitCol;
            LastUsed = DateTime.Now.AddSeconds(-Farmer.WheatCooldown);
            Blip = new PrivateBlip(Center, 0, 300) { Sprite = 364, Name = "Weizenfeld" };
            Core.GetService<PrivateStreamer>().AddEntity(Blip);
        }

        private readonly Item wheat = Core.GetService<ItemManager>().GetItem(ItemModel.Weizen);
        private Position Center { get; }
        private Colshape.Colshape Col { get; }
        private DateTime LastUsed { get; set; }
        public PrivateEntity Blip { get; set; }

        private void OnEnterCol(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob().JobId != (int)JobId.Farmer ||
                !player.GetCharacter().IsJobActive() || !player.IsInVehicle && !IsUsable()) return;

            player.GetCharacter().GetInventory().AddItem(wheat);
            Use();
            player.SendInformation("+1 Weizen");
        }

        private void OnExitCol(Colshape.Colshape colshape, IEntity entity)
        {

        }

        public bool IsUsable()
        {
            return Cooldown() < 0;
        }

        public void Use()
        {
            LastUsed = DateTime.Now;
        }

        public int Cooldown()
        {
            return (int)(Farmer.WheatCooldown - (DateTime.Now - LastUsed).TotalSeconds);
        }

        public void Destroy()
        {
            Col.Remove();
            Core.GetService<PrivateStreamer>().AddEntity(Blip);
        }
    }
}
