using KvizHub.Models.Solution;
using KvizHub.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.SolutionConfiguration
{
    public class TextEntrySolutionConfiguration : IEntityTypeConfiguration<TextEntrySolution>
    {
        public void Configure(EntityTypeBuilder<TextEntrySolution> builder)
        {
            builder.HasKey(x => x.QuizSolutionDetailsId);
            builder.Property(x => x.QuizSolutionDetailsId)
                   .ValueGeneratedOnAdd();

            builder.HasOne(x => x.QuizSolutionInfo)
                   .WithOne()
                   .HasForeignKey<TextEntrySolution>(x => x.QuizSolutionInfoId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Answer)
                   .WithOne(x => x.TESolution)
                   .HasForeignKey<TextEntryUserAnswer>(f => f.TextEntrySolutionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
