using System;
using System.Collections.Generic;
using AltV.Net.Data;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Environment
{
    internal class LosSantos : Cityhall
    {

        public LosSantos(int cityhallId) : base(cityhallId)
        {
            Name = "Los Santos Cityhall";
            SpriteId = 525;
            EntranceMarkerPos = new Position(232.48352f, -1095.033f, 29.279907f);
            EntranceSpawnPos = new Position(0f, 0f, 0f);
            ExitMarkerPos = new Position(0f, 0f, 0f);
            ExitSpawnPos = new Position(0f, 0f, 0f);

            Init();
        }
    }
}