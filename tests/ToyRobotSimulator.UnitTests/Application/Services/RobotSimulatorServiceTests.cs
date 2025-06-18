using ToyRobotSimulator.Application.Services;
using ToyRobotSimulator.Domain.Entities;
using ToyRobotSimulator.Domain.Enums;
using ToyRobotSimulator.Domain.ValueObjects;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Application.Services;

public class RobotSimulatorServiceTests
{
    private readonly Table _table;
    private readonly RobotSimulatorService _simulator;

    public RobotSimulatorServiceTests()
    {
        _table = new Table(5, 5);
        _simulator = new RobotSimulatorService(_table);
    }

    [Fact]
    public void Example_A_PlaceMoveReport_ShouldReturn_0_1_North()
    {
        _simulator.PlaceRobot(new Position(0, 0), Direction.North);
        _simulator.MoveRobot();

        var result = _simulator.ReportRobotStatus();

        Assert.Equal("0,1,NORTH", result);
    }

    [Fact]
    public void Example_C_PlaceMoveSequence_ShouldReturn_3_3_North()
    {
        _simulator.PlaceRobot(new Position(1, 2), Direction.East);
        _simulator.MoveRobot();
        _simulator.MoveRobot();
        _simulator.TurnRobotLeft();
        _simulator.MoveRobot();

        var result = _simulator.ReportRobotStatus();

        Assert.Equal("3,3,NORTH", result);
    }

    [Fact]
    public void PlaceRobot_ValidPosition_ShouldReturnTrue()
    {
        var result = _simulator.PlaceRobot(new Position(2, 2), Direction.North);

        Assert.True(result);
    }

    [Fact]
    public void MoveRobot_WhenNotPlaced_ShouldReturnFalse()
    {
        var result = _simulator.MoveRobot();

        Assert.False(result);
    }
}