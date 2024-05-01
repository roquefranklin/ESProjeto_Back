using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ESProjeto_Back.Models
{

    public class Token
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string token { get; set; } = null!;
        public User User { get; set; } = null!;
        public Guid UserId { get; set; } = Guid.Empty!;
        [Required]
        public TipoDeToken TipoToken { get; set; }
        [Required]
        public DateTime Validate { get; set; }
        [Required]
        public bool IsValid { get; set; }
        public Token? RefreshToken { get; set; }
    }

    public class RenewTokenDto
    {
        [Required]
        [JsonPropertyName("refresh-token")]
        public string? RefreshToken { get; set; }
    }

    public enum TipoDeToken
    {
        Instagram,
        Facebook,
        Google,
        LoginPadrao
    }
}
