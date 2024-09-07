
using ESProjeto_Back.Data.Dtos;

namespace ESProjeto_Back.Repositories.Interface
{
    public interface IReviewRepository
    {
        CreateReviewDto CreateNewReviewTo(CreateReviewDto newStopPoint);
        List<GetStopPointReviewDto> ListReviewsFrom(Guid stopPointId);
    }
}
