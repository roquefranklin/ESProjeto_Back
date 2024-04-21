namespace ESProjeto_Back.Models
{
    public class Config
    {

        public JwtConfig Jwt { get; set; }
        public class JwtConfig
        {
            public int Timeout { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string Key { get; set; }
        }
    }
}
