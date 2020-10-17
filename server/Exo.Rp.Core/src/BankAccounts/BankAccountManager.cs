using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Extensions;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Models.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.BankAccounts
{
    internal class BankAccountManager : IManager
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Dictionary<int, BankAccount> _accounts;
        public BankAccountManager(DatabaseContext databaseContext)
        {
            LoadATMCols();

            _databaseContext = databaseContext;
            _accounts = new Dictionary<int, BankAccount>();

            if (!_databaseContext.BankAccountModel.Local.Any()) return;

            foreach (var account in _databaseContext.BankAccountModel.Local)
                _accounts.Add(account.Id, account);
        }

        public BankAccount GetAccount(int id)
        {
            return _accounts.TryGetValue(id, out var account) ? account : null;
        }

        private Dictionary<int, Position[]> atmPositions;
        public string InteractionId { get; set; }

        public void LoadATMCols()
        {
            atmPositions = new Dictionary<int, Position[]>();

            var worldObjects = Core.GetService<DatabaseContext>().WorldObjectsModels.Local.Where(x => x.Type == WorldObjects.ATM);
            foreach (var model in worldObjects)
                atmPositions.Add(model.Id,
                    new[] { model.Position.DeserializeVector(), model.Rotation.DeserializeVector() });

            foreach (var pos in atmPositions) CreateATMCols(pos.Value[0]);
        }

        public void CreateATMCols(Position pos)
        {
            var col = (Colshape.Colshape)Alt.CreateColShapeSphere(pos, 1.5f);
            col.OnColShapeEnter += OnATMColEnter;
            col.OnColShapeExit += OnATMColExit;

            Core.GetService<PublicStreamer>().AddGlobalBlip(new StaticBlip
            {
                Color = 4,
                Name = "ATM",
                X = pos.X,
                Y = pos.Y,
                Z = pos.Z,
                SpriteId = 276,
            });
        }

        public void OnATMColEnter(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            InteractionId = player.GetCharacter()
                .ShowInteraction("Bankautomat", "BankAccount:ShowInteraction", interactionData: interactionData);
        }

        public void OnATMColExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;

            player.GetCharacter().HideInteraction(InteractionId);
        }
    }
}