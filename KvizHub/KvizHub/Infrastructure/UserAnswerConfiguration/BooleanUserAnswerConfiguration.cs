using KvizHub.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.UserAnswerConfiguration
{
    public class BooleanUserAnswerConfiguration:IEntityTypeConfiguration<BooleanUserAnswer>
    {
        public void Configure(EntityTypeBuilder<BooleanUserAnswer> builder) { 
            builder.HasKey(x => x.ParticipantResponseId);
            builder.Property(x => x.ParticipantResponseId)
                   .ValueGeneratedOnAdd();

        builder.Property(x => x.ResponseText)
                   .HasMaxLength(100)
                   .IsRequired();

        builder.HasOne(x => x.BSolution)
                   .WithOne(x => x.Answer)
                   .HasForeignKey<BooleanUserAnswer>(x => x.BooleanSolutionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
    }
}
}
