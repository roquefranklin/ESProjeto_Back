namespace ESProjeto_Back.Data.Dtos
{
    public class GetCloseStopPointsDto
    {
        public List<StopPointData> StopPoints { get; set; } = new List<StopPointData>();
    }

    public class StopPointData
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}
