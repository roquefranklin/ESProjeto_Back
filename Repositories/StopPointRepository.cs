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

        public Guid SetNewStopPoint(StopPoint stopPoint, double latitude, double longitude)
        {
            stopPoint.Id = Guid.NewGuid();
            _context.Add(stopPoint);
            _context.SaveChanges();
            return stopPoint.Id;
        }

        public GetCloseStopPointsDto GetPointsOnRadiousOf(double latitude, double longitude, float radius)
        {

            var locations = _context.StopPoints.FromSql(
                $"SELECT  Id,  Name, COALESCE(description, '') as Description,  Latitude,  Longitude,  geolocalizacaoId, userId, creationDate, (6371 * ACOS(COS(RADIANS({latitude})) * COS(RADIANS(Latitude)) * COS(RADIANS(Longitude) - RADIANS({longitude})) + SIN(RADIANS({latitude})) * SIN(RADIANS(Latitude)))) AS Distance FROM StopPoints ORDER BY Distance"
                ).ToList();

            GetCloseStopPointsDto stoppoints = new GetCloseStopPointsDto()
            {
                StopPoints = locations.Select(stopPointDb => new StopPointData() { 
                    Id = stopPointDb.Id, 
                    Latitude = stopPointDb.latitude, 
                    Longitude = stopPointDb.longitude, 
                    Name = stopPointDb.Name, 
                    Description = stopPointDb.Description != null ? stopPointDb.Description : string.Empty // Tratar NULL aqui
                    }).ToList()

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
