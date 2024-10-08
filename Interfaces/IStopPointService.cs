﻿using ESProjeto_Back.Data.Dtos;
using ESProjeto_Back.Models;

namespace ESProjeto_Back.Interfaces
{
    public interface IStopPointService
    {
        GetCloseStopPointsDto GetCloseStopPoints(float latitude, float longitude, float radius);
        GetStopPointDataDto GetStopPointData(Guid id);
        Guid NewStopPoint(CreateStopPointDto newStopPoint, User user);
    }
}
