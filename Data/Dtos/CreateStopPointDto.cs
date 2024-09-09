using ESProjeto_Back.Models;
using System.Text.Json.Serialization;

namespace ESProjeto_Back.Data.Dtos
{
    public class CreateStopPointDto
    {
        [JsonPropertyName("StopPointPosition")]
        public GeolocationPosition? StopPointPosition { get; set; }
        public DateTime creationDate { get; set; } = DateTime.Now;
        public string userCreatorEmail { get; set; } = string.Empty!;
        public string Name { get; set; } = string.Empty!;
        public string Description { get; set; } = string.Empty!;
    }
}
