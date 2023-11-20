using System.Collections.Concurrent;
using LibraryClasses.Entity;

namespace LibraryClasses.GameObjects;

public class UObject : IUobject
{
    public ConcurrentDictionary<string, object?> Properties { get; } = new();
    
    public object? GetProperty(string key)
    {
        Properties.TryGetValue(key, out var property);
        return property;
    }

    public void SetProperty(string key, object? newValue)
    {
        Properties.AddOrUpdate(key, newValue, (k, oldValue) => newValue);
    }
}