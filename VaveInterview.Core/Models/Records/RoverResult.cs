using VaveInterview.Core.Models.Enums;

namespace VaveInterview.Core.Models.Records
{
    public record RoverResult
    {
        public readonly bool IsValid;
        public readonly Direction Direction;
        public readonly Position StartPosition;
        public readonly Position EndPosition;

        public RoverResult(bool isValid, Direction direction, Position startPosition, Position endPosition)
        {
            IsValid = isValid;
            Direction = direction;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public override string ToString()
        {
            return $"{IsValid}, {Direction.ToString()[..1]}, {EndPosition}";
        }
    }
}
