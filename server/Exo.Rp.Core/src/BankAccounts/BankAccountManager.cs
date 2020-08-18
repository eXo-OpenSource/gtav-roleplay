using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Extensions;
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
            var col = (Colshape.Colshape)Alt.CreateColShapeSphere(pos, 1.9f);
            col.OnColShapeEnter += OnATMColEnter;
        }

        public void OnATMColEnter(Colshape.Colshape colshape, IEntity entity)
        {
            if (entity is IPlayer player)
                player.Emit("ATM:Show");
        }
    }
}