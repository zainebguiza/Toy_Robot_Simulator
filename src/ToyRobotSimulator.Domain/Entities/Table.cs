using ToyRobotSimulator.Domain.ValueObjects;

namespace ToyRobotSimulator.Domain.Entities;

public class Table
{
    public int Width { get; }
    public int Height { get; }

    public Table(int width, int height)
    {
        if (width <= 0)
            throw new ArgumentException("Table width must be positive.", nameof(width));
        if (height <= 0)
            throw new ArgumentException("Table height must be positive.", nameof(height));

        Width = width;
        Height = height;
    }

    public bool IsValidPosition(Position position)
    {
        return position.X >= 0 && position.X < Width &&
               position.Y >= 0 && position.Y < Height;
    }
}