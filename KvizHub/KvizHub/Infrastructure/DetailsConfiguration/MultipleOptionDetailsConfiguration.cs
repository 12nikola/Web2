using KvizHub.Models.Quiz_Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.DetailsConfiguration
{
    public class MultipleOptionDetailsConfiguration: IEntityTypeConfiguration<MultipleOptionDetails>
    {
        public void Configure(EntityTypeBuilder<MultipleOptionDetails> builder)
        {
            builder.HasKey(x => x.QuizQuestionDetailsId);
            builder.Property(x => x.QuizQuestionDetailsId)
                   .ValueGeneratedOnAdd();

            builder.HasOne(x => x.QuizQuestion)
                   .WithOne()
                   .HasForeignKey<MultipleOptionDetails>(x => x.QuizQuestionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Answers)
                   .WithOne(x => x.QuestionDetails)
                   .HasForeignKey(x => x.QuestionDetailsId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
