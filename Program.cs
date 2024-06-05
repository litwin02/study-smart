using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using StudiaPraca.Contexts;

Env.Load();

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")));
    builder.Services.AddCors();
    builder.Services.AddControllers();
}
var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.MapControllers();
}

var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Check database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        logger.LogInformation("Database connection got succesfully established");
    }
    catch (Exception ex)
    {
        logger.LogError($"Error while connecting to the database: {ex.Message}");
    }
}


app.Run();