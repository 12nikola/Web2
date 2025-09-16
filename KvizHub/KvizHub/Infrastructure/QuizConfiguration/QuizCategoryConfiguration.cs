using KvizHub.Models.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.QuizConfiguration
{
    public class QuizCategoryConfiguration : IEntityTypeConfiguration<QuizCategory>
    {
        public void Configure(EntityTypeBuilder<QuizCategory> builder)
        {
            builder.HasKey(x => x.CategoryId);
            builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();

            builder.Property(x => x.CategoryName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(x => x.CategoryName)
                   .IsUnique();

            builder.HasMany(c => c.Questions)
                   .WithOne(q => q.Category)
                   .HasForeignKey(q => q.CategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
