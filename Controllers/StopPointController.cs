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

        private static IStopPointService _stopPointAssessmentService;
        private static IUserService _userService;

        public StopPointController(IStopPointService stopPointAssessmentService, IUserService userService)
        {
            _stopPointAssessmentService = stopPointAssessmentService;
            _userService = userService;
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

                Guid stopPointId = _stopPointAssessmentService.NewStopPoint(newStopPoint, user);

                return Ok(new { stopPointId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
