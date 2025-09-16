using KvizHub.Models.Solution;
using KvizHub.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.SolutionConfiguration
{
    public class BooleanSolutionConfiguration : IEntityTypeConfiguration<BooleanSolution>
    {
        public void Configure(EntityTypeBuilder<BooleanSolution> builder)
        {
            builder.HasKey(x => x.QuizSolutionDetailsId);
            builder.Property(x => x.QuizSolutionDetailsId)
                   .ValueGeneratedOnAdd();

            builder.HasOne(x => x.QuizSolutionInfo)
                   .WithOne()
                   .HasForeignKey<BooleanSolution>(x => x.QuizSolutionInfoId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Answer)
                   .WithOne(x => x.BSolution)
                   .HasForeignKey<BooleanUserAnswer>(f => f.BooleanSolutionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
