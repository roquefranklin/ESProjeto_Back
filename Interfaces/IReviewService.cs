
using ESProjeto_Back.Data.Dtos;

namespace ESProjeto_Back.Interfaces
{
    public interface IReviewService
    {
        List<GetStopPointReviewDto> GetReviewsOf(Guid stopPointId);
        CreateReviewDto GiveReviewTo(CreateReviewDto newReview);
    }
}
