﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Data;
using AltV.Net.Enums;
using models.Enums;
using Newtonsoft.Json;
using server.BankAccounts;
using server.Inventory.Inventories;
using server.Jobs;

namespace server.Players.Characters
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

        [NotMapped]
        public CharacterJobData JobData { get; set; }

        private PlayerInventory _playerInventory { get; set; }

        [Column("JobLevels")]
        public string JobLevelsSerialized
        {
            get => JsonConvert.SerializeObject(JobData);
            set => JobData = string.IsNullOrEmpty(value)
                ? new CharacterJobData()
                : JsonConvert.DeserializeObject<CharacterJobData>(value);
        }

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