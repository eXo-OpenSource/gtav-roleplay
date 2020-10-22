using System;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Models.Enums;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Streamer.Private;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Jobs.Jobs
{
    internal class Tree
    {
        public Tree(Position treeCenter)
        {
            Center = treeCenter;
            Col = (Colshape.Colshape) Alt.CreateColShapeSphere(Center, 2);
            Col.OnColShapeEnter += OnEnterCol;
            Col.OnColShapeExit += OnExitCol;
            LastUsed = DateTime.Now.AddSeconds(-Farmer.TreeCooldown);
            Blip = new PrivateBlip(Center, 0, 300) { Sprite = 364, Name = "Apfelbaum" };
            Core.GetService<PrivateStreamer>().AddEntity(Blip);
        }

        private Position Center { get; }
        private Colshape.Colshape Col { get; }
        private DateTime LastUsed { get; set; }
        private string InteractionId { get; set; }
        public PrivateEntity Blip { get; set; }

        private void OnEnterCol(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob().JobId != (int)JobId.Farmer ||
                !player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };

            InteractionId = player.GetCharacter().ShowInteraction("Apfelbaum", "JobFarmer:onTreeInteract",
                interactionData: interactionData, text: "Drücke E um einen Apfel zu pflücken!");
        }

        private void OnExitCol(Colshape.Colshape colshape, IEntity entity)
        {
            if(!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || !(player.GetCharacter().GetJob() is Farmer) ||
                !player.GetCharacter().IsJobActive()) return;

            if (InteractionId == null) return;

            player.GetCharacter().HideInteraction(InteractionId);
            InteractionId = null;
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
            return (int)(Farmer.TreeCooldown - (DateTime.Now - LastUsed).TotalSeconds);
        }

        public void Destroy(IPlayer player)
        {
            Col.Remove();
            Core.GetService<PrivateStreamer>().RemoveEntity(Blip);
            Blip.RemoveVisibleEntity(player.Id);
        }

        public static readonly Position[] treePositions =
        {
            new Position(2390.40771484375f, 4992.125f, 45.21962356567383f),
            new Position(2390.079833984375f, 5004.26171875f, 45.74787902832031f),
            new Position(2378.049560546875f, 5004.0302734375f, 44.6589241027832f),
            new Position(2376.60205078125f, 5016.2978515625f, 45.408302307128906f),
            new Position(2369.846923828125f, 5010.75244140625f, 44.35784912109375f),
            new Position(2361.297607421875f, 5002.5927734375f, 43.47602081298828f),
            new Position(2374.164794921875f, 4989.5556640625f, 44.0247917175293f),
            new Position(2361.7666015625f, 4988.44384765625f, 43.33650207519531f),
            new Position(2361.88525390625f, 4976.88330078125f, 43.247520446777344f),
            new Position(2349.7001953125f, 4975.369140625f, 42.78531265258789f),
            new Position(2349.57275390625f, 4989.04296875f, 43.03837585449219f),
            new Position(2336.568603515625f, 4976.43359375f, 42.61845779418945f),
            new Position(2318.156494140625f, 4984.11181640625f, 41.77555465698242f),
            new Position(2331.426513671875f, 4996.09716796875f, 42.12300491333008f),
            new Position(2344.7578125f, 5007.5498046875f, 42.72938919067383f),
            new Position(2357.5576171875f, 5020.33154296875f, 43.9100341796875f),
            new Position(2344.184326171875f, 5022.59619140625f, 43.53952407836914f),
            new Position(2331.153076171875f, 5007.36572265625f, 42.35152053833008f),
            new Position(2316.817626953125f, 4993.8310546875f, 42.0576286315918f),
            new Position(2305.280029296875f, 4996.951171875f, 42.300811767578125f),
            new Position(2316.397216796875f, 5008.6875f, 42.53541564941406f),
            new Position(2330.649658203125f, 5021.5f, 42.893898010253906f),
            new Position(2342.25f, 5034.69140625f, 44.306278228759766f),
            new Position(2329.868896484375f, 5036.81982421875f, 44.40830993652344f),
            new Position(2316.949462890625f, 5023.7119140625f, 43.345802307128906f)
        };
    }
}