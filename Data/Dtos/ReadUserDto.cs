namespace ESProjeto_Back.Data.Dtos
{
    public class ReadUserDto
    {
        public string FirsName { get;}
        public string? LastName { get; }
        public int? Age { get; }
        public DateTime HoraDaConsult { get; set; } = DateTime.Now;
    }
}
