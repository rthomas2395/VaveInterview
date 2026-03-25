using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;

namespace VaveInterview.Core.Services
{
    public class RoverService
    {
        public RoverResult Execute(int width, int height, Direction direction, Position position, string commands)
        {
            commands = commands.ToUpper();

            if (!IsValidCommand(commands))
                return new RoverResult(false, direction, position);

            foreach (var cmd in commands)
            {
                switch (cmd)
                {
                    case 'A':
                        var result = MoveForward(position.X, position.Y, direction);

                        if (IsOutOfBounds(result, width, height))
                            return new RoverResult(false, direction, result);

                        position = result;
                        break;
                    case 'L':
                        direction = TurnLeft(direction);
                        break;
                    case 'R':
                        direction = TurnRight(direction);
                        break;
                    default:
                        break;
                }
            }

            return new RoverResult(true, direction, position);
        }

        private bool IsValidCommand(string commands)
        {
            //TODO: Should this be public, do we want to validate the command before execution? 
            //Could also do this using RegEx - would run a BenchmarkDotNet to see which is better in performance critical scenario.
            if (string.IsNullOrWhiteSpace(commands))
                return false;

            foreach (var c in commands)
            {
                if (c != 'A' && c != 'L' && c != 'R')
                    return false;
            }

            return true;
        }

        private bool IsOutOfBounds(Position p, int width, int height) => p.X < 0 || p.X > width || p.Y < 0 || p.Y > height;

        #region Navigation Methods
        private Position MoveForward(int x, int y, Direction dir)
        {
            return dir switch
            {
                Direction.North => new Position(x, y + 1),
                Direction.South => new Position(x, y - 1),
                Direction.East => new Position(x + 1, y),
                Direction.West => new Position(x - 1, y),
                _ => new Position(x, y)
            };
        }
        private Direction TurnLeft(Direction dir)
        {
            return dir switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => dir
            };
        }
        private Direction TurnRight(Direction dir)
        {
            return dir switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => dir
            };
        }
        #endregion
    }
}
