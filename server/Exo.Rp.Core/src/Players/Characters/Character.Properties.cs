using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Enums;
using models.Enums;
using server.Util.Log;

namespace server.Players.Characters
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
        public int LastInteractionId;

        [NotMapped]
        public Dictionary<int, InteractionData> InteractionData = new Dictionary<int, InteractionData>();

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