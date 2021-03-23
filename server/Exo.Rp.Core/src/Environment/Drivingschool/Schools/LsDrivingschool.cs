using System;
using System.Collections.Generic;
using AltV.Net.Data;
using AltV.Net.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Environment
{
    internal class LsDrivingschool : Drivingschool
    {

        public LsDrivingschool(int drivingschoolId) : base(drivingschoolId)
        {
            Name = "Strawberry Drivingschool";
            SpriteId = 545;
            PedPosition = new Position(214.45714f, -1400.189f, 30.57727f);
            PedModel = PedModel.Business01AMM;

            Init();
        }
    }
}
