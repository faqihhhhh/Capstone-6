using DocumentManagement.API.Helpers;
using DocumentManagement.API;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using DocumentManagement.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<JobService>();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var connectionString = builder.Configuration.GetConnectionString("DocumentDbConnectionString");
// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

var app = builder.Build();

try
{
    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
    {   // Disini kita dapat mengakses layanan dalam lingkup baru untuk OBJEK DocumentContext
        var context = serviceScope.ServiceProvider.GetRequiredService<DocumentContext>();
        // untuk variabel context dimana scopednya adalah objek DocumentContext akan digunakan untuk services migrate Database
        context.Database.Migrate();
    } // Scope akand dihancurkan di sini dan objek-objeknya akan dihapus
}
catch (System.Exception)
{
    throw;
}

ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
startup.Configure(app, app.Environment, loggerFactory);

app.UseHangfireDashboard();
app.MapHangfireDashboard();


JobService jobService = app.Services.GetRequiredService<JobService>();
jobService.StartScheduler();

app.Run();