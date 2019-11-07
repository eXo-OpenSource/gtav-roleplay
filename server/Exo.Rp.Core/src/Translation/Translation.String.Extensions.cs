using System.Globalization;
using models.Enums;
using models.Enums.Translation;
using server.Players;

namespace server.Translation
{
    public static class TranslationStringExtensions
    {
        public static string Translate(this string formatString, IPlayer player, params object[] formatStringArgs)
        {
            return formatString.Translate(player.GetAccount().Culture, formatStringArgs);
        }

        public static string Translate(this string formatString, IPlayer player, TranslationCatalog catalog, params object[] formatStringArgs)
        {
            return formatString.Translate(player.GetAccount().Culture, catalog, formatStringArgs);
        }

        public static string Translate(this string formatString, CultureInfo lang, params object[] formatStringArgs)
        {
            return formatString.Translate(lang, TranslationCatalog.General, formatStringArgs);
        }

        public static string Translate(this string formatString, CultureInfo lang, TranslationCatalog catalog, params object[] formatStringArgs)
        {
            return Core.GetService<TranslationManager>().Translate(formatString, lang, catalog, formatStringArgs);
        }
    }
}