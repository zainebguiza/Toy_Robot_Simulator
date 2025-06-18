using ToyRobotSimulator.Application.Services;
using ToyRobotSimulator.Domain.Entities;
using ToyRobotSimulator.Infrastructure.Parsers;

namespace ToyRobotSimulator.Console;

class Program
{
    static void Main(string[] args)
    {
        var table = new Table(5, 5);
        var robotSimulator = new RobotSimulatorService(table);
        var commandExecutor = new CommandExecutorService(robotSimulator);
        var commandParser = new CommandParser();

        System.Console.WriteLine("Toy Robot Simulator");
        System.Console.WriteLine("Commands: PLACE X,Y,F | MOVE | LEFT | RIGHT | REPORT | EXIT");
        System.Console.WriteLine("Example: PLACE 0,0,NORTH");
        System.Console.WriteLine();

        if (args.Length > 0)
        {
            ProcessFile(args[0], commandExecutor, commandParser, robotSimulator);
        }
        else
        {
            ProcessInteractiveMode(commandExecutor, robotSimulator);
        }
    }

    private static void ProcessFile(string filePath, CommandExecutorService commandExecutor, CommandParser commandParser, RobotSimulatorService robotSimulator)
    {
        try
        {
            var fileContent = File.ReadAllText(filePath);
            var commands = commandParser.ParseCommands(fileContent);

            foreach (var command in commands)
            {
                var result = commandExecutor.ExecuteCommand(command);
                if (!string.IsNullOrEmpty(result))
                {
                    System.Console.WriteLine(result);
                }
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }

    private static void ProcessInteractiveMode(CommandExecutorService commandExecutor, RobotSimulatorService robotSimulator)
    {
        while (true)
        {
            System.Console.Write("> ");
            var input = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input.Trim().ToUpperInvariant() == "EXIT")
                break;

            var result = commandExecutor.ExecuteCommand(input);
            var commandType = input.Trim().Split(' ')[0].ToUpperInvariant();

            if (!string.IsNullOrEmpty(result))
            {
                //  error messages
                System.Console.WriteLine(result);
            }
            else
            {
                switch (commandType)
                {
                    case "PLACE":
                        System.Console.WriteLine($"Robot placed. Current position: {robotSimulator.ReportRobotStatus()}");
                        break;
                    case "MOVE":
                        var status = robotSimulator.ReportRobotStatus();
                        if (status.Contains("not placed"))
                        {
                            System.Console.WriteLine("Robot is not placed. Use PLACE command first.");
                        }
                        else
                        {
                            System.Console.WriteLine($"Robot moved. Current position: {status}");
                        }
                        break;
                    case "LEFT":
                        var leftStatus = robotSimulator.ReportRobotStatus();
                        if (leftStatus.Contains("not placed"))
                        {
                            System.Console.WriteLine("Robot is not placed. Use PLACE command first.");
                        }
                        else
                        {
                            System.Console.WriteLine($"Robot turned left. Current position: {leftStatus}");
                        }
                        break;
                    case "RIGHT":
                        var rightStatus = robotSimulator.ReportRobotStatus();
                        if (rightStatus.Contains("not placed"))
                        {
                            System.Console.WriteLine("Robot is not placed. Use PLACE command first.");
                        }
                        else
                        {
                            System.Console.WriteLine($"Robot turned right. Current position: {rightStatus}");
                        }
                        break;
                }
            }
        }
    }
}
