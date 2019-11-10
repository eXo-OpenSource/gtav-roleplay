﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Database;

namespace server.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191110222416_accountlang2")]
    partial class accountlang2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("server.BankAccounts.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<int>("OwnerType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("server.Inventory.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BagsSerialized")
                        .HasColumnName("Bags")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerType")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Inventories");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Inventory");
                });

            modelBuilder.Entity("server.Inventory.InventoryItemsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Options")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("server.Inventory.Items.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Bag")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<sbyte>("Stackable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SubText")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("server.Peds.Ped", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<float>("PosX")
                        .HasColumnType("float");

                    b.Property<float>("PosY")
                        .HasColumnType("float");

                    b.Property<float>("PosZ")
                        .HasColumnType("float");

                    b.Property<float>("Rot")
                        .HasColumnType("float");

                    b.Property<int>("SkinId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Peds");
                });

            modelBuilder.Entity("server.Players.Accounts.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AdminLvl")
                        .HasColumnType("int");

                    b.Property<sbyte>("Autologin")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("EMail")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ForumId")
                        .HasColumnType("int");

                    b.Property<ulong>("HardwareId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Language")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("SocialClubId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("server.Players.Characters.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Armor")
                        .HasColumnType("int");

                    b.Property<int>("BankAccountId")
                        .HasColumnType("int");

                    b.Property<sbyte>("Citizenship")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("FaceFeaturesId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("JobLevelsSerialized")
                        .HasColumnName("JobLevels")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<int>("PlayingTime")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<double>("PosX")
                        .HasColumnType("double");

                    b.Property<double>("PosY")
                        .HasColumnType("double");

                    b.Property<double>("PosZ")
                        .HasColumnType("double");

                    b.Property<int>("SkinId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("FaceFeaturesId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("server.Players.Characters.FaceFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddBodyBlemishes")
                        .HasColumnType("int");

                    b.Property<int>("AddBodyBlemishesColor1")
                        .HasColumnType("int");

                    b.Property<int>("AddBodyBlemishesColor2")
                        .HasColumnType("int");

                    b.Property<int>("Ageing")
                        .HasColumnType("int");

                    b.Property<int>("AgeingColor1")
                        .HasColumnType("int");

                    b.Property<int>("AgeingColor2")
                        .HasColumnType("int");

                    b.Property<int>("Blemishes")
                        .HasColumnType("int");

                    b.Property<int>("BlemishesColor1")
                        .HasColumnType("int");

                    b.Property<int>("BlemishesColor2")
                        .HasColumnType("int");

                    b.Property<int>("Blush")
                        .HasColumnType("int");

                    b.Property<int>("BlushColor1")
                        .HasColumnType("int");

                    b.Property<int>("BlushColor2")
                        .HasColumnType("int");

                    b.Property<int>("BodyBlemishes")
                        .HasColumnType("int");

                    b.Property<int>("BodyBlemishesColor1")
                        .HasColumnType("int");

                    b.Property<int>("BodyBlemishesColor2")
                        .HasColumnType("int");

                    b.Property<int>("BrowHeight")
                        .HasColumnType("int");

                    b.Property<int>("BrowWidth")
                        .HasColumnType("int");

                    b.Property<int>("CheekboneHeight")
                        .HasColumnType("int");

                    b.Property<int>("CheekboneWidth")
                        .HasColumnType("int");

                    b.Property<int>("CheeksWidth")
                        .HasColumnType("int");

                    b.Property<int>("ChestHair")
                        .HasColumnType("int");

                    b.Property<int>("ChestHairColor1")
                        .HasColumnType("int");

                    b.Property<int>("ChestHairColor2")
                        .HasColumnType("int");

                    b.Property<int>("ChinLength")
                        .HasColumnType("int");

                    b.Property<int>("ChinPosition")
                        .HasColumnType("int");

                    b.Property<int>("ChinShape")
                        .HasColumnType("int");

                    b.Property<int>("ChinWidth")
                        .HasColumnType("int");

                    b.Property<int>("Complexion")
                        .HasColumnType("int");

                    b.Property<int>("ComplexionColor1")
                        .HasColumnType("int");

                    b.Property<int>("ComplexionColor2")
                        .HasColumnType("int");

                    b.Property<int>("EyeColor")
                        .HasColumnType("int");

                    b.Property<int>("Eyebrows")
                        .HasColumnType("int");

                    b.Property<int>("EyebrowsColor1")
                        .HasColumnType("int");

                    b.Property<int>("EyebrowsColor2")
                        .HasColumnType("int");

                    b.Property<int>("EyesWidth")
                        .HasColumnType("int");

                    b.Property<int>("FacialHair")
                        .HasColumnType("int");

                    b.Property<int>("FacialHairColor1")
                        .HasColumnType("int");

                    b.Property<int>("FacialHairColor2")
                        .HasColumnType("int");

                    b.Property<int>("Freckles")
                        .HasColumnType("int");

                    b.Property<int>("FrecklesColor1")
                        .HasColumnType("int");

                    b.Property<int>("FrecklesColor2")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Hair")
                        .HasColumnType("int");

                    b.Property<int>("HairColor")
                        .HasColumnType("int");

                    b.Property<int>("HairColorHighlight")
                        .HasColumnType("int");

                    b.Property<int>("JawHeight")
                        .HasColumnType("int");

                    b.Property<int>("JawWidth")
                        .HasColumnType("int");

                    b.Property<int>("LipsWidth")
                        .HasColumnType("int");

                    b.Property<int>("Lipstick")
                        .HasColumnType("int");

                    b.Property<int>("LipstickColor1")
                        .HasColumnType("int");

                    b.Property<int>("LipstickColor2")
                        .HasColumnType("int");

                    b.Property<int>("Makeup")
                        .HasColumnType("int");

                    b.Property<int>("MakeupColor1")
                        .HasColumnType("int");

                    b.Property<int>("MakeupColor2")
                        .HasColumnType("int");

                    b.Property<int>("NeckWidth")
                        .HasColumnType("int");

                    b.Property<int>("NoseBridge")
                        .HasColumnType("int");

                    b.Property<int>("NoseHeight")
                        .HasColumnType("int");

                    b.Property<int>("NoseLength")
                        .HasColumnType("int");

                    b.Property<int>("NoseShift")
                        .HasColumnType("int");

                    b.Property<int>("NoseTip")
                        .HasColumnType("int");

                    b.Property<int>("NoseWidth")
                        .HasColumnType("int");

                    b.Property<int>("ShapeFirst")
                        .HasColumnType("int");

                    b.Property<int>("ShapeMix")
                        .HasColumnType("int");

                    b.Property<int>("ShapeSecond")
                        .HasColumnType("int");

                    b.Property<int>("ShapeThird")
                        .HasColumnType("int");

                    b.Property<int>("SkinFirst")
                        .HasColumnType("int");

                    b.Property<int>("SkinMix")
                        .HasColumnType("int");

                    b.Property<int>("SkinSecond")
                        .HasColumnType("int");

                    b.Property<int>("SkinThird")
                        .HasColumnType("int");

                    b.Property<int>("SunDamage")
                        .HasColumnType("int");

                    b.Property<int>("SunDamageColor1")
                        .HasColumnType("int");

                    b.Property<int>("SunDamageColor2")
                        .HasColumnType("int");

                    b.Property<int>("ThirdMix")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FaceFeatures");
                });

            modelBuilder.Entity("server.Shops.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BankAccountId")
                        .HasColumnType("int");

                    b.Property<int>("Blip")
                        .HasColumnType("int(11)");

                    b.Property<string>("BlipText")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OptionsSerialized")
                        .HasColumnName("Options")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerType")
                        .HasColumnType("int");

                    b.Property<int?>("PedId")
                        .HasColumnType("int");

                    b.Property<int>("ShopType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("PedId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("server.Teams.DepartmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("server.Teams.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BankAccountId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TeamType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("server.Teams.TeamMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("server.Teams.TeamMemberPermissionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("TeamMemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("TeamMemberPermissions");
                });

            modelBuilder.Entity("server.Vehicles.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Color1")
                        .HasColumnType("int");

                    b.Property<int>("Color2")
                        .HasColumnType("int");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<sbyte>("Locked")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("Model")
                        .HasColumnType("bigint");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerType")
                        .HasColumnType("int");

                    b.Property<string>("Plate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("PosX")
                        .HasColumnType("float");

                    b.Property<float>("PosY")
                        .HasColumnType("float");

                    b.Property<float>("PosZ")
                        .HasColumnType("float");

                    b.Property<float>("RotZ")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("server.Vehicles.VehicleShopVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("PosX")
                        .HasColumnType("float");

                    b.Property<float>("PosY")
                        .HasColumnType("float");

                    b.Property<float>("PosZ")
                        .HasColumnType("float");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<float>("RotX")
                        .HasColumnType("float");

                    b.Property<float>("RotY")
                        .HasColumnType("float");

                    b.Property<float>("RotZ")
                        .HasColumnType("float");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ShopVehicles");
                });

            modelBuilder.Entity("server.WorldObjects.WorldObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlacedBy")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Rotation")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WorldObjects");
                });

            modelBuilder.Entity("server.Inventory.Inventories.PlayerInventory", b =>
                {
                    b.HasBaseType("server.Inventory.Inventory");

                    b.ToTable("Inventories");

                    b.HasDiscriminator().HasValue("PlayerInventory");
                });

            modelBuilder.Entity("server.Inventory.Inventories.TeamInventory", b =>
                {
                    b.HasBaseType("server.Inventory.Inventory");

                    b.ToTable("Inventories");

                    b.HasDiscriminator().HasValue("TeamInventory");
                });

            modelBuilder.Entity("server.Inventory.Inventories.VehicleInventory", b =>
                {
                    b.HasBaseType("server.Inventory.Inventory");

                    b.ToTable("Inventories");

                    b.HasDiscriminator().HasValue("VehicleInventory");
                });

            modelBuilder.Entity("server.Players.Accounts.Account", b =>
                {
                    b.HasOne("server.Players.Characters.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Players.Characters.Character", b =>
                {
                    b.HasOne("server.BankAccounts.BankAccount", "BankAccount")
                        .WithMany()
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Players.Characters.FaceFeatures", "FaceFeatures")
                        .WithMany()
                        .HasForeignKey("FaceFeaturesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Inventory.Inventories.PlayerInventory", "InventoryModel")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Shops.Shop", b =>
                {
                    b.HasOne("server.BankAccounts.BankAccount", "BankAccount")
                        .WithMany()
                        .HasForeignKey("BankAccountId");

                    b.HasOne("server.Peds.Ped", "PedModel")
                        .WithMany()
                        .HasForeignKey("PedId");
                });

            modelBuilder.Entity("server.Teams.DepartmentModel", b =>
                {
                    b.HasOne("server.Teams.Team", "Team")
                        .WithMany("Departments")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Teams.Team", b =>
                {
                    b.HasOne("server.BankAccounts.BankAccount", "BankAccount")
                        .WithMany()
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Inventory.Inventories.TeamInventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Teams.TeamMember", b =>
                {
                    b.HasOne("server.Players.Characters.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Teams.DepartmentModel", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Teams.Team", "Team")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Teams.TeamMemberPermissionModel", b =>
                {
                    b.HasOne("server.Teams.TeamMember", "TeamMember")
                        .WithMany()
                        .HasForeignKey("TeamMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Vehicles.Vehicle", b =>
                {
                    b.HasOne("server.Inventory.Inventories.VehicleInventory", "InventoryModel")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
