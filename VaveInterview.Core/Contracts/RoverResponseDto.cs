namespace VaveInterview.Core.Contracts
{
    public class RoverResponseDto
    {
        public bool IsValid { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; } = "";
    }
}
