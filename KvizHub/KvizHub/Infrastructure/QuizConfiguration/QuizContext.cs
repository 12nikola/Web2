using KvizHub.Models.Answers;
using KvizHub.Models.Base;
using KvizHub.Models.Quiz;
using KvizHub.Models.Quiz_Details;
using KvizHub.Models.Quiz_Information;
using KvizHub.Models.Quiz_Response;
using KvizHub.Models.Solution;
using KvizHub.Models.User;
using Microsoft.EntityFrameworkCore;
using QuizWebServer.Models.QuizSolution;

namespace KvizHub.Infrastructure.QuizConfiguration
{
    public class QuizContext: DbContext
    {
        //User
        public DbSet<Users> Users { get; set; }

        //QuestionCategories
        public DbSet<QuizCategory> QuizCategories { get; set; }

        //Quiz 
        public DbSet<Quizz> Quizzes { get; set; }

        public DbSet<QuizQuestionInfo> QuizQuestionInformations { get; set; }

        //Quiz-Details
        public DbSet<SingleOptionDetails> SingleOptionDetails { get; set; }
        public DbSet<MultipleOptionDetails> MultipleOptionDetails { get; set; }
        public DbSet<BooleanDetails> BooleanDetails { get; set; }
        public DbSet<TextEntryDetails> TextEntryDetails { get; set; }

        //Quiz-Answers
        public DbSet<SingleOptionAnswer> SingleOptionAnswer { get; set; }
        public DbSet<MultipleOptionAnswer> MultipleOptionAnswer { get; set; }
        public DbSet<BooleanAnswer> BooleanAnswer { get; set; }
        public DbSet<TextEntryAnswer> TextEntryAnswer { get; set; }

        //SoulutionAttempt 
        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        public DbSet<QuizQuestionInfo> QuizQuestionInfo { get; set; }

        //Solution-Details
        public DbSet<SingleOptionSolution> SingleOptionSolution { get; set; }
        public DbSet<MultipleOptionSolution> MultipleOptionSolution { get; set; }
        public DbSet<BooleanSolution> BooleanSolution { get; set; }
        public DbSet<TextEntrySolution> TextEntrySolution { get; set; }

        //Solution-UserAnswer
        public DbSet<SingleOptionUserAnswer> SingleOptionUserAnswer { get; set; }
        public DbSet<MultipleOptionUserAnswer> MultipleOptionUserAnswer { get; set; }
        public DbSet<TextEntryUserAnswer> TextEntryUserAnswer { get; set; }
        public DbSet<BooleanUserAnswer> BooleanUserAnswer { get; set; }

        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<QuizSolutionDetailBase>();
            modelBuilder.Ignore<QuizQuestionDetailBase>();
            modelBuilder.Ignore<ResponseBase>();
            modelBuilder.Ignore<ParticipantResponseBase>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizContext).Assembly);
        }

    }
}
