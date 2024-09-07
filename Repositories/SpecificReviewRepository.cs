using ESProjeto_Back.Data;
using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Repositories
{
    public class SpecificReviewRepository : ISpecificReviewRepository
    {

        private readonly MotofretaContext _context;

        public SpecificReviewRepository(MotofretaContext motofretaContext)
        {
            _context = motofretaContext;
        }

        public CreateSpecificReviewDto CreateSpecificReview(CreateSpecificReviewDto specificReviewDto)
        {

            var newSpecificReview = new SpecificReview()
            {
                Id = Guid.NewGuid(),
                SpecificReviewScore = specificReviewDto.Score,
                ReviewId = specificReviewDto.ReviewId,
                type = specificReviewDto.Type,
            };
            _context.Add(newSpecificReview);
            _context.SaveChanges();

            specificReviewDto.Id = newSpecificReview.Id;

            return specificReviewDto;

        }

        public List<GetSpecificReviewDto> GetAllSpecificReveiwsFromReview(Guid reviewId)
        {
            var specificReviews = _context.SpecificReviews
                .Where(specificReview => specificReview.ReviewId == reviewId)
                .Select(specificReview => new GetSpecificReviewDto()
                {
                    Id = specificReview.Id,
                    ReviewId = specificReview.ReviewId,
                    Score = specificReview.SpecificReviewScore,
                    Type = specificReview.type
                }).ToList();

            return specificReviews;
        }

        public GetSpecificReviewDto GetSpecificReview(Guid id)
        {

            var specificReview = _context.SpecificReviews.FirstOrDefault(specificReveiw => specificReveiw.Id == id);

            return new GetSpecificReviewDto
            {
                Id = specificReview.Id,
                ReviewId = specificReview.ReviewId,
                Score = specificReview.SpecificReviewScore,
                Type = specificReview.type
            };
        }
    }
}
