using ESProjeto_Back.Data;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Repositories
{
    public class SpecificReviewRepository : ISpecificReviewRepository
    {

        private readonly MotofretaContext _motofretaContext;

        public SpecificReviewRepository(MotofretaContext motofretaContext)
        {
            _motofretaContext = motofretaContext;
        }

    }
}
