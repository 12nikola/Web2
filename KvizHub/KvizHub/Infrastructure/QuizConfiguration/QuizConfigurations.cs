using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.QuizConfiguration
{
    public class QuizConfigurations : IEntityTypeConfiguration<KvizHub.Models.Quiz.Quizz>
    {
        public void Configure(EntityTypeBuilder<KvizHub.Models.Quiz.Quizz> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Difficulty)
                   .IsRequired();

            builder.Property(x => x.TimeLimit)
                   .IsRequired();

            builder.Property(x => x.UpdatedAt)
                   .IsRequired();
        }
    }
}
