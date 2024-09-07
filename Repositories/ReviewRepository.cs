using ESProjeto_Back.Data;
using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Repositories
{
    public class ReviewRepository : IReviewRepository
    {

        public readonly MotofretaContext _context;

        public ReviewRepository(MotofretaContext context)
        {
            _context = context;
        }

        public CreateReviewDto CreateNewReviewTo(CreateReviewDto newReview)
        {

            var newDBReview = new Review()
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                UserId = newReview.UserId,
                StopPointId = newReview.StopPointId,
                ReviewScore = newReview.Score,
                Description = newReview.Description,
            };
            _context.Reviews.Add(newDBReview);
            _context.SaveChanges();

            newReview.Id = newDBReview.Id;

            return newReview;
        }

        public List<GetStopPointReviewDto> ListReviewsFrom(Guid stopPointId)
        {

            var reviews = _context.Reviews.Where(review => review.StopPointId.Equals(stopPointId))
                .Select(review => new GetStopPointReviewDto()
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    StopPointId = review.StopPointId,
                    Description = review.Description,
                    Score = review.ReviewScore,
                    Media_url = ""
                }).ToList();

            return reviews;
        }
    }
}
