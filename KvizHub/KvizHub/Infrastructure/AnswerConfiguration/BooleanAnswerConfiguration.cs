using KvizHub.Models.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KvizHub.Infrastructure.AnswerConfiguration
{
    public class BooleanAnswerConfiguration: IEntityTypeConfiguration<MultipleOptionAnswer>
    {
        public void Configure(EntityTypeBuilder<MultipleOptionAnswer> builder)
        {
            builder.HasKey(ba => ba.ResponseId);
            builder.Property(ba => ba.ResponseId).ValueGeneratedOnAdd();
            builder.Property(ba => ba.Content).IsRequired();
            builder.Property(ba => ba.Correct).HasMaxLength(1000);
        }
    }
}
