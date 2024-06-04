using Microsoft.EntityFrameworkCore;
using StudiaPraca.Contexts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddCors();
	builder.Services.AddControllers();
}
var app = builder.Build();
{
	app.UseHttpsRedirection();
	app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
	app.MapControllers();
}


// logger do konsolki
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Sprawdzanie czy jest po³¹czenie z baz¹ danych
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        logger.LogInformation("Po³¹czenie z baz¹ danych zosta³o nawi¹zane pomyœlnie.");
    }
    catch (Exception ex)
    {
        logger.LogError($"B³¹d po³¹czenia z baz¹ danych: {ex.Message}");
    }
}


app.Run();