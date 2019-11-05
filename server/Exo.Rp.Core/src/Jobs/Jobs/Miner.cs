using System.Collections.Generic;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Object = AltV.Net.Elements.Entities.IWorldObject;

namespace server.Jobs.Jobs
{
    internal class Miner : Job
    {
        public static readonly double StoneCooldown = 30; // Seconds

        private readonly Dictionary<IPlayer, Object> _pickaxes = new Dictionary<IPlayer, Object>();

        private Stone[] _stones =
        {
            new Stone(new Position(2969.906f, 2777.57f, 38.48782f))
        };

        public Miner(int jobId) : base(jobId)
        {
            Name = "Bergbau-Arbeiter";
            Description = "Gewinne wertvolle Erze als Bergbau-Arbeiter!";
            PedPosition = new Position(2947.522f, 2743.842f, 43.35672f);

            Init();
        }

        public override void StartJobForPlayer(IPlayer player)
        {
            base.StartJobForPlayer(player);
            /*var pickaxe =
                NAPI.Object.CreateObject((int) Objects.Pickaxe, player.Position, new Position(0, 0, 0));
            _pickaxes.Add(player, pickaxe);
            player.AttachObject(pickaxe, 6286, new Position(0, 0, 0), new Position(0, 0, 180));*/

        }
    }
}