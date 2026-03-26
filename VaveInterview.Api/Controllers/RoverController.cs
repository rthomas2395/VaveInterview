using Microsoft.AspNetCore.Mvc;
using VaveInterview.Api.Services;
using VaveInterview.Core.Contracts;
using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;

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

        [HttpPost("create")]
        public async Task<ActionResult<CreateRoverResponse>> CreateRover([FromBody] CreateRoverRequest request)
        {
            if (!Enum.TryParse<Direction>(request?.Direction, true, out var direction))
                return BadRequest("Invalid direction. 'North', 'South', 'East' or 'West' are valid.");

            try
            {
                var startingPosition = new Position(request.StartX, request.StartY);

                //TODO: Validate the starting position is actually inside the grid.

                var roverId = await _roverService.CreateRoverAsync(request.Height, request.Width, startingPosition, direction);

                var response = new CreateRoverResponse
                {
                    Id = roverId
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

        //TODO: An endpoint for getting a rover, using a URL parameter.

        [HttpPost("command")]
        public async Task<ActionResult<CommandRoverResponse>> CommandRover([FromBody] CommandRoverRequest request)
        {
            try
            {
                var rover = await _roverService.GetRoverAsync(request.RoverId);

                if (rover == null)
                    return BadRequest($"Rover '{request.RoverId}' not found.");

                var result = rover.Execute(request.Commands);

                if (result.IsValid)
                    await _roverService.UpdateRoverAsync(rover);

                var response = new CommandRoverResponse
                {
                    IsValid = result.IsValid,
                    Direction = result.Direction,
                    StartingPosition = result.StartPosition,
                    EndingPosition = result.EndPosition,
                    Description = result.ToString()
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
