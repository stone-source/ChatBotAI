using ChatBotAI.Application.AiEngineResponses.Handlers;
using ChatBotAI.Application.Conversations.Handlers;
using ChatBotAI.Application.UserRequests.Handlers;
using Microsoft.EntityFrameworkCore;
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
    builder.Services.AddDbContext<ChatBotAI.Infrastructure.DatabaseContext.ChatbotAiContext>(
        options => options
            .UseSqlServer(builder.Configuration
            .GetConnectionString("DefaultConnectionString")));

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AiEngineResponseSaveResponseHandler).Assembly));
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AIEngineResponseUpdateRateHandler).Assembly));
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ConversationGetAllByUserIdHandler).Assembly));
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserRequestCompletionsHandler).Assembly));
    
    builder.Services.AddTransient<ChatBotAI.Infrastructure.Abstract.IAiEngineResponseRepository, ChatBotAI.Infrastructure.Repository.AiEngineResponseRepository>();
    builder.Services.AddTransient<ChatBotAI.Infrastructure.Abstract.IUserRequestRepository, ChatBotAI.Infrastructure.Repository.UserRequestRepository>();

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
