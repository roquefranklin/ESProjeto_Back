using ESProjeto_Back.Data;
using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ESProjeto_Back.Repositories
{
    public class StopPointRepository : IStopPointRepository
    {

        public static MotofretaContext _context;

        public StopPointRepository(MotofretaContext context)
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

        public GetCloseStopPointsDto GetPointsOnRadiousOf(float latitude, float longitude, float radius)
        {

            var locations = _context.StopPoints.FromSql(
                $"SELECT  Id,  Name,  Latitude,  Longitude,  geolocalizacaoId, userId, creationDate, (6371 * ACOS(COS(RADIANS({latitude})) * COS(RADIANS(Latitude)) * COS(RADIANS(Longitude) - RADIANS({longitude})) + SIN(RADIANS({latitude})) * SIN(RADIANS(Latitude)))) AS Distance  FROM stoppoints HAVING  Distance < {radius} ORDER BY Distance"
                ).ToList();

            GetCloseStopPointsDto stoppoints = new GetCloseStopPointsDto()
            {
                StopPoints = locations.Select(stopPointDb => new StopPointData() { Id = stopPointDb.Id, Latitude = stopPointDb.latitude, Longitude = stopPointDb.longitude, Name = stopPointDb.Name }).ToList()
            };

            return stoppoints;
        }

        public StopPoint? GetStopPointInfo(Guid id)
        {
            var stopPoint = _context.StopPoints.FirstOrDefault((point) => point.Id == id);
            return stopPoint;
        }
    }
}
