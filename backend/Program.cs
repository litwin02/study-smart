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

// Sprawdzanie czy jest po��czenie z baz� danych
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        logger.LogInformation("Po��czenie z baz� danych zosta�o nawi�zane pomy�lnie.");
    }
    catch (Exception ex)
    {
        logger.LogError($"B��d po��czenia z baz� danych: {ex.Message}");
    }
}


app.Run();