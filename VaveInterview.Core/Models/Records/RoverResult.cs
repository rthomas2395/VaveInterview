using VaveInterview.Core.Models.Enums;

namespace VaveInterview.Core.Models.Records
{
    public record RoverResult
    {
        public readonly bool IsValid;
        public readonly Direction Direction;
        public readonly Position Position;

        public RoverResult(bool isValid, Direction direction, Position position)
        {
            IsValid = isValid;
            Direction = direction;
            Position = position;
        }

        public override string ToString()
        {
            return $"{IsValid}, {Direction.ToString()[..1]}, {Position}";
        }
    }
}
