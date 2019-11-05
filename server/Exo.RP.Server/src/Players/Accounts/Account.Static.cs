using System.Collections.Generic;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using models.Enums;
using server.BankAccounts;
using server.Database;
using server.Inventory;
using server.Inventory.Inventories;
using server.Players.Characters;

namespace server.Players.Accounts
{
    internal static class AccountStatic
    {
        public static bool CreateAccount(this IPlayer player, string username, string mail, AdminLevel adminLvl)
        {
            var accountData = new Account
            {
                Username = username,
                EMail = mail,
                AdminLvl = adminLvl,
                SocialClubName = player.SocialClubId.ToString(),
                Serial = player.HardwareIdHash.ToString(),
                Character = new Character()
                {
                    FirstName = "Fred",
                    LastName = "Feuerstein",
                    Skin = PedModel.FreemodeMale01,
                    Gender = Gender.Male,
                    Health = 100,
                    Money = 3000,
                    PosX = -1040.907f,
                    PosY = -2743.189f,
                    PosZ = 13.94503f,
                    BankAccount = new BankAccount
                    {
                        OwnerType = OwnerType.Player,
                        Money = 0
                    },
                    InventoryModel = new PlayerInventory()
                    {
                        OwnerType = OwnerType.Player,
                        Type = InventoryType.Player,
                        Bags = new Dictionary<BagNames, BagModel>()
                    },
                    FaceFeatures = new FaceFeatures()
                    {

                    }
                    
                }
            };

            //var characterData = ;

            ContextFactory.Instance.AccountModel.Local.Add(accountData);
            ContextFactory.Instance.CharacterModel.Local.Add(accountData.Character);
            ContextFactory.Instance.BankAccountModel.Local.Add(accountData.Character.BankAccount);
            ContextFactory.Instance.InventoryModel.Local.Add(accountData.Character.InventoryModel);
            ContextFactory.Instance.FaceFeaturesModel.Local.Add(accountData.Character.FaceFeatures);

            DatabaseCore.SaveChangeToDatabase();

            //player.SetData("account.id", accountData.Id);
            return true;

        }
    }
}
