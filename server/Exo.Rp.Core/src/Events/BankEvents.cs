using AltV.Net;
using System;
using System.Collections.Generic;
using System.Text;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Events
{
    class BankEvents : IScript
    {

        [ClientEvent("BankAccount:ShowInteraction")]
        public void ShowInteraction(IPlayer player)
        {
            player.Emit("ATM:Show");
        }

        [ClientEvent("BankAccount:RefreshData")]
        public void RefreshData(IPlayer client)
        {
            client.Emit("BankAccount:UpdateData", client.GetCharacter().GetMoney(true),
                client.GetCharacter().GetMoney(false), client.GetCharacter().GetNormalizedName());
        }

        [ClientEvent("BankAccount:CashIn")]
        public void CashIn(IPlayer client, int amount)
        {
            if (client.GetCharacter().GetMoney() < amount)
            {
                client.SendError("Du hast nicht genug Geld bei dir.");
                return;
            }

            RefreshData(client);

            client.GetCharacter().GiveMoney(amount, "Maze Bank Einzahlung", true);
            client.GetCharacter().TakeMoney(amount, "Maze Bank Einzahlung", false);
            client.SendSuccess($"Du hast ${amount} eingezahlt!");
        }

        [ClientEvent("BankAccount:CashOut")]
        public void CashOut(IPlayer client, int amount)
        {
            if (client.GetCharacter().BankAccount.GetMoney() < amount)
            {
                client.SendError("Du verfügst nicht über genügend Geld auf der Bank.");
                return;
            }

            RefreshData(client);

            client.GetCharacter().GiveMoney(amount, "Maze Bank Auszahlung", false);
            client.GetCharacter().TakeMoney(amount, "Maze Bank Auszahlung", true);
            client.SendSuccess($"Du hast ${amount} ausgezahlt!");
        }
    }
}
