using Microsoft.AspNetCore.Mvc;
using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;
using VaveInterview.Core.Services;
using VaveInterview.Core.Contracts;

namespace VaveInterview.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoverController : ControllerBase
    {
        private readonly RoverService _roverService;

        public RoverController(RoverService roverService)
        {
            _roverService = roverService;
        }

        [HttpPost("execute")]
        public ActionResult<RoverResponseDto> Execute([FromBody]RoverRequestDto request)
        {
            if (!Enum.TryParse<Direction>(request?.Direction, true, out var direction))
                return BadRequest("Invalid direction. 'North', 'South', 'East' or 'West' are valid.");

            try
            {
                var startPosition = new Position(request.StartX, request.StartY);

                var result = _roverService.Execute(request.Width, request.Height, direction, startPosition, request.Commands);

                var response = new RoverResponseDto
                {
                    IsValid = result.IsValid,
                    Direction = result.Direction.ToString(),
                    X = result.Position.X,
                    Y = result.Position.Y,
                };

                return Ok(response);
            }
            catch (ArgumentException aex)
            {
                return BadRequest($"Invalid arguments. {aex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured. {ex.Message}");
            }
        }
    }
}
