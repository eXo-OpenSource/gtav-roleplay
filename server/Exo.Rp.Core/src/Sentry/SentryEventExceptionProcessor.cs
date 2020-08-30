using System;
using Exo.Rp.Sdk.Logger;
using Microsoft.VisualBasic;
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
            _logger.Info($"Exception | Captured Event {sentryEvent.EventId}");
            _logger.Info(exception.Message);
            _logger.Info(exception.Source);
            _logger.Info(exception.StackTrace);
            _logger.Info(new String('=', 5));
        }
    }
}