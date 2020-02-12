namespace models.Enums
{
    // TODO: Need to be moved out of enums namespace
    public class TransferMoneyOptions
    {
        public bool AllowNegative = false;
        public bool FromBank = false; // Only for Players
        public bool Silent = false;
        public bool ToBank = false; // Only for Players
    }
}