using System;
using Exo.Rp.Sdk.Logger;
using Sentry;
using Sentry.Extensibility;

namespace Exo.Rp.Core.Sentry
{
    public class SentryEventExceptionProcessor : ISentryEventExceptionProcessor
    {
        private readonly ILogger<SentryEventExceptionProcessor> _logger;
        
        public SentryEventExceptionProcessor(ILogger<SentryEventExceptionProcessor> logger)
        {
            _logger = logger;
        }
        
        public void Process(Exception exception, SentryEvent sentryEvent)
        {
            
        }
    }
}