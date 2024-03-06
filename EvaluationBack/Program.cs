using EvaluationBack.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();

using (IServiceScope scope = host.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;

    var dbContext = serviceProvider.GetService<DbContextEntity>();
    dbContext.Database.EnsureCreated();
}
