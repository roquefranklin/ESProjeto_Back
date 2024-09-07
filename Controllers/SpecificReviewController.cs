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

    }
}
