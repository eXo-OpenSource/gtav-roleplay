using System;
using AltV.Net;
using server.Players;
using IPlayer = AltV.Net.Elements.Entities.IPlayer;

namespace server.Factories.Entities
{
    public class PlayerEntityFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(IntPtr entityPointer, ushort id)
        {
            return new Player(entityPointer, id);
        }
    }
}