using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Services
{
    public class SpecificReviewService : ISpecificReviewService
    {

        private readonly ISpecificReviewRepository _specificReviewController;

        public SpecificReviewService(ISpecificReviewRepository specificReviewService)
        {
            _specificReviewController = specificReviewService;
        }

        public CreateSpecificReviewDto CreateSpecificReview(CreateSpecificReviewDto specificReviewDto)
        {
            var newSpecificReview = _specificReviewController.CreateSpecificReview(specificReviewDto);

            return newSpecificReview;
        }

        public List<GetSpecificReviewDto> GetAllSpecificReviewsFromReveiw(Guid reviewId)
        {
            var specificReviews = _specificReviewController.GetAllSpecificReveiwsFromReview(reviewId);

            return specificReviews;
        }

        public GetSpecificReviewDto GetSpecificReview(Guid id)
        {
            var specificReview = _specificReviewController.GetSpecificReview(id);

            return specificReview;
        }
    }
}
