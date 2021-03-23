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
            PedPosition = new Position(232.48352f, -1095.033f, 29.279907f);
            PedModel = PedModel.Business01AMM;

            Init();
        }
    }
}