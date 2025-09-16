using KvizHub.Models.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.AnswerConfiguration
{
    public class MultipleOptionAnswerConfiguration : IEntityTypeConfiguration<MultipleOptionAnswer>
    {
        public void Configure(EntityTypeBuilder<MultipleOptionAnswer> builder)
        {
            builder.HasKey(m => m.QuestionDetailsId);
            builder.Property(m => m.Content).IsRequired().HasMaxLength(500);
            builder.Property(m => m.Correct).IsRequired();
            builder.HasOne(m => m.QuestionDetails)
                   .WithMany(q => q.Answers)
                   .HasForeignKey(q => q.ResponseId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
