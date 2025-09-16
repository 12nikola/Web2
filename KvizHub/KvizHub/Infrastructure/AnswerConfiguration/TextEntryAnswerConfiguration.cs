using KvizHub.Models.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.AnswerConfiguration
{
    public class TextEntryAnswerConfiguration: IEntityTypeConfiguration<TextEntryAnswer>
    {
        public void Configure(EntityTypeBuilder<TextEntryAnswer> builder)
        {
            builder.HasKey(t => t.QuestionDetailsId);
            builder.Property(t => t.Content).IsRequired().HasMaxLength(500);
            builder.Property(t => t.Correct).IsRequired();
            builder.HasOne(t => t.QuestionDetails)
                   .WithOne(q => q.CorrectAnswer)
                   .HasForeignKey<TextEntryAnswer>(q => q.ResponseId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
