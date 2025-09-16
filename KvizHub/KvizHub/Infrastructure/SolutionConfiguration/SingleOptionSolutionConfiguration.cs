using KvizHub.Models.Solution;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.SolutionConfiguration
{
    public class SingleOptionSolutionConfiguration : IEntityTypeConfiguration<SingleOptionSolution>
    {
        public void Configure(EntityTypeBuilder<SingleOptionSolution> builder)
        {
            builder.HasKey(x => x.QuizSolutionDetailsId);
            builder.Property(x => x.QuizSolutionDetailsId)
                   .ValueGeneratedOnAdd();

            builder.HasOne(x => x.QuizSolutionInfo)
                   .WithOne()
                   .HasForeignKey<SingleOptionSolution>(x => x.QuizSolutionInfoId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Answer)
                   .WithOne(x => x.SOSolution)
                   .HasForeignKey<SingleOptionSolution>(f => f.QuizSolutionInfoId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
