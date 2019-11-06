using System.Collections.Generic;
using System.Linq;
using server.Database;

namespace server.BankAccounts
{
    internal class BankAccountManager : IManager
    {
        private readonly Dictionary<int, BankAccount> _accounts;

        public BankAccountManager()
        {
            _accounts = new Dictionary<int, BankAccount>();

            if (!ContextFactory.Instance.BankAccountModel.Local.Any()) return;

            foreach (var account in ContextFactory.Instance.BankAccountModel.Local)
                _accounts.Add(account.Id, account);
        }

        public BankAccount GetAccount(int id)
        {
            return _accounts.TryGetValue(id, out var account) ? account : null;
        }
    }
}