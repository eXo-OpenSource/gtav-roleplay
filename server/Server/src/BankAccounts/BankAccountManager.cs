using System.Collections.Generic;
using System.Linq;
using server.Database;

namespace server.BankAccounts
{
    internal class BankAccountManager
    {
        private static readonly Dictionary<int, BankAccount> _accounts;

        static BankAccountManager()
        {
            _accounts = new Dictionary<int, BankAccount>();

            if (!ContextFactory.Instance.BankAccountModel.Local.Any()) return;

            foreach (var account in ContextFactory.Instance.BankAccountModel.Local)
                _accounts.Add(account.Id, account);
        }

        public static BankAccount GetAccount(int id)
        {
            return _accounts.TryGetValue(id, out var account) ? account : null;
        }
    }
}