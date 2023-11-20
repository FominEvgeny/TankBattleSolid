namespace LibraryClasses.Entity;

public class Coord2d
{
    public Coord2d(float x, float y)
    {
        X = x;
        Y = y;
    }

   
    public float X { get; set; }

    public float Y { get; set; }

    

    public static Coord2d Append(Coord2d first, Coord2d second)
    {
        return new Coord2d(first.X + second.X, first.Y + second.Y);
    }
}