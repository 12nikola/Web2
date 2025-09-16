using KvizHub.Models.Quiz_Details;
using KvizHub.Models.Quiz_Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.DetailsConfiguration
{
    public class TextEntryDetailsConfiguration:IEntityTypeConfiguration<TextEntryDetails>
    {
        public void Configure(EntityTypeBuilder<TextEntryDetails> builder)
        {
            builder.HasKey(x => x.QuizQuestionDetailsId);
            builder.Property(x => x.QuizQuestionDetailsId)
                   .ValueGeneratedOnAdd();

            builder.HasOne(x => x.QuizQuestion)
                   .WithOne()
                   .HasForeignKey<TextEntryDetails>(x => x.QuizQuestionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CorrectAnswer)
                   .WithOne(x => x.QuestionDetails)
                   .HasForeignKey<SingleOptionDetails>(f => f.QuizQuestionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
