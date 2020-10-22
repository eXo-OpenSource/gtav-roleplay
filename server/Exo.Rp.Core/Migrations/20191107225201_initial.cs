using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exo.Rp.Core.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder),
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    OwnerType = table.Column<int>(nullable: false),,
                    Money = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id),;
                }),;

            migrationBuilder.CreateTable(
                name: "FaceFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Gender = table.Column<int>(nullable: false),,
                    Hair = table.Column<int>(nullable: false),,
                    HairColor = table.Column<int>(nullable: false),,
                    HairColorHighlight = table.Column<int>(nullable: false),,
                    EyeColor = table.Column<int>(nullable: false),,
                    NoseWidth = table.Column<int>(nullable: false),,
                    NoseHeight = table.Column<int>(nullable: false),,
                    NoseLength = table.Column<int>(nullable: false),,
                    NoseBridge = table.Column<int>(nullable: false),,
                    NoseTip = table.Column<int>(nullable: false),,
                    NoseShift = table.Column<int>(nullable: false),,
                    BrowWidth = table.Column<int>(nullable: false),,
                    BrowHeight = table.Column<int>(nullable: false),,
                    CheekboneHeight = table.Column<int>(nullable: false),,
                    CheekboneWidth = table.Column<int>(nullable: false),,
                    CheeksWidth = table.Column<int>(nullable: false),,
                    EyesWidth = table.Column<int>(nullable: false),,
                    LipsWidth = table.Column<int>(nullable: false),,
                    JawWidth = table.Column<int>(nullable: false),,
                    JawHeight = table.Column<int>(nullable: false),,
                    ChinLength = table.Column<int>(nullable: false),,
                    ChinPosition = table.Column<int>(nullable: false),,
                    ChinWidth = table.Column<int>(nullable: false),,
                    ChinShape = table.Column<int>(nullable: false),,
                    NeckWidth = table.Column<int>(nullable: false),,
                    ShapeFirst = table.Column<int>(nullable: false),,
                    ShapeSecond = table.Column<int>(nullable: false),,
                    ShapeThird = table.Column<int>(nullable: false),,
                    SkinFirst = table.Column<int>(nullable: false),,
                    SkinSecond = table.Column<int>(nullable: false),,
                    SkinThird = table.Column<int>(nullable: false),,
                    ShapeMix = table.Column<int>(nullable: false),,
                    SkinMix = table.Column<int>(nullable: false),,
                    ThirdMix = table.Column<int>(nullable: false),,
                    Blemishes = table.Column<int>(nullable: false),,
                    FacialHair = table.Column<int>(nullable: false),,
                    Eyebrows = table.Column<int>(nullable: false),,
                    Ageing = table.Column<int>(nullable: false),,
                    Makeup = table.Column<int>(nullable: false),,
                    Blush = table.Column<int>(nullable: false),,
                    Complexion = table.Column<int>(nullable: false),,
                    SunDamage = table.Column<int>(nullable: false),,
                    Lipstick = table.Column<int>(nullable: false),,
                    Freckles = table.Column<int>(nullable: false),,
                    ChestHair = table.Column<int>(nullable: false),,
                    BodyBlemishes = table.Column<int>(nullable: false),,
                    AddBodyBlemishes = table.Column<int>(nullable: false),,
                    BlemishesColor1 = table.Column<int>(nullable: false),,
                    FacialHairColor1 = table.Column<int>(nullable: false),,
                    EyebrowsColor1 = table.Column<int>(nullable: false),,
                    AgeingColor1 = table.Column<int>(nullable: false),,
                    MakeupColor1 = table.Column<int>(nullable: false),,
                    BlushColor1 = table.Column<int>(nullable: false),,
                    ComplexionColor1 = table.Column<int>(nullable: false),,
                    SunDamageColor1 = table.Column<int>(nullable: false),,
                    LipstickColor1 = table.Column<int>(nullable: false),,
                    FrecklesColor1 = table.Column<int>(nullable: false),,
                    ChestHairColor1 = table.Column<int>(nullable: false),,
                    BodyBlemishesColor1 = table.Column<int>(nullable: false),,
                    AddBodyBlemishesColor1 = table.Column<int>(nullable: false),,
                    BlemishesColor2 = table.Column<int>(nullable: false),,
                    FacialHairColor2 = table.Column<int>(nullable: false),,
                    EyebrowsColor2 = table.Column<int>(nullable: false),,
                    AgeingColor2 = table.Column<int>(nullable: false),,
                    MakeupColor2 = table.Column<int>(nullable: false),,
                    BlushColor2 = table.Column<int>(nullable: false),,
                    ComplexionColor2 = table.Column<int>(nullable: false),,
                    SunDamageColor2 = table.Column<int>(nullable: false),,
                    LipstickColor2 = table.Column<int>(nullable: false),,
                    FrecklesColor2 = table.Column<int>(nullable: false),,
                    ChestHairColor2 = table.Column<int>(nullable: false),,
                    BodyBlemishesColor2 = table.Column<int>(nullable: false),,
                    AddBodyBlemishesColor2 = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceFeatures", x => x.Id),;
                }),;

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Type = table.Column<int>(nullable: false),,
                    OwnerType = table.Column<int>(nullable: false),,
                    OwnerId = table.Column<int>(nullable: false),,
                    Bags = table.Column<string>(nullable: true),,
                    Discriminator = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id),;
                }),;

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    ItemId = table.Column<int>(nullable: false),,
                    InventoryId = table.Column<int>(nullable: false),,
                    Amount = table.Column<int>(nullable: false),,
                    Options = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id),;
                }),;

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Name = table.Column<string>(nullable: true),,
                    SubText = table.Column<string>(nullable: true),,
                    Icon = table.Column<string>(nullable: true),,
                    Bag = table.Column<int>(nullable: false),,
                    Stackable = table.Column<sbyte>(type: "tinyint(1),", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id),;
                }),;

            migrationBuilder.CreateTable(
                name: "Peds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Name = table.Column<string>(nullable: true),,
                    SkinId = table.Column<int>(nullable: false),,
                    PosX = table.Column<float>(nullable: false),,
                    PosY = table.Column<float>(nullable: false),,
                    PosZ = table.Column<float>(nullable: false),,
                    Rot = table.Column<float>(nullable: false),,
                    Type = table.Column<int>(nullable: false),,
                    ObjectId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peds", x => x.Id),;
                }),;

            migrationBuilder.CreateTable(
                name: "ShopVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    ModelName = table.Column<string>(nullable: true),,
                    PosX = table.Column<float>(nullable: false),,
                    PosY = table.Column<float>(nullable: false),,
                    PosZ = table.Column<float>(nullable: false),,
                    RotX = table.Column<float>(nullable: false),,
                    RotY = table.Column<float>(nullable: false),,
                    RotZ = table.Column<float>(nullable: false),,
                    Price = table.Column<int>(nullable: false),,
                    ShopId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopVehicles", x => x.Id),;
                }),;

            migrationBuilder.CreateTable(
                name: "WorldObjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Type = table.Column<int>(nullable: false),,
                    Position = table.Column<string>(nullable: true),,
                    Rotation = table.Column<string>(nullable: true),,
                    PlacedBy = table.Column<int>(nullable: false),,
                    Date = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorldObjects", x => x.Id),;
                }),;

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    FirstName = table.Column<string>(nullable: true),,
                    LastName = table.Column<string>(nullable: true),,
                    Gender = table.Column<int>(nullable: false),,
                    SkinId = table.Column<int>(nullable: false),,
                    Money = table.Column<int>(nullable: false),,
                    PlayingTime = table.Column<int>(nullable: false),,
                    Points = table.Column<int>(nullable: false),,
                    Level = table.Column<int>(nullable: false),,
                    Citizenship = table.Column<sbyte>(type: "tinyint(1),", nullable: false),,
                    Health = table.Column<int>(nullable: false),,
                    Armor = table.Column<int>(nullable: false),,
                    PosX = table.Column<double>(nullable: false),,
                    PosY = table.Column<double>(nullable: false),,
                    PosZ = table.Column<double>(nullable: false),,
                    BankAccountId = table.Column<int>(nullable: false),,
                    InventoryId = table.Column<int>(nullable: false),,
                    JobId = table.Column<int>(nullable: false),,
                    FaceFeaturesId = table.Column<int>(nullable: false),,
                    JobLevels = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id),;
                    table.ForeignKey(
                        name: "FK_Characters_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                    table.ForeignKey(
                        name: "FK_Characters_FaceFeatures_FaceFeaturesId",
                        column: x => x.FaceFeaturesId,
                        principalTable: "FaceFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                    table.ForeignKey(
                        name: "FK_Characters_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                }),;

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Name = table.Column<string>(nullable: true),,
                    Description = table.Column<string>(nullable: true),,
                    TeamType = table.Column<int>(nullable: false),,
                    BankAccountId = table.Column<int>(nullable: false),,
                    InventoryId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id),;
                    table.ForeignKey(
                        name: "FK_Teams_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                    table.ForeignKey(
                        name: "FK_Teams_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                }),;

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Model = table.Column<long>(nullable: false),,
                    OwnerType = table.Column<int>(nullable: false),,
                    OwnerId = table.Column<int>(nullable: false),,
                    PosX = table.Column<float>(nullable: false),,
                    PosY = table.Column<float>(nullable: false),,
                    PosZ = table.Column<float>(nullable: false),,
                    RotZ = table.Column<float>(nullable: false),,
                    Color1 = table.Column<int>(nullable: false),,
                    Color2 = table.Column<int>(nullable: false),,
                    Plate = table.Column<string>(nullable: true),,
                    Locked = table.Column<sbyte>(type: "tinyint(1),", nullable: false),,
                    InventoryId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id),;
                    table.ForeignKey(
                        name: "FK_Vehicles_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                }),;

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Name = table.Column<string>(nullable: true),,
                    ShopType = table.Column<int>(nullable: false),,
                    OwnerType = table.Column<int>(nullable: false),,
                    OwnerId = table.Column<int>(nullable: false),,
                    Blip = table.Column<int>(type: "int(11),", nullable: false),,
                    BlipText = table.Column<string>(nullable: true),,
                    BankAccountId = table.Column<int>(nullable: true),,
                    PedId = table.Column<int>(nullable: true),,
                    Options = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id),;
                    table.ForeignKey(
                        name: "FK_Shops_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict),;
                    table.ForeignKey(
                        name: "FK_Shops_Peds_PedId",
                        column: x => x.PedId,
                        principalTable: "Peds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict),;
                }),;

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    SocialClubId = table.Column<ulong>(nullable: false),,
                    HardwareId = table.Column<ulong>(nullable: false),,
                    Username = table.Column<string>(nullable: true),,
                    EMail = table.Column<string>(nullable: true),,
                    AdminLvl = table.Column<int>(nullable: false),,
                    ForumId = table.Column<int>(nullable: false),,
                    CharacterId = table.Column<int>(nullable: false),,
                    Autologin = table.Column<sbyte>(type: "tinyint(1),", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id),;
                    table.ForeignKey(
                        name: "FK_Accounts_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                }),;

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Name = table.Column<string>(nullable: true),,
                    Description = table.Column<string>(nullable: true),,
                    Rank = table.Column<int>(nullable: false),,
                    TeamId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id),;
                    table.ForeignKey(
                        name: "FK_Departments_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                }),;

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    Rank = table.Column<int>(nullable: false),,
                    TeamId = table.Column<int>(nullable: false),,
                    DepartmentId = table.Column<int>(nullable: false),,
                    CharacterId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id),;
                    table.ForeignKey(
                        name: "FK_TeamMembers_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                    table.ForeignKey(
                        name: "FK_TeamMembers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                }),;

            migrationBuilder.CreateTable(
                name: "TeamMemberPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),,
                    TeamMemberId = table.Column<int>(nullable: false),,
                    PermissionId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMemberPermissions", x => x.Id),;
                    table.ForeignKey(
                        name: "FK_TeamMemberPermissions_TeamMembers_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalTable: "TeamMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade),;
                }),;

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CharacterId",
                table: "Accounts",
                column: "CharacterId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Characters_BankAccountId",
                table: "Characters",
                column: "BankAccountId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Characters_FaceFeaturesId",
                table: "Characters",
                column: "FaceFeaturesId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Characters_InventoryId",
                table: "Characters",
                column: "InventoryId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Departments_TeamId",
                table: "Departments",
                column: "TeamId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Shops_BankAccountId",
                table: "Shops",
                column: "BankAccountId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Shops_PedId",
                table: "Shops",
                column: "PedId"),;

            migrationBuilder.CreateIndex(
                name: "IX_TeamMemberPermissions_TeamMemberId",
                table: "TeamMemberPermissions",
                column: "TeamMemberId"),;

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_CharacterId",
                table: "TeamMembers",
                column: "CharacterId"),;

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_DepartmentId",
                table: "TeamMembers",
                column: "DepartmentId"),;

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Teams_BankAccountId",
                table: "Teams",
                column: "BankAccountId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Teams_InventoryId",
                table: "Teams",
                column: "InventoryId"),;

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_InventoryId",
                table: "Vehicles",
                column: "InventoryId"),;
        }

        protected override void Down(MigrationBuilder migrationBuilder),
        {
            migrationBuilder.DropTable(
                name: "Accounts"),;

            migrationBuilder.DropTable(
                name: "InventoryItems"),;

            migrationBuilder.DropTable(
                name: "Items"),;

            migrationBuilder.DropTable(
                name: "Shops"),;

            migrationBuilder.DropTable(
                name: "ShopVehicles"),;

            migrationBuilder.DropTable(
                name: "TeamMemberPermissions"),;

            migrationBuilder.DropTable(
                name: "Vehicles"),;

            migrationBuilder.DropTable(
                name: "WorldObjects"),;

            migrationBuilder.DropTable(
                name: "Peds"),;

            migrationBuilder.DropTable(
                name: "TeamMembers"),;

            migrationBuilder.DropTable(
                name: "Characters"),;

            migrationBuilder.DropTable(
                name: "Departments"),;

            migrationBuilder.DropTable(
                name: "FaceFeatures"),;

            migrationBuilder.DropTable(
                name: "Teams"),;

            migrationBuilder.DropTable(
                name: "BankAccounts"),;

            migrationBuilder.DropTable(
                name: "Inventories"),;
        }
    }
}