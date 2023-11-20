using System.Collections.Concurrent;
using LibraryClasses.Entity;

namespace GamePlayerMonogame.GamableMg;

public static class TexturesObjects
{
    public static ConcurrentDictionary<string, object> TexturesDictionary { get; } = new();

    public static object Get(string key)
    {
        TexturesDictionary.TryGetValue(key, out var property);
        return property;
    }

    public static void Set(string key, object newValue)
    {
        TexturesDictionary.AddOrUpdate(key, newValue, (k, oldValue) => newValue);
    }
}