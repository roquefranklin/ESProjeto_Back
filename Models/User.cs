using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ESProjeto_Back.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        [EmailAddress(ErrorMessage = "The {0} is in a incorrect format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(100, ErrorMessage = "The {0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(1000, ErrorMessage = "The {0} must have, at least {2} and a max {1}", MinimumLength = 1)]
        public string? NickName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Perfil { get; set; }
        public ICollection<Token> Tokens { get; }
        public ICollection<Review> Reviews { get; }
        [StringLength(6, ErrorMessage = "The {0} have to be a 6 lenght string of numbers values", MinimumLength = 6)]
        public string? RecoveryCode { get; internal set; }
    }

    public class UserLoginResponse
    {
        public UserLoginResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = 3600;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }

    public class UserLogin()
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class NewUser
    {
        public string Nome { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmationPassword { get; set; } = string.Empty;

        public bool confirmPassword()
        {
            bool noEmptyPassword = Password != null && Password.Length > 0;
            bool noEmptyConfirmPassword = ConfirmationPassword != null && ConfirmationPassword.Length > 0;
            bool arePasswordsEquals = Password == ConfirmationPassword;
            return noEmptyPassword && noEmptyConfirmPassword && arePasswordsEquals;
        }
    }

    public class UpdateUser
    {
        public required string Email { get; set; }
        public string? Nome { get; set; } = string.Empty;
        public string? NickName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Perfil { get; set; } = string.Empty;

        public bool EmptyAlterations()
        {
            return string.IsNullOrEmpty(Nome) && string.IsNullOrEmpty(NickName) && string.IsNullOrEmpty(PhoneNumber) && string.IsNullOrEmpty(Perfil);
        }
    }
}
