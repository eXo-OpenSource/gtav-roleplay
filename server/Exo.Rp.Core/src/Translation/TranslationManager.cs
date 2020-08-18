using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AltV.Net;
using Exo.Rp.Sdk;
using models.Enums.Translation;
using NGettext;
using Sentry;
using server.Util;
using server.Util.Log;

namespace server.Translation
{
    public class TranslationManager : IManager
    {
        private readonly ILogger<TranslationManager> _logger;

        private readonly string _localDir = Path.Combine("resources", Alt.Server.Resource.Name, "translations");
        private readonly IReadOnlyList<CultureInfo> _languages = new List<CultureInfo>( new[] { new CultureInfo("en-US") } );
        private readonly IReadOnlyDictionary<CultureInfo, IReadOnlyDictionary<TranslationCatalog, Catalog>> _catalogs;

        public TranslationManager(ILogger<TranslationManager> logger)
        {
            _logger = logger;
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
                return catalog == default ? string.Format(formatString, args) : catalog.GetString(formatString, args);
            }
            catch (Exception e)
            {
                SentrySdk.AddBreadcrumb(null, "Translation", null, new Dictionary<string, string> { { "message", formatString }, { "language", lang.DisplayName }, { "catalog", translationCatalog.ToString() }, });
                e.TrackOrThrow();

                _logger.Warn($"Translation of message '{formatString}' for '{lang}/{translationCatalog}' failed.");
                return formatString;
            }
        }
    }
}