using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Data;
using AltV.Net.Enums;
using Exo.Rp.Core.BankAccounts;
using Exo.Rp.Core.Inventory.Inventories;
using Exo.Rp.Core.Jobs;
using Exo.Rp.Models.Enums;
using Newtonsoft.Json;

namespace Exo.Rp.Core.Players.Characters
{
    [Table("Characters")]
    public partial class Character
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public int SkinId { get; set; }
        public int Money { get; set; }
        public int PlayingTime { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }

        [Column(TypeName = "tinyint(1)")]
        public bool Citizenship { get; set; }

        public int Health { get; set; }
        public int Armor { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }

        [ForeignKey("BankAccount")]
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        [ForeignKey("InventoryModel")]
        public int InventoryId { get; set; }

        public PlayerInventory InventoryModel { get; set; }

        //[ForeignKey("AccountId")]
        //public AccountModel AccountModel { get; set; }

        //public int InventoryId { get; set; }
        public int JobId { get; set; }

        [ForeignKey("FaceFeatures")]
        public int FaceFeaturesId { get; set; }

        public FaceFeatures FaceFeatures { get; set; }

        [ForeignKey("Licenses")]
        public int LicensesId { get; set; }

        public Licenses Licenses { get; set; }

        [NotMapped]
        public CharacterJobData JobData {
            get => string.IsNullOrEmpty(JobLevelsSerialized) || JobLevelsSerialized == "null"
                    ? new CharacterJobData()
                    : JsonConvert.DeserializeObject<CharacterJobData>(JobLevelsSerialized);

            set => JobLevelsSerialized = JsonConvert.SerializeObject(value);
        }

        private PlayerInventory _playerInventory { get; set; }

        [Column("JobLevels")]
        public string JobLevelsSerialized { get; set; }

        [NotMapped]
        public PedModel Skin
        {
            get => (PedModel) SkinId;
            set => SkinId = (int) value;
        }

        [NotMapped]
        public PlayerInventory Inventory
        {
            get => InventoryModel;
            set
            {
                _playerInventory = value;
                InventoryModel = value;
            }
        }

        [NotMapped] public string FullName => FirstName + " " + LastName;

        [NotMapped]
        public Position Pos
        {
            get => new Position((float)PosX, (float)PosY, (float)PosZ);
            set
            {
                PosX = value.X;
                PosY = value.Y;
                PosZ = value.Z;
            }
        }

    }
}