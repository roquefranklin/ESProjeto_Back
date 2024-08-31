using ESProjeto_Back.Models;

namespace ESProjeto_Back.Repositories.Interface
{
    public interface IStopPointAssessmentRepository
    {
        public Guid SetNewStopPoint(StopPoint stopPoint, float latitude, float longitude);
    }
}
