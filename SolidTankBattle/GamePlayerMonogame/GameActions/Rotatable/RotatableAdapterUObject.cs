using System;
using LibraryClasses.Entity;
using LibraryClasses.GameObjects;

namespace GamePlayerMonogame.GameActions.Rotatable;

public class RotatableAdapterUObject : IRotatable
{
    private readonly UObject _uobject;

    public RotatableAdapterUObject(UObject uobject)
    {
        _uobject = uobject;
    }

    public void SetDirection(int newDirection)
    {
        _uobject.SetProperty("Direction", newDirection);
    }

    public int? GetDirection()
    {
        return (int?)_uobject.GetProperty("Direction");
    }

    public void SetAngularVelocity(int velocity)
    {
        _uobject.SetProperty("AngularVelocity", velocity);
    }

    public int? GetAngularVelocity()
    {
        return (int?)_uobject.GetProperty("AngularVelocity");
    }
}