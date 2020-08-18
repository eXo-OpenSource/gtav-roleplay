using System;
using AltV.Net;
using Exo.Rp.Core.Players;
using IPlayer = AltV.Net.Elements.Entities.IPlayer;

namespace Exo.Rp.Core.Factories.Entities
{
    public class PlayerEntityFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(IntPtr entityPointer, ushort id)
        {
            return new Player(entityPointer, id);
        }
    }
}