using GestaContinua.Application.Services;
using GestaContinua.Application.UseCases;
using GestaContinua.Domain.Repositories;
using GestaContinua.Infrastructure.Data;
using GestaContinua.Infrastructure.Repositories;
using GestaContinua.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Warning: Connection string 'DefaultConnection' not found in configuration. Please set it using user secrets or environment variables.");
    // Provide a default connection string for development - this should be overridden in production
    connectionString = "Host=localhost;Database=GestaContinuaDb;Username=postgres;Password=your_password";
}
builder.Services.AddDbContext<GestaContinuaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register domain repositories
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<ITaskRepository, EfTaskRepository>();
builder.Services.AddScoped<IProgressRecordRepository, EfProgressRecordRepository>();

// Register application services
builder.Services.AddScoped<ITaskScheduler, GestaContinua.Application.Services.TaskScheduler>();
builder.Services.AddScoped<IProgressRecordMappingService, ProgressRecordMappingService>();

// Register Telegram bot client and notification service
var botToken = builder.Configuration["TelegramBotToken"];
if (string.IsNullOrEmpty(botToken))
{
    Console.WriteLine("Warning: TelegramBotToken is not configured in app settings or user secrets.");
}
else
{
    Console.WriteLine("TelegramBotToken loaded from configuration.");
}

// Register ITelegramBotClient with the token from configuration
var botTokenForRegistration = builder.Configuration["TelegramBotToken"];
if (string.IsNullOrEmpty(botTokenForRegistration))
{
    Console.WriteLine("Warning: TelegramBotToken not configured. Please set it using user secrets or environment variables.");
    // Use a placeholder token to avoid runtime errors, but the service won't function properly
    botTokenForRegistration = "YOUR_TELEGRAM_BOT_TOKEN";
}
builder.Services.AddScoped<ITelegramBotClient>(provider => new TelegramBotClient(botTokenForRegistration));
builder.Services.AddScoped<INotificationService, TelegramNotificationService>();

// Register use cases
builder.Services.AddScoped<CreateTaskUseCase>();
builder.Services.AddScoped<ProcessUserResponseUseCase>();
builder.Services.AddScoped<UpdateTaskStatusUseCase>();
builder.Services.AddScoped<ScheduleRemindersUseCase>();

// Register background service for reminders
builder.Services.AddHostedService<ReminderBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GestaContinuaDbContext>();
    try
    {
        // Wait a bit to ensure PostgreSQL server is ready (useful for containerized setups)
        await Task.Delay(1000);

        context.Database.EnsureCreated();
        Console.WriteLine("Database ensured successfully.");
    }
    catch (NpgsqlException ex)
    {
        Console.WriteLine($"A PostgreSQL error occurred while ensuring the database: {ex.Message}");
        Console.WriteLine("Make sure PostgreSQL server is running and connection string is correct.");
        throw;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred while ensuring the database: {ex.Message}");
        throw;
    }
}

await app.RunAsync();