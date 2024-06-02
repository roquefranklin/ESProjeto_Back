namespace ESProjeto_Back.Data.Dtos
{
    public class NewPassword
    {
        public string Password { get; set; } = string.Empty;
        public string ConfirmationPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string forgotenCode { get; set; } = string.Empty;

        public bool PasswordMatch()
        {
            bool areEmptyPassword = Password.Length == 0 || ConfirmationPassword.Length == 0;
            if (areEmptyPassword)
            {
                return false;
            }
            return  Password == ConfirmationPassword ;
        }

    }
}
