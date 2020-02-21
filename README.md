
# Alt:V exo Server

## Explanations
*Descriptions for undocumented methods of the Api*

`SetSyncedMetaData` stream synced meta data is only synchronized with clients near the entity

## Sentry Error Tracking
In order to track exceptions in Sentry you need to catch and track them manually, as unhandled exceptions crash the server.
On local development the exceptions won't be tracked instead they will be re-thrown.
Short example on how track an exception:
```csharp
catch (Exception e) {
    var correlationId = e.TrackOrThrow();
}
```

With user context:
```csharp
catch (Exception e) {
    e.WithScopeOrThrow(s =>
    {
        s.User = player.SentryContext;

        var correlationId = e.TrackOrThrow();
    });
}
```

To add additionally metadata use `SentrySdk.AddBreadcrumb`:
```csharp
catch (Exception e) {
    SentrySdk.AddBreadcrumb(null, "Command", null, new Dictionary<string, string> { { "command", command }, { "args", string.Join(',', args) } });
    var correlationId = e.TrackOrThrow();
}
```
