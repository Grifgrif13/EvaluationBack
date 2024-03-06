using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EvaluationBack.DAL.startup
{
    public class DbContextEntityFactory : IDesignTimeDbContextFactory<DbContextEntity>
    {
        public DbContextEntity CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContextEntity>();

            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString"));

            return new DbContextEntity(optionsBuilder.Options);
        }
    }
}
