namespace VaveInterview.Core.Contracts
{
    public class RoverRequestDto
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public string Direction { get; set; } = "North";
        public string Commands { get; set; } = "";
    }
}
