using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizWebServer.Models.QuizSolution;

namespace KvizHub.Infrastructure.QuizConfiguration
{
    public class QuizAttemptConfiguration : IEntityTypeConfiguration<QuizAttempt>
    {
        public void Configure(EntityTypeBuilder<QuizAttempt> builder)
        {
            builder.HasKey(x => x.QuizAttemptId);
            builder.Property(x => x.QuizAttemptId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.CorrectAnswersCount)
                   .IsRequired();

            builder.Property(x => x.TotalAnswersCount)
                   .IsRequired();

            builder.Property(x => x.IncorrectAnswersCount)
                   .IsRequired();

            builder.Property(x => x.ScorePoints)
                    .IsRequired();

            builder.Property(x => x.ScorePercentage)
                   .IsRequired();

            builder.Property(x => x.AttemptDate)
                   .IsRequired();

            builder.Property(x => x.Duration)
                   .IsRequired();

        }
    }
}
