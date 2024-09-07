using ESProjeto_Back.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESProjeto_Back.Data.Configuration
{
    public class SpecificReviewConfiguration : IEntityTypeConfiguration<SpecificReview>
    {

        public void Configure(EntityTypeBuilder<SpecificReview> builder)
        {
            builder
                .HasKey(review => review.Id);

            builder
                .HasOne(review => review.Review)
                .WithMany(review => review.SpecificReviews)
                .HasForeignKey(review => review.ReviewId)
                .IsRequired();
        }
    }
}
