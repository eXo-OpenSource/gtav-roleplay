using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AltV.Net.Data;
using AltV.Net.Enums;
using Exo.Rp.Core.Extensions;
using Exo.Rp.Core.Inventory.Inventories;
using Exo.Rp.Core.Jobs;
using Exo.Rp.Core.Shops;
using Exo.Rp.Core.Teams;
using Exo.Rp.Models.Enums;
using Newtonsoft.Json;
using Team = Exo.Rp.Core.Teams.Team;

namespace Exo.Rp.Core.Players.Characters
{
    public partial class Character
    {

        public void Login(IPlayer player)
        {
            _player = player;
            //player.Name = FullName;
            _player.SetPublicSync("dsf", "sdgf");

            IsLoggedIn = true;
            Init();
        }

        public void Logout()
        {
            Save();
            _player = null;
            IsLoggedIn = false;
        }

        public void Init()
        {
            _player.Spawn(Pos);
            _player.Position = Pos;
            _player.Health = (ushort)Health;
            _player.Model = Convert.ToUInt32(Skin);
            Console.Write(FirstName + " " + LastName + " ist gespawnt!");
            /*
            _player.SetPublicSync("player.data", new PlayerDto()
            {
                Id = Id,
                Firstname = FirstName,
                Lastname = LastName,
                UserName = _account.Username,
                Gender = Gender
            });

            PedManager.SendToIPlayer(_player);
            */
            InventoryModel.Load();

            LoadTeams();

            DefaultSkin = Skin;
            /*
            Task.Delay(50).ContinueWith(_ => // Don´t know why this is needed
            {
                ResetSkin();
                _player.SetSyncedMetaData("player:faceFeatures", JsonConvert.SerializeObject(new FaceFeaturesDto()
            {
                Gender = FaceFeatures.Gender,
                Hair = FaceFeatures.Hair,
                HairColor = FaceFeatures.HairColor,
                HairColorHighlight = FaceFeatures.HairColorHighlight,
                EyeColor = FaceFeatures.EyeColor,
                Face = new Face()
                {
                    NoseWidth = FaceFeatures.NoseWidth,
                    NoseHeight = FaceFeatures.NoseHeight,
                    NoseLength = FaceFeatures.NoseLength,
                    NoseBridge = FaceFeatures.NoseBridge,
                    NoseTip = FaceFeatures.NoseTip,
                    NoseShift = FaceFeatures.NoseShift,
                    BrowWidth = FaceFeatures.BrowWidth,
                    BrowHeight = FaceFeatures.BrowHeight,
                    CheekboneHeight = FaceFeatures.CheekboneHeight,
                    CheekboneWidth = FaceFeatures.CheekboneWidth,
                    CheeksWidth = FaceFeatures.CheeksWidth,
                    EyesWidth = FaceFeatures.EyesWidth,
                    LipsWidth = FaceFeatures.LipsWidth,
                    JawWidth = FaceFeatures.JawWidth,
                    JawHeight = FaceFeatures.JawHeight,
                    ChinLength = FaceFeatures.ChinLength,
                    ChinPosition = FaceFeatures.ChinPosition,
                    ChinWidth = FaceFeatures.ChinWidth,
                    ChinShape = FaceFeatures.ChinShape,
                    NeckWidth = FaceFeatures.NeckWidth
                },
                Parents = new Parents()
                {
                    ShapeFirst = FaceFeatures.ShapeFirst,
                    ShapeSecond = FaceFeatures.ShapeSecond,
                    ShapeThird = FaceFeatures.ShapeThird,
                    SkinFirst = FaceFeatures.SkinFirst,
                    SkinSecond = FaceFeatures.SkinSecond,
                    SkinThird = FaceFeatures.SkinThird,
                    ShapeMix = FaceFeatures.ShapeMix,
                    SkinMix = FaceFeatures.SkinMix,
                    ThirdMix = FaceFeatures.ThirdMix
                },
                Overlay = new Overlay()
                {
                    Blemishes = FaceFeatures.Blemishes,
                    FacialHair = FaceFeatures.FacialHair,
                    Eyebrows = FaceFeatures.Eyebrows,
                    Ageing = FaceFeatures.Ageing,
                    Makeup = FaceFeatures.Makeup,
                    Blush = FaceFeatures.Blush,
                    Complexion = FaceFeatures.Complexion,
                    SunDamage = FaceFeatures.SunDamage,
                    Lipstick = FaceFeatures.Lipstick,
                    Freckles = FaceFeatures.Freckles,
                    ChestHair = FaceFeatures.ChestHair,
                    BodyBlemishes = FaceFeatures.BodyBlemishes,
                    AddBodyBlemishes = FaceFeatures.AddBodyBlemishes
                },
                OverlayColor = new OverlayColor()
                {
                    Blemishes = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.BlemishesColor1,
                        Color2 = FaceFeatures.BlemishesColor2
                    },
                    FacialHair = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.FacialHairColor1,
                        Color2 = FaceFeatures.FacialHairColor2
                    },
                    Eyebrows = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.EyebrowsColor1,
                        Color2 = FaceFeatures.EyebrowsColor2
                    },
                    Ageing = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.AgeingColor1,
                        Color2 = FaceFeatures.AgeingColor2
                    },
                    Makeup = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.MakeupColor1,
                        Color2 = FaceFeatures.MakeupColor2
                    },
                    Blush = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.BlushColor1,
                        Color2 = FaceFeatures.BlushColor2
                    },
                    Complexion = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.ComplexionColor1,
                        Color2 = FaceFeatures.ComplexionColor2
                    },
                    SunDamage = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.SunDamageColor1,
                        Color2 = FaceFeatures.SunDamageColor2
                    },
                    Lipstick = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.LipstickColor1,
                        Color2 = FaceFeatures.LipstickColor2
                    },
                    Freckles = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.FrecklesColor1,
                        Color2 = FaceFeatures.FrecklesColor2
                    },
                    ChestHair = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.ChestHairColor1,
                        Color2 = FaceFeatures.ChestHairColor2
                    },
                    BodyBlemishes = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.BodyBlemishesColor1,
                        Color2 = FaceFeatures.BodyBlemishesColor2
                    },
                    AddBodyBlemishes = new shared.Characters.Color()
                    {
                        Color1 = FaceFeatures.AddBodyBlemishesColor1,
                        Color2 = FaceFeatures.AddBodyBlemishesColor2
                    }
                }
            }));
            });
            */
            //_player.Nametag = GetNormalizedName();
            //_player.Name = GetNormalizedName();

            SyncFaceFeatures();

            Logger.Debug("SEND TEAMS to " + _player.Name);
            // PedManager.Instance.SendToIPlayer(player);
            Logger.Debug("SEND IPL's to " + _player.Name);
            //Logger.Debug("SEND INVENTORY to " + _player.Name);
            //InventoryModel.SyncInventory(_player);
            Logger.Debug("APPLY FACEFEATURES to " + _player.Name);
            //_player.SetElementData("player:FaceFeatures", FaceFeatures);
            // EntityExtensions.TriggerElementDatas(player);

            _player.UpdateHud();
            _player.ShowRadar(false);
            _player.RequestDefaulIpls();
        }

        public void Save()
        {
            Pos = _player.Position;
            Health = _player.Health;
            Skin = DefaultSkin;

        }

        public void LoadTeams()
        {


        }

        public string GetNormalizedName(bool swap = false)
        {
            return swap ? $"{LastName} {FirstName}" : $"{FirstName} {LastName}";
        }

        public byte GetWantedLevel()
        {
            return 0; // ToDo
        }

        public List<Team> GetTeams()
        {
            return Core.GetService<TeamManager>().GetTeamsForPlayer(this);
        }

        public void SyncFaceFeatures()
        {
            var ff = _player.GetCharacter().FaceFeatures;

            object[] data = {
                ff.Gender, ff.ShapeFirst, ff.ShapeSecond, ff.ShapeThird,
                ff.SkinFirst, ff.SkinSecond, ff.SkinThird, ff.ShapeMix, ff.SkinMix,
                ff.Freckles, ff.EyeColor, ff.Hair, ff.HairColor, ff.HairColorHighlight,
                ff.Eyebrows, ff.EyebrowsColor1, ff.Ageing, ff.FacialHair, ff.FacialHairColor1
            };

            _player.SetSyncedMetaData("faceFeatures.Data", JsonConvert.SerializeObject(data));
        }

        public PlayerInventory GetInventory()
        {
            return Inventory;
        }

        #region Weapon

        private void SaveWeapons()
        {
            // var weapons = _player.Weapon;
            /* Not implemented yet
            foreach (WeaponModel i in weapons)
            {
                properties.SavedWeapons.Add(new SavedWeapon { weapon = i, ammo = player.GetWeaponAmmo(i) });
            }
            */
        }

        private void RestoreWeapons()
        {
            // foreach (var i in SavedWeapons) _player.GiveWeapon(i.Weapon, i.Ammo);
            //var savedWeapons = new List<SavedWeapon>();
        }

        #endregion

        #region Skin

        public void SetPermanentSkin(PedModel newSkin, bool weapons = true)
        {
            if (weapons) SaveWeapons();
            _player.Model = (uint)newSkin;
            DefaultSkin = newSkin;
            if (weapons) RestoreWeapons();
        }

        public void SetTemporarySkin(PedModel newSkin, bool weapons = true)
        {
            if (weapons) SaveWeapons();
            DefaultSkin = (PedModel) _player.Model;
            _player.Model = (uint)newSkin;
            if (weapons) RestoreWeapons();
        }

        public void ResetSkin()
        {
            _player.Model = (uint)DefaultSkin;
        }

        #endregion

        #region Money

        public int GetMoney(bool bank = false)
        {
            return bank ? BankAccount.GetMoney() : Money;
        }

        // TODO: change later to private back
        public bool GiveMoney(int amount, string reason, bool bank = false, bool silent = false)
        {
            if (bank) return BankAccount.GiveMoney(amount);

            Money = GetMoney() + amount;
            _player.UpdateHud();
            return true;
        }

        // TODO: change later to private back
        public bool TakeMoney(int amount, string reason, bool bank = false, bool silent = false)
        {
            if (bank) return BankAccount.TakeMoney(amount);

            if (Money < amount) return false;

            Money = GetMoney() - amount;
            _player.UpdateHud();
            return true;
        }

        public bool TransferMoneyToPlayer(Character target, int amount, string reason, MoneyTransferCategory category,
            MoneyTransferSubCategory subcategory, [Optional] TransferMoneyOptions options)
        {
            if (options == null) options = DefaultMoneyTransferOptions;

            if (!TakeMoney(amount, reason, options.FromBank, options.Silent)) return false;
            target.GiveMoney(amount, reason, options.ToBank, options.Silent);
            return true;
        }

        public bool TransferMoneyToTeam(Team target, int amount, string reason, MoneyTransferCategory category,
            MoneyTransferSubCategory subcategory, [Optional] TransferMoneyOptions options)
        {
            if (options == null) options = DefaultMoneyTransferOptions;

            if (!TakeMoney(amount, reason, options.FromBank, options.Silent)) return false;
            target.GiveMoney(amount, reason, options.Silent);
            return true;
        }

        public bool TransferMoneyToShop(Shop target, int amount, string reason, MoneyTransferCategory category,
            MoneyTransferSubCategory subcategory, [Optional] TransferMoneyOptions options)
        {
            if (options == null) options = DefaultMoneyTransferOptions;

            if (!TakeMoney(amount, reason, options.FromBank, options.Silent)) return false;
            target.GiveMoney(amount, reason, options.Silent);

            return true;
        }

        #endregion

        #region Job

        public Job GetJob()
        {
            return JobId > 0 ? Core.GetService<JobManager>().GetJob(JobId) : null; // Todo WTF?
            return null;
        }

        public void SetJob(Job job)
        {
            JobId = job?.JobId ?? 0; // Todo WTF?
            Save();
        }

        public bool IsJobActive()
        {
            _player.GetSyncedMetaData("Job:Active", out bool active);
            return active;
        }

        public void SetJobActive(bool state)
        {
            _player.SetSyncedMetaData("Job:Active", state);
        }

        public bool IsJobCurrentAndActive<T>()
            where T : Job
        {
            return GetJob().GetType() == typeof(T) && IsJobActive();
        }

        #endregion

        #region marker

        public void CreateMarker(Position position, int markerType = 0, int markerScale = 1, int colshapeRange = 1,
            Action<IPlayer> colEnter = null, Action<IPlayer> colExit = null)
        {
            /*var markerId = MarkerManager.Instance.AddMarker(new MarkerAction(colEnter, colExit));
            _player.Emit("player:createMarker", _player, position, markerId, markerType, markerScale,
                colshapeRange);*/
        }

        public void DeleteMarker(Position position)
        {
            _player.Emit("player:deleteMarker", _player, position);
        }

        #endregion
    }
}