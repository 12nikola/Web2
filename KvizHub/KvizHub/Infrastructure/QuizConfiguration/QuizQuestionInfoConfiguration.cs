using KvizHub.Models.Quiz_Information;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.QuizConfiguration
{
    public class QuizQuestionInfoConfiguration : IEntityTypeConfiguration<QuizQuestionInfo>
    {
        public void Configure(EntityTypeBuilder<QuizQuestionInfo> builder)
        {
            builder.HasKey(x => x.ParentQuizId);
            builder.Property(x => x.ParentQuizId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.QuestionText)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.QuestionType)
                   .IsRequired();

            builder.HasOne(q => q.Category)
                   .WithMany(qz => qz.Questions)
                   .HasForeignKey(q => q.CategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(q => q.Category)
                   .WithMany(c => c.Questions)
                   .HasForeignKey(q => q.CategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
