using KvizHub.Models.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.AnswerConfiguration
{
    public class SingleOptionAnswerConfiguration : IEntityTypeConfiguration<SingleOptionAnswer>
    {
        public void Configure(EntityTypeBuilder<SingleOptionAnswer> builder)
        {
            builder.HasKey(s => s.QuestionDetailsId);
            builder.Property(s => s.Content).IsRequired().HasMaxLength(500);
            builder.Property(s => s.Correct).IsRequired();
            builder.HasOne(s => s.QuestionDetails)
                   .WithOne()
                   .HasForeignKey<SingleOptionAnswer>(q => q.ResponseId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
