using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
