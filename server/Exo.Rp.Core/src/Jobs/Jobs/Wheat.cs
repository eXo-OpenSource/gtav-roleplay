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
using Exo.Rp.Core.Util;

namespace Exo.Rp.Core.Jobs.Jobs
{
    internal class Wheat
    {
        public Wheat(Position pos) {
            Center = pos;
            Col = (Colshape.Colshape)Alt.CreateColShapeSphere(Center, 2);
            Col.OnColShapeEnter += OnEnterCol;
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
            if (Farmer.MaxWheat < Farmer.CurrentWheat)
            {
                player.SendError("Geh dein Traktor entladen!");
                return;
            }

            LastUsed = DateTime.Now;
            Farmer.CurrentWheat++;
            player.SendInformation("+1 Weizen");
            player.Emit("Progress:Set", Math.Round(Farmer.CurrentWheat / (float)Farmer.MaxWheat, 2));
            player.GetCharacter().GetInventory().AddItem(wheat);
            Blip.RemoveVisibleEntity(player.Id);
            Core.GetService<PrivateStreamer>().RemoveEntity(Blip);

            /*new TimerHandler(() => {
                Core.GetService<PrivateStreamer>().AddEntity(Blip);
                Blip.AddVisibleEntity(player.Id);
            }, (int)Farmer.WheatCooldown * 1000); */
        }

        public bool IsUsable()
        {
            return Cooldown() < 0;
        }

        public int Cooldown()
        {
            return (int)(Farmer.WheatCooldown - (DateTime.Now - LastUsed).TotalSeconds);
        }

        public void Destroy(IPlayer player)
        {
            Col.Remove();
            Blip.RemoveVisibleEntity(player.Id);
            Core.GetService<PrivateStreamer>().RemoveEntity(Blip);
        }
    }
}
