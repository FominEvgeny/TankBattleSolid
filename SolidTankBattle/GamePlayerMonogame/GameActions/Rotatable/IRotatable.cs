namespace GamePlayerMonogame.GameActions.Rotatable;

public interface IRotatable
{
    void SetDirection(int newDirection);

    int? GetDirection();

    void SetAngularVelocity(int velocity);

    int? GetAngularVelocity();
}