namespace ESProjeto_Back.Data.Dtos
{
    public class GetCloseStopPointsDto
    {
        public List<StopPointData> StopPoints { get; set; } = new List<StopPointData>();
    }

    public class StopPointData
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }
}
