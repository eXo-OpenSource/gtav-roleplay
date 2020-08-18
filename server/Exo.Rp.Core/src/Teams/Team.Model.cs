using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exo.Rp.Core.BankAccounts;
using Exo.Rp.Core.Inventory.Inventories;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Teams
{
    [Table("Teams")]
    public partial class Team {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TeamType TeamType { get; set; }

        [ForeignKey("BankAccount")]
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public TeamInventory Inventory { get; set; }

        public List<DepartmentModel> Departments { get; set; }
        public List<TeamMember> TeamMembers { get; set; }

        /*
        [NotMapped]
        public BankAccount.BankAccount BankAccount
        {
            get => BankAccountManager.Instance.GetOrCreateAccount(BankAccountId, ownerType.Team, id);
            set
            {
                if (BankAccountId > 0)
                {
                    BankAccountId = value.Data.id;
                }
                else
                {
                    var nBankAccount =
                        BankAccountManager.Instance.GetOrCreateAccount(BankAccountId, ownerType.Team, id);
                    BankAccountId = nBankAccount.Data.id;
                }
            }
        }

        [NotMapped]
        public TeamInventory Inventory
        {
            get => TeamInventoryStatic.GetOrCreateTeamInventory(inventoryId, id);
            set => inventoryId = value.Data.id;
        }
        */
    }
}