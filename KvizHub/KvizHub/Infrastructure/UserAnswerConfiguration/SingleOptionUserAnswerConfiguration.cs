using KvizHub.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.UserAnswerConfiguration
{
    public class SingleOptionUserAnswerConfiguration : IEntityTypeConfiguration<SingleOptionUserAnswer>
    {
        public void Configure(EntityTypeBuilder<SingleOptionUserAnswer> builder)
        {
            builder.HasKey(x => x.ParticipantResponseId);
            builder.Property(x => x.ParticipantResponseId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ResponseText)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasOne(x => x.SOSolution)
                   .WithOne(x => x.Answer)
                   .HasForeignKey<SingleOptionUserAnswer>(x => x.SingleOptionSolutionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
