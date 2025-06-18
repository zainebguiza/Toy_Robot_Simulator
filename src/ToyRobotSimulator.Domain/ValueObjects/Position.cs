namespace ToyRobotSimulator.Domain.ValueObjects;

public readonly record struct Position(int X, int Y)
{
    public static Position Create(int x, int y)
    {
        if (x < 0 || y < 0)
            throw new ArgumentException("Position coordinates must be non-negative.");

        return new Position(x, y);
    }
}