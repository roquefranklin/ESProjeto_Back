using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Interfaces;
using ESProjeto_Back.Models;
using ESProjeto_Back.Repositories;
using ESProjeto_Back.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ESProjeto_Back.Services
{
    public class StopPointService : IStopPointService
    {

        private static IStopPointRepository _stopPointRepository;

        public StopPointService(Repositories.Interface.IStopPointRepository stopPointAssessmentRepository)
        {
            _stopPointRepository = stopPointAssessmentRepository;
        }

        public Guid NewStopPoint(CreateStopPointDto newStopPoint, User user)
        {
            GeolocationPosition geolocationPosition = newStopPoint.StopPointPosition;
            if (geolocationPosition == null)
            {
                throw new Exception("No Stop Point Coordinates");
            }

            GeolocationCoordinates coordinates = geolocationPosition.coords;

            if (coordinates == null)
                throw new Exception("No Stop Point Coordinates");

            if (coordinates.latitude == null)
                throw new Exception("No latitude on Stop Point Coordinates");

            if (coordinates.longitude == null)
                throw new Exception("No longitude on Stop Point Coordinates");


            float latitude = (float)coordinates.latitude;
            float longitude = (float)coordinates.longitude;

            StopPoint stopPoint = new StopPoint()
            {
                geolocalizacao = geolocationPosition,
                latitude = latitude,
                longitude = longitude,
                creationDate = DateTime.Now,
                Name = newStopPoint.Name,
                User = user,
                UserId = user.Id
            };

            Guid stopPointId = _stopPointRepository.SetNewStopPoint(stopPoint, latitude, longitude);

            return stopPointId;
        }

        public GetCloseStopPointsDto GetCloseStopPoints(float latitude, float longitude, float radius)
        {
            var stopPoints = _stopPointRepository.GetPointsOnRadiousOf(latitude, longitude, radius);

            return stopPoints;
        }
    }
}
