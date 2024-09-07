
using ESProjeto_Back.Data.Dtos;

namespace ESProjeto_Back.Interfaces
{
    public interface ISpecificReviewService
    {
        CreateSpecificReviewDto CreateSpecificReview(CreateSpecificReviewDto specificReviewDto);
        List<GetSpecificReviewDto> GetAllSpecificReviewsFromReveiw(Guid reviewId);
        GetSpecificReviewDto GetSpecificReview(Guid id);
    }
}
