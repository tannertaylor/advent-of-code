using static System.Math;

namespace Day14;

public record struct Coord(int X, int Y)
{
    public static Coord FromString(string coordStr)
    {
        var splits = coordStr.Split(',');
        return new(int.Parse(splits[0]), int.Parse(splits[1]));
    }

    public static implicit operator Coord((int X, int Y) tuple) => new Coord(tuple.X, tuple.Y);

    public static Coord operator +(Coord original, Coord vector) => new(original.X + vector.X, original.Y + vector.Y);

    public IEnumerable<Coord> PathTo(Coord destination)
    {
        yield return this;

        var (deltaX, deltaY) = (destination.X - X, destination.Y - Y);
        for (var i = Sign(deltaX + deltaY); Abs(i) < Abs(deltaX + deltaY); i += Sign(deltaX + deltaY))
            yield return new(X + i * Abs(Sign(deltaX)), Y + i * Abs(Sign(deltaY)));

        yield return destination;
    }
}