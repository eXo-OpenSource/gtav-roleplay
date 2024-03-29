using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Exo.Rp.Core.Players;
using Exo.Rp.Models.Enums.Translation;

namespace Exo.Rp.Core.Translation
{
    public static class T
    {
        public static string _([NotNull] string formatString, [NotNull] IPlayer player, params object[] formatStringArgs)
        {
            return _(formatString, player?.GetAccount()?.GetLanguage(), formatStringArgs);
        }

        public static string _([NotNull] string formatString, [NotNull] IPlayer player, TranslationCatalog catalog,
            params object[] formatStringArgs)
        {
            return _(formatString, player?.GetAccount()?.GetLanguage(), catalog, formatStringArgs);
        }

        public static string _([NotNull] string formatString, [NotNull] CultureInfo lang, params object[] formatStringArgs)
        {
            return _(formatString, lang, TranslationCatalog.General, formatStringArgs);
        }

        public static string _([NotNull] string formatString, [NotNull] CultureInfo lang, [NotNull] TranslationCatalog catalog, params object[] formatStringArgs)
        {
            return Core.GetService<TranslationManager>().Translate(formatString, lang ?? new CultureInfo("de-DE"), catalog, formatStringArgs);
        }
    }
}