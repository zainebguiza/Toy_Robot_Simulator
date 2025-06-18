using ToyRobotSimulator.Domain.Enums;
using ToyRobotSimulator.Domain.ValueObjects;

namespace ToyRobotSimulator.Domain.Interfaces;

public interface IRobotSimulator
{
    bool PlaceRobot(Position position, Direction direction);
    bool MoveRobot();
    void TurnRobotLeft();
    void TurnRobotRight();
    string ReportRobotStatus();
}