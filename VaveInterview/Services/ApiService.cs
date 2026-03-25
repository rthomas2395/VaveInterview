using System.Net.Http.Json;
using System.Text.Json;
using VaveInterview.Core.Contracts;
using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;

namespace VaveInterview.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService([FromKeyedServices("api")] HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RoverResult> PostDataAsync(RoverRequestDto request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync<RoverRequestDto>("rover/execute", request);

            httpResponse.EnsureSuccessStatusCode();
            //TODO: Proper error handling

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var dto = JsonSerializer.Deserialize<RoverResponseDto>(jsonResponse, jsonOptions) ?? new RoverResponseDto();

            var direction = Enum.Parse<Direction>(dto.Direction);
            return new RoverResult(dto.IsValid, direction, new Position(dto.X, dto.Y));
        }
    }
}
