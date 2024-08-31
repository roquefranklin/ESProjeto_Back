using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Models;

namespace ESProjeto_Back.Interfaces
{
    public interface IStopPointService
    {
        Guid NewStopPoint(CreateStopPointDto newStopPoint, User user);
    }
}
