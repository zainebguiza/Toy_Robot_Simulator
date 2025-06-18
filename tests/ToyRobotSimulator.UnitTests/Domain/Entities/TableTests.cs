using ToyRobotSimulator.Domain.Entities;
using ToyRobotSimulator.Domain.ValueObjects;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Domain.Entities;

public class TableTests
{
    [Fact]
    public void Constructor_ValidDimensions_ShouldCreateTable()
    {
        var table = new Table(5, 5);

        Assert.Equal(5, table.Width);
        Assert.Equal(5, table.Height);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_InvalidWidth_ShouldThrowArgumentException(int width)
    {
        Assert.Throws<ArgumentException>(() => new Table(width, 5));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_InvalidHeight_ShouldThrowArgumentException(int height)
    {
        Assert.Throws<ArgumentException>(() => new Table(5, height));
    }

    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(4, 4, true)]
    [InlineData(2, 3, true)]
    [InlineData(5, 5, false)]
    [InlineData(-1, 0, false)]
    [InlineData(0, -1, false)]
    [InlineData(5, 0, false)]
    [InlineData(0, 5, false)]
    public void IsValidPosition_VariousPositions_ShouldReturnCorrectValidity(int x, int y, bool expected)
    {
        var table = new Table(5, 5);
        var position = new Position(x, y);

        var result = table.IsValidPosition(position);

        Assert.Equal(expected, result);
    }
}