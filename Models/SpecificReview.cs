using System.ComponentModel.DataAnnotations;

namespace ESProjeto_Back.Models
{
    public class SpecificReview
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string type { get; set; }
        [Required, Range(0, 5, ErrorMessage = "A pontuação para a avaliação específica deve estar entre 0 e 5")]
        public int SpecificReviewScore { get; set; }
        [Required]
        public Guid ReviewId { get; set; }
        public Review Review {  get; set; } 
    }
}
