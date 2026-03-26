using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;

namespace VaveInterview.Core.Models
{
    public class Rover
    {
        public Guid Id { get; set; }
        public int BoundaryWidth { get; set; }
        public int BoundaryHeight { get; set; }
        public Direction FacingDirection { get; set; }
        public Position CurrentPosition { get; set; } = new(0, 0);

        public RoverResult Execute(string commands)
        {
            commands = commands.ToUpper();

            if (!IsValidCommand(commands))
                return new RoverResult(false, FacingDirection, CurrentPosition, CurrentPosition);

            var startPos = new Position(CurrentPosition.X, CurrentPosition.Y);

            foreach (var cmd in commands)
            {
                switch (cmd)
                {
                    case 'A':
                        var result = MoveForward(CurrentPosition.X, CurrentPosition.Y, FacingDirection);

                        if (IsOutOfBounds(result, BoundaryWidth, BoundaryHeight))
                            return new RoverResult(false, FacingDirection, result, result);

                        CurrentPosition = result;
                        break;
                    case 'L':
                        FacingDirection = TurnLeft(FacingDirection);
                        break;
                    case 'R':
                        FacingDirection = TurnRight(FacingDirection);
                        break;
                    default:
                        break;
                }
            }

            return new RoverResult(true, FacingDirection, startPos, CurrentPosition);
        }

        #region Private Methods
        private bool IsOutOfBounds(Position p, int width, int height) => p.X < 0 || p.X > width || p.Y < 0 || p.Y > height;

        private bool IsValidCommand(string commands)
        {
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
        #endregion

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
