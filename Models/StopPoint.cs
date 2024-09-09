using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ESProjeto_Back.Models
{
    [Table("StopPoints")]
    public class StopPoint
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        public GeolocationPosition geolocalizacao { get; set; } = null!;
        [Required(ErrorMessage = "The {0} is required"), MinLength(-90), MaxLength(90)]
        public float latitude { get; set; }
        [Required(ErrorMessage = "The {0} is required"), MinLength(-180), MaxLength(180)]
        public float longitude { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        public User User { get; set; } = null!;
        public Guid UserId { get; set; } = Guid.Empty!;
        public Collection<Review> Reviews { get; }
    }

    public class GeolocationPosition
    {
        [Key, Required]
        public Guid Id { get; set; }
        [JsonPropertyName("coords")]
        public GeolocationCoordinates? coords { get; set; }
        [JsonPropertyName("timestamp")]
        public float timestamp { get; set; }
    }

    public class GeolocationCoordinates
    {
        [Key, Required]
        public Guid Id { get; set; }
        public float? accuracy { get; set; } = 1;
        public float? altitude { get; set; } = null;
        public float? altitudeAccuracy { get; set; } = null;
        public float? heading { get; set; } = null;
        public double? latitude { get; set; } = null;
        public double? longitude { get; set; } = null;
        public float? speed { get; set; } = null;

        [JsonConstructor]
        public GeolocationCoordinates() { }

        public GeolocationCoordinates(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

    }

}
