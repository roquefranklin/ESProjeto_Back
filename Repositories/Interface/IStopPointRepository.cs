using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Models;

namespace ESProjeto_Back.Repositories.Interface
{
    public interface IStopPointRepository
    {
        public GetCloseStopPointsDto GetPointsOnRadiousOf(double latitude, double longitude, float radius);
        public StopPoint? GetStopPointInfo(Guid id);
        public Guid SetNewStopPoint(StopPoint stopPoint, double latitude, double longitude);
    }
}
