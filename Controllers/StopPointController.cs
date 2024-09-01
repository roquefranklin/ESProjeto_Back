using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESProjeto_Back.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class StopPointController : ControllerBase
    {

        private static IStopPointService _stopPointService;
        private static IUserService _userService;

        public StopPointController(IStopPointService stopPointAssessmentService, IUserService userService)
        {
            _stopPointService = stopPointAssessmentService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetStopPointInfo(Guid id)
        {

            try
            {

                var stopPointData = _stopPointService.GetStopPointData(id);

                return Ok(stopPointData);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        [HttpPost("new-stop-point")]
        public IActionResult CreateNewStopPoint([FromBody] CreateStopPointDto newStopPoint)
        {
            try
            {

                if (newStopPoint.StopPointPosition == null)
                {
                    return BadRequest("No Stop Point defined");
                }

                User? user = _userService.getUserByEmail(newStopPoint.userCreatorEmail);

                if (user == null)
                    return BadRequest($"User of Email \"{newStopPoint.userCreatorEmail}\" was not found");

                Guid stopPointId = _stopPointService.NewStopPoint(newStopPoint, user);

                return Ok(new { stopPointId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-stop-points")]
        public IActionResult GetClosePointsTo([FromQuery] float latitude, [FromQuery] float longitude, [FromQuery] float radius)
        {
            try
            {

                if (radius <= 0)
                    radius = 10;

                GetCloseStopPointsDto closePoints = _stopPointService.GetCloseStopPoints(latitude, longitude, radius);

                return Ok(closePoints);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

    }
}
