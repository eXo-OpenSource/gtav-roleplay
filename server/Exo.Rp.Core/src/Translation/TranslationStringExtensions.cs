using System.Globalization;
using models.Enums.Translation;
using server.Players;

namespace server.Translation
{
	public static class T
	{
		public static string _(string formatString, IPlayer player, params object[] formatStringArgs)
		{
			return _(formatString, player?.GetAccount()?.GetLanguage(), formatStringArgs);
		}

		public static string _(string formatString, IPlayer player, TranslationCatalog catalog,
			params object[] formatStringArgs)
		{
			return _(formatString, player?.GetAccount()?.GetLanguage(), catalog, formatStringArgs);
		}

		public static string _(string formatString, CultureInfo lang, params object[] formatStringArgs)
		{
			return _(formatString, lang, TranslationCatalog.General, formatStringArgs);
		}

		public static string _(string formatString, CultureInfo lang, TranslationCatalog catalog, params object[] formatStringArgs)
		{
			return Core.GetService<TranslationManager>().Translate(formatString, lang ?? new CultureInfo("de-DE"), catalog, formatStringArgs);
		}
	}
}
