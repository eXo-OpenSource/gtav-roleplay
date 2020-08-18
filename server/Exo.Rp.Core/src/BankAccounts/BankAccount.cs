using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.BankAccounts
{
    [Table("BankAccounts")]
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }

        public OwnerType OwnerType { get; set; }

        /*
        [ForeignKey("CharacterId")]
        public CharacterModel Character { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
        */

        public int Money { get; set; }

        #region Functions

        public int GetMoney() => Money;

        public bool GiveMoney(int amount)
        {
            Money += amount;
            return true;
        }

        /*
        * Was in old class "Data.Money = Data.Money + amount;".
        * Unintended?
        */
        public bool TakeMoney(int amount)
        {
            if (Money < amount)
                return false;

            Money -= amount;
            return true;
        }

        #endregion

    }
}