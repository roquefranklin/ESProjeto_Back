using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ESProjeto_Back.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;

        public ReviewController(IReviewService reviewService, IUserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        [HttpGet("from-stop-point/{stopPointId}")]
        public IActionResult GetReviewsForStopPoint(Guid stopPointId)
        {
            try
            {

                var reviewList = _reviewService.GetReviewsOf(stopPointId);

                return Ok(reviewList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPost]
        public IActionResult GiveReviewToStopPoint([FromBody] CreateReviewDto newReview)
        {
            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;

                if (identity == null)
                {
                    return BadRequest("Erro ao identificar usuário, calims não encontadas em token");
                }

                IEnumerable<Claim> claims = identity.Claims;

                string userEmail = claims.FirstOrDefault(claim => claim.Type.Contains(ClaimTypes.Email))!.Value ?? "";

                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest($"Token não possui claim {JwtRegisteredClaimNames.Email} email de usuário!");
                }

                var user = _userService.getUserByEmail(userEmail);

                if (user == null)
                {
                    return BadRequest($"Usuário com Email {userEmail} nãp encontrado");
                }

                newReview.UserId = user.Id;

                var createReview = _reviewService.GiveReviewTo(newReview);

                return CreatedAtAction(nameof(GetReviewsForStopPoint), new { stopPointId = createReview.Id }, createReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
