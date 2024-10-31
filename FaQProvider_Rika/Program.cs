using FaQProvider_Rika.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<QuestionDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));
        });
    })
    .Build();

host.Run();
