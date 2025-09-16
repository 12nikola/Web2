using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KvizHub.Models;
using KvizHub.Models.Quiz_Response;

namespace QuizWebServer.Infrastructure.Configurations
{
    public class SingleChoiceQuestionDetailsConfiguration : IEntityTypeConfiguration<SingleOptionDetails>
    {
        public void Configure(EntityTypeBuilder<SingleOptionDetails> builder)
        {
            builder.HasKey(x => x.QuizQuestionDetailsId);
            builder.Property(x => x.QuizQuestionDetailsId)
                   .ValueGeneratedOnAdd();

            builder.HasOne(x => x.QuizQuestion)
                   .WithOne()
                   .HasForeignKey<SingleOptionDetails>(x => x.QuizQuestionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
