using System;
using System.Collections.Generic;
using AltV.Net.Data;
using AltV.Net.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Environment
{
    internal class LsCityhall : Cityhall
    {

        public LsCityhall(int cityhallId) : base(cityhallId)
        {
            Name = "Los Santos Cityhall";
            SpriteId = 498;
            PedPosition = new Position(-559.5692f, -202.24615f, 31.09961f);
            PedModel = PedModel.Business01AMM;
            ExitPosition = new Position(-542.32086f, -211.49011f, 31.09961f);
            EntrancePosition = new Position(232.48352f, -1095.033f, 29.279907f);
            ExitSpawn = new Position(232.48352f, -1095.033f, 29.279907f);
            Init();
        }
    }
}