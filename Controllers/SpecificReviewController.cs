using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESProjeto_Back.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class SpecificReviewController : ControllerBase
    {

        private readonly ISpecificReviewService _specificReviewController;

        public SpecificReviewController(ISpecificReviewService specificReviewService)
        {
            _specificReviewController = specificReviewService;
        }

        [HttpGet("{id}")]
        public IActionResult GetSpecificReview(Guid id)
        {
            try
            {
                var specificReview = _specificReviewController.GetSpecificReview(id);

                return Ok(specificReview);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateSpecificReviewTo(CreateSpecificReviewDto specificReviewDto)
        {
            try
            {

                var specificReview = _specificReviewController.CreateSpecificReview(specificReviewDto);

                return CreatedAtAction(nameof(GetSpecificReview), new { specificReview.Id }, specificReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all-from-review/{ReveiwId}")]
        public IActionResult CreateSpecificReviewTo(Guid ReveiwId)
        {
            try
            {

                var specificReviews = _specificReviewController.GetAllSpecificReviewsFromReveiw(ReveiwId);

                return Ok(specificReviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
