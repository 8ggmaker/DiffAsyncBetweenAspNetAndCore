In our web applications, we often want to do some short-term background tasks, but not hang the http response, that is:

:heavy_check_mark:

```csharp
[HttpGet]
public long GetValue()
{
    var _ = DoShortTask();
    return 0; // response returns without DoShortTask finished.
}

private Task DoShortTask()
{
    // do something
}
```

:x:

```csharp
[HttpGet]
public long GetValue()
{
    await DoShortTask();
    return 0; // response returns after DoShortTask finished.
}

private Task DoShortTask()
{
    // do something
}
```



There are some traps in Asp.Net when we use this pattern, and the AspNetCore framework also has different behavior with Asp.Net. This repository provides samples to show results of different approaches and why this occurs.

## To do

- Default
- Task.Run
- Suppress SynchronizationContext explicitly
- ConfigureAwait(false) (ConfigureAwait(false) in wrong callee/caller layer)
- AspNet Core