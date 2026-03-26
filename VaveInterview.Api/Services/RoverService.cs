using System.Text.Json;
using VaveInterview.Core.Converters;
using VaveInterview.Core.Models;
using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;

namespace VaveInterview.Api.Services
{
    public class RoverService
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public RoverService()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            _jsonOptions.Converters.Add(new PositionJsonConverter());
        }

        public async Task<Guid> CreateRoverAsync(int height, int width, Position position, Direction direction)
        {
            var rover = new Rover
            {
                Id = Guid.NewGuid(),
                BoundaryHeight = height,
                BoundaryWidth = width,
                CurrentPosition = position,
                FacingDirection = direction,
            };

            var roverJson = JsonSerializer.Serialize(rover, _jsonOptions);

            //In a production scenario we wouldn't be storing data as file... we'd use a database.
            await File.WriteAllTextAsync($"data/{rover.Id}.json", roverJson);

            return rover.Id;
        }

        public async Task<Rover?> GetRoverAsync(Guid roverId)
        {
            var filePath = $"data/{roverId}.json";

            if (!File.Exists(filePath))
                return null;

            var roverJson = await File.ReadAllTextAsync(filePath);

            var rover = JsonSerializer.Deserialize<Rover>(roverJson, _jsonOptions) ?? null;

            return rover;
        }

        public async Task UpdateRoverAsync(Rover rover)
        {
            var roverJson = JsonSerializer.Serialize(rover, _jsonOptions);
            await File.WriteAllTextAsync($"data/{rover.Id}.json", roverJson);
        }
    }
}
