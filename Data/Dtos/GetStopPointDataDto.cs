namespace ESProjeto_Back.Data.Dtos
{
    public class GetStopPointDataDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
