using ToyRobotSimulator.Domain.Enums;
using ToyRobotSimulator.Domain.ValueObjects;

namespace ToyRobotSimulator.Domain.Entities;

public class Robot
{
    public Position? Position { get; private set; }
    public Direction? Direction { get; private set; }
    public bool IsPlaced => Position.HasValue && Direction.HasValue;

    public void Place(Position position, Direction direction)
    {
        Position = position;
        Direction = direction;
    }

    public Position GetNextPosition()
    {
        if (!IsPlaced)
            throw new InvalidOperationException("Robot must be placed before calculating next position.");

        return Direction!.Value switch
        {
            Enums.Direction.North => new Position(Position!.Value.X, Position.Value.Y + 1),
            Enums.Direction.East => new Position(Position!.Value.X + 1, Position.Value.Y),
            Enums.Direction.South => new Position(Position!.Value.X, Position.Value.Y - 1),
            Enums.Direction.West => new Position(Position!.Value.X - 1, Position.Value.Y),
            _ => throw new InvalidOperationException("Invalid direction.")
        };
    }

    public void Move()
    {
        if (!IsPlaced)
            return;

        Position = GetNextPosition();
    }

    public void TurnLeft()
    {
        if (!IsPlaced)
            return;

        Direction = Direction!.Value switch
        {
            Enums.Direction.North => Enums.Direction.West,
            Enums.Direction.West => Enums.Direction.South,
            Enums.Direction.South => Enums.Direction.East,
            Enums.Direction.East => Enums.Direction.North,
            _ => throw new InvalidOperationException("Invalid direction.")
        };
    }

    public void TurnRight()
    {
        if (!IsPlaced)
            return;

        Direction = Direction!.Value switch
        {
            Enums.Direction.North => Enums.Direction.East,
            Enums.Direction.East => Enums.Direction.South,
            Enums.Direction.South => Enums.Direction.West,
            Enums.Direction.West => Enums.Direction.North,
            _ => throw new InvalidOperationException("Invalid direction.")
        };
    }

    public string Report()
    {
        if (!IsPlaced)
            return "Robot is not placed on the table.";

        return $"{Position!.Value.X},{Position.Value.Y},{Direction}".ToUpperInvariant();
    }
}