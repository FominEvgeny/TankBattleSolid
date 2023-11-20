namespace LibraryClasses.Entity;

public interface IUobject
{
    object? GetProperty(string key);

    void SetProperty(string key, object newValue);
}