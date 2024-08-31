using ESProjeto_Back.Data;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;

namespace ESProjeto_Back.Repositories
{
    public class StopPointAssessmentRepository : IStopPointAssessmentRepository
    {

        public static MotofretaContext _context;

        public StopPointAssessmentRepository(MotofretaContext context)
        {
            _context = context;
        }

        public Guid SetNewStopPoint(StopPoint stopPoint, float latitude, float longitude)
        {
            stopPoint.Id = Guid.NewGuid();
            _context.Add(stopPoint);
            _context.SaveChanges();
            return stopPoint.Id;
        }
    }
}
