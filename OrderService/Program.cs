using Microsoft.EntityFrameworkCore;
using OrdersService.Database;
using OrdersService.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurazione del DbContext con SQLite (file database orders.db)
builder.Services.AddDbContext<OrderDB>(options =>
    options.UseSqlite("Data Source=orders.db"));

// Registra il servizio di logica ordini
builder.Services.AddScoped<ServiceOrders>();

// Abilita CORS (consente tutte le origini per test)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
var app = builder.Build();

// Configura porta di ascolto per il microservizio Ordini (porta 5001)
app.Urls.Add("http://localhost:5001");

app.UseCors("AllowAll");
app.MapControllers();

// Assicura che il database e la tabella Ordini siano creati
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderDB>();
    db.Database.EnsureCreated();
}

app.Run();
