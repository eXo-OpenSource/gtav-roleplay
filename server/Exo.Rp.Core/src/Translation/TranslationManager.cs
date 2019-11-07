using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AltV.Net;
using models.Enums.Translation;
using NGettext;

namespace server.Translation
{
    public class TranslationManager : IManager
    {
        private readonly string _localDir = Path.Combine("resources", Alt.Server.Resource.Name);
        private readonly CultureInfo[] _languages = { new CultureInfo("de-DE") };
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
    }
}