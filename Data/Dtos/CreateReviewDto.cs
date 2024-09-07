namespace ESProjeto_Back.Data.Dtos
{
    public class CreateReviewDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid StopPointId { get; set; }
        public string? Description { get; set; }
        public int Score { get; set; }
    }
}
