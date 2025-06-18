using ToyRobotSimulator.Domain.Interfaces;

namespace ToyRobotSimulator.Infrastructure.Parsers;

public class CommandParser : ICommandParser
{
    public IEnumerable<string> ParseCommands(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Enumerable.Empty<string>();

        return input
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim());
    }
}