using System.Collections.Generic;
using System.Linq;
using server.Database;

namespace server.BankAccounts
{
    internal class BankAccountManager : IManager
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Dictionary<int, BankAccount> _accounts;
        public BankAccountManager(DatabaseContext databaseContext)
        {
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
    }
}