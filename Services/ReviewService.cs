using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Services
{
    public class ReviewService : IReviewService
    {

        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public List<GetStopPointReviewDto> GetReviewsOf(Guid stopPointId)
        {
            if (stopPointId == Guid.Empty)
            {
                throw new Exception("Id de Ponto de Parada em Branco");
            }

            var reviews = _reviewRepository.ListReviewsFrom(stopPointId);

            return reviews;

        }

        public CreateReviewDto GiveReviewTo(CreateReviewDto newReview)
        {

            if (newReview.UserId == Guid.Empty)
            {
                throw new Exception("Id do Passageiro em Branco");
            }

            if (newReview.StopPointId == Guid.Empty)
            {
                throw new Exception("Id de Ponto de Parada em Branco");
            }

            var newReviewId = _reviewRepository.CreateNewReviewTo(newReview);

            return newReviewId;
        }
    }
}
