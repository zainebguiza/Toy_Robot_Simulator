namespace ToyRobotSimulator.Domain.Interfaces;

public interface ICommandParser
{
    IEnumerable<string> ParseCommands(string input);
}