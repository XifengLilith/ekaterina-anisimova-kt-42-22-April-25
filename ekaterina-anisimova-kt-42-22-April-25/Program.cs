using ekaterina_anisimova_kt_42_22_April_25.Database;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using ekaterina_anisimova_kt_42_22_April_25.Services;
using ekaterina_anisimova_kt_42_22_April_25.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<TeacherDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    // ������������ ��� ������� ����������, ������������ � ServiceExtensions
    builder.Services.AddAppServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
}
finally
{
    LogManager.Shutdown();
}