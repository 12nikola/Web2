using KvizHub.Models.Solution;
using KvizHub.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.SolutionConfiguration
{
    public class MultipleOptionSolutionConfiguration : IEntityTypeConfiguration<MultipleOptionSolution>
    {
        public void Configure(EntityTypeBuilder<MultipleOptionSolution> builder)
        {
            builder.HasKey(x => x.QuizSolutionDetailsId);
            builder.Property(x => x.QuizSolutionDetailsId)
                   .ValueGeneratedOnAdd();

            builder.HasOne(x => x.QuizSolutionInfo)
                   .WithOne()
                   .HasForeignKey<MultipleOptionSolution>(x => x.QuizSolutionInfoId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Answers)
                   .WithOne(x => x.MOSolution)
                   .HasForeignKey(f => f.MultipleOptionSolutionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
