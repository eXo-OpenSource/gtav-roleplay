using System;
using Sentry;
using Sentry.Protocol;

namespace Exo.Rp.Core.Util
{
    public static class ExceptionSentryExtensions
    {
        public static SentryId TrackOrThrow(this Exception exception)
        {
            if (SentrySdk.IsEnabled)
            {
                return SentrySdk.CaptureException(exception);
            }

            throw exception;
        }

        public static void WithScopeOrThrow(this Exception exception, Action<Scope> scopeAction)
        {
            if (!SentrySdk.IsEnabled)
            {
                throw exception;
            }

            SentrySdk.WithScope(scopeAction);
        }
    }
}