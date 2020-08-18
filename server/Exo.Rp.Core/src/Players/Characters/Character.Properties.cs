using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Enums;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Players.Characters
{
    public partial class Character
    {
        [NotMapped]
        private static readonly Logger<Character> Logger = new Logger<Character>();

        //Logged in:
        [NotMapped]
        private IPlayer _player;
        [NotMapped]
        public bool IsLoggedIn = false;


        //Interation:
        [NotMapped]
        public string LastInteractionId;

        [NotMapped]
        public Dictionary<string, InteractionData> InteractionData = new Dictionary<string, InteractionData>();

        //Money
        [NotMapped]
        public TransferMoneyOptions DefaultMoneyTransferOptions = new TransferMoneyOptions
        {
            AllowNegative = false,
            FromBank = false,
            Silent = false,
            ToBank = false
        };

        //Other
        [NotMapped]
        public PedModel DefaultSkin;
        [NotMapped]
        public bool IsFactionDuty = false;
        //[NotMapped]
        //public List<SavedWeapon> SavedWeapons = new List<SavedWeapon>();

    }
}