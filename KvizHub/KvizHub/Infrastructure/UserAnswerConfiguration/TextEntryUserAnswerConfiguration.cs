using KvizHub.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.UserAnswerConfiguration
{
    public class TextEntryUserAnswerConfiguration : IEntityTypeConfiguration<TextEntryUserAnswer>
    {
        public void Configure(EntityTypeBuilder<TextEntryUserAnswer> builder)
        {
            builder.HasKey(x => x.ParticipantResponseId);
            builder.Property(x => x.ParticipantResponseId)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ResponseText)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasOne(x => x.TESolution)
                   .WithOne(x => x.Answer)
                   .HasForeignKey<TextEntryUserAnswer>(x => x.TextEntrySolutionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
