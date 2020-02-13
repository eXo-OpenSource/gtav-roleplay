using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AltV.Net;
using models.Enums.Translation;
using NGettext;
using Sentry;
using server.Util.Log;

namespace server.Translation
{
    public class TranslationManager : IManager
    {
        private static readonly Logger<TranslationManager> Logger = new Logger<TranslationManager>();

        private readonly string _localDir = Path.Combine("resources", Alt.Server.Resource.Name, "translations");
        private readonly IReadOnlyList<CultureInfo> _languages = new List<CultureInfo>( new[] { new CultureInfo("en-US") } );
        private readonly IReadOnlyDictionary<CultureInfo, IReadOnlyDictionary<TranslationCatalog, Catalog>> _catalogs;

        public TranslationManager()
        {
            _catalogs =
                _languages.ToDictionary<CultureInfo, CultureInfo, IReadOnlyDictionary<TranslationCatalog, Catalog>>(
                    lang => lang,
                    lang => Enum.GetValues(typeof(TranslationCatalog)).Cast<TranslationCatalog>()
                        .ToDictionary(translationCatalog => translationCatalog,
                            translationCatalog => new Catalog(translationCatalog.ToString(), _localDir, lang)));
        }

        public string Translate(string formatString, CultureInfo lang, TranslationCatalog translationCatalog, 
            params object[] args)
        {
            try
            {
                if (_catalogs == default)
                    return string.Format(formatString, args);

                _catalogs.TryGetValue(lang, out var catalogDict);
                if (catalogDict == default)
                    return string.Format(formatString, args);

                catalogDict.TryGetValue(translationCatalog, out var catalog);
                if (catalog == default)
                    return string.Format(formatString, args);

                return catalog.GetString(formatString, args);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);

                Logger.Warn($"Translation of message '{formatString}' for '{lang}/{translationCatalog}' failed.");
                return formatString;
            }
        }
    }
}