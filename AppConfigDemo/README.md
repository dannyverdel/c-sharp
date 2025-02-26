# App Configuration Demo

[Tutorial: Use dynamic configuration in an Azure Functions app](https://learn.microsoft.com/en-us/azure/azure-app-configuration/enable-dynamic-configuration-azure-functions-csharp)

## Refresh werking

Het stukje code doet het volgende, iedere seconde checkt de code of de configuration 'refreshSentinel' is aangepast. Wanneer dit het geval is ververst die deze configruation. De refreshAll vlag zorgt ervoor dat alle andere configurations ook worden ververst.

```cs
.ConfigureRefresh(refresh => {
    refresh.Register("refreshSentinel", LabelFilter.Null, refreshAll: true)
    .SetRefreshInterval(TimeSpan.FromSeconds(1));
});
```
