using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using server.Enums;
using server.Peds;

namespace server.Teams.State
{
    internal class Lspd : Team
    {
        public Lspd()
        {
            var pos = new Position(440.7878f, -978.15f, 30.68959f);
            PedManager.CreatePed(PedModel.Cop01SFY, pos, 180, 0);
            var blip = Alt.CreateBlip(BlipType.Cop, pos);
        }
    }
}