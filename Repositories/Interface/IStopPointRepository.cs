using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Models;

namespace ESProjeto_Back.Repositories.Interface
{
    public interface IStopPointRepository
    {
        public GetCloseStopPointsDto GetPointsOnRadiousOf(float latitude, float longitude, float radius);
        public Guid SetNewStopPoint(StopPoint stopPoint, float latitude, float longitude);
    }
}
