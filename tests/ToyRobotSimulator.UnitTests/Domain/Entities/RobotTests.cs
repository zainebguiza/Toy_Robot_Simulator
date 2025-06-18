using ToyRobotSimulator.Domain.Entities;
using ToyRobotSimulator.Domain.Enums;
using ToyRobotSimulator.Domain.ValueObjects;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Domain.Entities;

public class RobotTests
{
    [Fact]
    public void Robot_InitialState_ShouldNotBePlaced()
    {
        var robot = new Robot();

        Assert.False(robot.IsPlaced);
        Assert.Null(robot.Position);
        Assert.Null(robot.Direction);
    }

    [Fact]
    public void Place_ValidPositionAndDirection_ShouldPlaceRobot()
    {
        var robot = new Robot();
        var position = new Position(1, 2);
        var direction = Direction.North;

        robot.Place(position, direction);

        Assert.True(robot.IsPlaced);
        Assert.Equal(position, robot.Position);
        Assert.Equal(direction, robot.Direction);
    }

    [Fact]
    public void GetNextPosition_WhenFacingNorth_ShouldReturnPositionWithIncreasedY()
    {
        var robot = new Robot();
        robot.Place(new Position(1, 1), Direction.North);

        var nextPosition = robot.GetNextPosition();

        Assert.Equal(new Position(1, 2), nextPosition);
    }
    [Fact]
    public void GetNextPosition_WhenNotPlaced_ShouldThrowException()
    {
        var robot = new Robot();

        Assert.Throws<InvalidOperationException>(() => robot.GetNextPosition());
    }

    [Fact]
    public void Move_WhenNotPlaced_ShouldNotThrowException()
    {
        var robot = new Robot();

        robot.Move();

        Assert.False(robot.IsPlaced);
    }

    [Theory]
    [InlineData(Direction.North, Direction.West)]
    [InlineData(Direction.West, Direction.South)]
    [InlineData(Direction.South, Direction.East)]
    [InlineData(Direction.East, Direction.North)]
    public void TurnLeft_ShouldRotateCounterClockwise(Direction initial, Direction expected)
    {
        var robot = new Robot();
        robot.Place(new Position(0, 0), initial);

        robot.TurnLeft();

        Assert.Equal(expected, robot.Direction);
    }
}