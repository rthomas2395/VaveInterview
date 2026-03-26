using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;

namespace VaveInterview.Core.Contracts
{
    public class CommandRoverResponse
    {
        public bool IsValid { get; set; }
        public Direction Direction { get; set; }
        public Position StartingPosition { get; set; } = new(0, 0);
        public Position EndingPosition { get; set; } = new(0, 0);
        public string Description { get; set; } = "";
    }
}
