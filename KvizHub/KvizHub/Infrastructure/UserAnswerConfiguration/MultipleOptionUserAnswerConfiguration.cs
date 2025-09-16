using KvizHub.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.UserAnswerConfiguration
{
    public class MultipleOptionUserAnswerConfiguration:IEntityTypeConfiguration<MultipleOptionUserAnswer>
    {
        public void Configure(EntityTypeBuilder<MultipleOptionUserAnswer> builder)
        {
            builder.HasKey(x => x.ParticipantResponseId);
            builder.Property(x => x.ParticipantResponseId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ResponseText)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasOne(x => x.MOSolution)
                   .WithMany(x => x.Answers)
                   .HasForeignKey(x => x.MultipleOptionSolutionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
