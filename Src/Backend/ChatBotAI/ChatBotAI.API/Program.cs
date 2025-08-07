using ChatBotAI.Application.Service;
using ChatBotAI.Infrastructure.Service;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngular",
            policy => policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod());
    });

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Configuration.AddUserSecrets<Program>();
    builder.Services.AddDbContext(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices();

    var app = builder.Build();

    app.UseCors("AllowAngular");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
