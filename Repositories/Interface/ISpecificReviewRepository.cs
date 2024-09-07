using ESProjeto_Back.Data.Dtos;

namespace ESProjeto_Back.Repositories.Interface
{
    public interface ISpecificReviewRepository
    {
        CreateSpecificReviewDto CreateSpecificReview(CreateSpecificReviewDto specificReviewDto);
        List<GetSpecificReviewDto> GetAllSpecificReveiwsFromReview(Guid reviewId);
        GetSpecificReviewDto GetSpecificReview(Guid id);
    }
}
