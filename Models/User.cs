using System.ComponentModel.DataAnnotations;

namespace ESProjeto_Back.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O Primeiro Nome do Usuário é Obrigatório")]
        public string FirsName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
    }
}
