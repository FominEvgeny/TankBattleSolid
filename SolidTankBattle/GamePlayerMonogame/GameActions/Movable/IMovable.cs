using LibraryClasses.Entity;

namespace GamePlayerMonogame.GameActions.Movable;

public interface IMovable
{
    void SetPosition(Coord2d position);

    Coord2d? GetPosition();

    void SetVelocity(Coord2d position);

    Coord2d? GetVelocity();

}