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

        private readonly IStopPointRepository _stopPointRepository;

        public StopPointService(Repositories.Interface.IStopPointRepository stopPointAssessmentRepository)
        {
            _stopPointRepository = stopPointAssessmentRepository;
        }

        public Guid NewStopPoint(CreateStopPointDto newStopPoint, User user)
        {

            if (newStopPoint == null)
                throw new ArgumentNullException(nameof(newStopPoint));

            if (newStopPoint.StopPointPosition == null)
            {
                throw new ArgumentNullException(nameof(newStopPoint.StopPointPosition));
            }
            else if (newStopPoint.StopPointPosition.coords == null)
            {
                throw new ArgumentNullException(nameof(newStopPoint.StopPointPosition.coords));
            }

            GeolocationPosition geolocationPosition = newStopPoint.StopPointPosition;

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
                Description = newStopPoint.Description,
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

        public GetStopPointDataDto GetStopPointData(Guid id)
        {

            var stopPointData = _stopPointRepository.GetStopPointInfo(id);

            if(stopPointData == null)
                throw new ArgumentNullException($"Stop Point of id {id} not found");

            var stopPoint = new GetStopPointDataDto()
            {
                Id = stopPointData.Id,
                Name = stopPointData.Name,
                Description = stopPointData.Description,
                Latitude = stopPointData.latitude,
                Longitude = stopPointData.longitude
            };

            return stopPoint;
        }
    }
}
