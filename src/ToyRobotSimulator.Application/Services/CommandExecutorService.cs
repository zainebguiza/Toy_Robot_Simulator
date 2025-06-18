using ToyRobotSimulator.Domain.Enums;
using ToyRobotSimulator.Domain.Interfaces;
using ToyRobotSimulator.Domain.ValueObjects;

namespace ToyRobotSimulator.Application.Services;

public class CommandExecutorService
{
    private readonly IRobotSimulator _robotSimulator;

    public CommandExecutorService(IRobotSimulator robotSimulator)
    {
        _robotSimulator = robotSimulator ?? throw new ArgumentNullException(nameof(robotSimulator));
    }

    public string ExecuteCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
            return string.Empty;

        var parts = command.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var commandType = parts[0].ToUpperInvariant();

        return commandType switch
        {
            "PLACE" => ExecutePlaceCommand(parts),
            "MOVE" => ExecuteMoveCommand(),
            "LEFT" => ExecuteLeftCommand(),
            "RIGHT" => ExecuteRightCommand(),
            "REPORT" => ExecuteReportCommand(),
            _ => $"Unknown command: {command}"
        };
    }

    private string ExecutePlaceCommand(string[] parts)
    {
        if (parts.Length != 2)
            return "Invalid PLACE command format. Expected: PLACE X,Y,F";

        var parameters = parts[1].Split(',');
        if (parameters.Length != 3)
            return "Invalid PLACE command format. Expected: PLACE X,Y,F";

        if (!int.TryParse(parameters[0], out var x) ||
            !int.TryParse(parameters[1], out var y) ||
            !Enum.TryParse<Direction>(parameters[2], true, out var direction))
        {
            return "Invalid PLACE command parameters. X and Y must be integers, F must be NORTH, SOUTH, EAST, or WEST.";
        }

        try
        {
            var position = Position.Create(x, y);
            var success = _robotSimulator.PlaceRobot(position, direction);
            return success ? string.Empty : "Cannot place robot at the specified position.";
        }
        catch (ArgumentException ex)
        {
            return ex.Message;
        }
    }

    private string ExecuteMoveCommand()
    {
        var success = _robotSimulator.MoveRobot();
        return success ? string.Empty : string.Empty;
    }

    private string ExecuteLeftCommand()
    {
        _robotSimulator.TurnRobotLeft();
        return string.Empty;
    }

    private string ExecuteRightCommand()
    {
        _robotSimulator.TurnRobotRight();
        return string.Empty;
    }

    private string ExecuteReportCommand()
    {
        return _robotSimulator.ReportRobotStatus();
    }
}