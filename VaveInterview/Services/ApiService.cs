using System.Net.Http.Json;
using System.Text.Json;
using VaveInterview.Core.Contracts;
using VaveInterview.Core.Converters;

namespace VaveInterview.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        //TODO: This needs proper error handling to cater for unsuccessful responses from the API.

        public ApiService([FromKeyedServices("api")] HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jsonOptions.Converters.Add(new PositionJsonConverter());
        }

        public async Task<CreateRoverResponse> CreateRoverAsync(CreateRoverRequest request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync<CreateRoverRequest>("rover/create", request);

            httpResponse.EnsureSuccessStatusCode();

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CreateRoverResponse>(jsonResponse, _jsonOptions) ?? new CreateRoverResponse();

            return result;
        }

        public async Task<CommandRoverResponse> CommandRoverAsync(CommandRoverRequest request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync<CommandRoverRequest>("rover/command", request);

            httpResponse.EnsureSuccessStatusCode();

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CommandRoverResponse>(jsonResponse, _jsonOptions) ?? new CommandRoverResponse();

            return result;
        }
    }
}
