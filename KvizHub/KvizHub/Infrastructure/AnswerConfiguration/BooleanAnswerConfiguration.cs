using KvizHub.Models.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.AnswerConfiguration
{
    public class BooleanAnswerConfiguration: IEntityTypeConfiguration<BooleanAnswer>
    {
        public void Configure(EntityTypeBuilder<BooleanAnswer> builder)
        {
            builder.HasKey(b => b.QuestionDetailsId);
            builder.Property(b => b.Content).IsRequired().HasMaxLength(500);
            builder.Property(b => b.Correct).IsRequired();
            builder.HasOne(b => b.QuestionDetails)
                   .WithOne(q => q.CorrectAnswer)
                   .HasForeignKey<BooleanAnswer>(q => q.QuestionDetailsId)
                   .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
