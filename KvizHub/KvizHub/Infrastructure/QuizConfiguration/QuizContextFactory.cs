using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KvizHub.Infrastructure.QuizConfiguration
{
    public class QuizHubDbContextFactory : IDesignTimeDbContextFactory<QuizContext>
    {
        public QuizContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json")
                                                          .Build();

            var connectionString = configuration.GetConnectionString("webprojekat");

            var optionsBuilder = new DbContextOptionsBuilder<QuizContext>();

            optionsBuilder.UseMySql(
                            connectionString,
                            ServerVersion.AutoDetect(connectionString)
            );

            return new QuizContext(optionsBuilder.Options);
        }

    }
}
