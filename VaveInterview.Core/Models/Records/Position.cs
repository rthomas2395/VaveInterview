namespace VaveInterview.Core.Models.Records
{
    public record Position
    {
        public int X = 0;
        public int Y = 0;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
