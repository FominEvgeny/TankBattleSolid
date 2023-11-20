using System;
using LibraryClasses.Entity;
using LibraryClasses.GameObjects;

namespace GamePlayerMonogame.GameActions.Movable;

public class MovableAdapterUObject : IMovable
{
    private readonly UObject _uobject;

    public MovableAdapterUObject(UObject uobject)
    {
        _uobject = uobject;
    }

    public void SetPosition(Coord2d position)
    {
        _uobject.SetProperty("Position", position);
    }

    public Coord2d? GetPosition()
    {
        return (Coord2d?)_uobject.GetProperty("Position");
    }

    public void SetVelocity(Coord2d velocity)
    {
        _uobject.SetProperty("Velocity", velocity);
    }

    public Coord2d? GetVelocity()
    {
        return (Coord2d?)_uobject.GetProperty("Velocity");
    }
}