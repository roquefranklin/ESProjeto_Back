namespace ESProjeto_Back.Data.Dtos
{
    public class CreateSpecificReviewDto
    {
        public Guid Id { get; set; }
        public Guid ReviewId { get; set; }
        public string Type { get; set; }
        public int Score { get; set; }
    }
}
