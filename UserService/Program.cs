using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura il DbContext per Utenti con SQLite (users.db)
builder.Services.AddDbContext<UserDB>(options =>
    options.UseSqlite("Data Source=users.db"));

builder.Services.AddScoped<ServiceUser>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
var app = builder.Build();

// Porta HTTP per il microservizio Utenti (5003)
app.Urls.Add("http://localhost:5003");

app.UseCors("AllowAll");
app.MapControllers();

// Crea il database/tabella Utenti se non esiste
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDB>();
    db.Database.EnsureCreated();
}

app.Run();
