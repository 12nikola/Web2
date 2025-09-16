using KvizHub.Models.Quiz_Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.DetailsConfiguration
{
    public class BooleanDetailsConfiguration:IEntityTypeConfiguration<BooleanDetails>
    {
        public void Configure(EntityTypeBuilder<BooleanDetails> builder)
        {
            builder.HasKey(b => b.QuizQuestionDetailsId);
            builder.HasOne(b => b.CorrectAnswer)
                   .WithOne()
                   .HasForeignKey<BooleanDetails>(b => b.QuizQuestionId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
