using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.QuizConfiguration
{
    public class QuizQuestionSolutionConfiguration : IEntityTypeConfiguration<QuizQuestionSolutionInfo>
    {
        public void Configure(EntityTypeBuilder<QuizQuestionSolutionInfo> builder)
        {
            builder.HasKey(x => x.QuizQuestionSolutionInfoId);
            builder.Property(x => x.QuizQuestionSolutionInfoId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.QuizQuestion)
                   .IsRequired();

            builder.HasOne(q => q.QuizQuestion)
                   .WithMany()
                   .HasForeignKey(q => q.QuizQuestionInfoId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(q => q.QuizAttempt)
                   .WithMany()
                   .HasForeignKey(q => q.QuizAttemptId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
