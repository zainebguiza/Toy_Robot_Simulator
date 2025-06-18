using ToyRobotSimulator.Domain.Entities;
using ToyRobotSimulator.Domain.Enums;
using ToyRobotSimulator.Domain.Interfaces;
using ToyRobotSimulator.Domain.ValueObjects;

namespace ToyRobotSimulator.Application.Services;

public class RobotSimulatorService : IRobotSimulator
{
    private readonly Robot _robot;
    private readonly Table _table;

    public RobotSimulatorService(Table table)
    {
        _table = table ?? throw new ArgumentNullException(nameof(table));
        _robot = new Robot();
    }

    public bool PlaceRobot(Position position, Direction direction)
    {
        if (!_table.IsValidPosition(position))
            return false;

        _robot.Place(position, direction);
        return true;
    }

    public bool MoveRobot()
    {
        if (!_robot.IsPlaced)
            return false;

        var nextPosition = _robot.GetNextPosition();
        if (!_table.IsValidPosition(nextPosition))
            return false;

        _robot.Move();
        return true;
    }

    public void TurnRobotLeft()
    {
        _robot.TurnLeft();
    }

    public void TurnRobotRight()
    {
        _robot.TurnRight();
    }

    public string ReportRobotStatus()
    {
        return _robot.Report();
    }
}