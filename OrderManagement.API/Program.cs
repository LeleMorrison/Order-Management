using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Abilita CORS per consentire chiamate da qualsiasi origine (solo per sviluppo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configura l'app per ascoltare sulla porta 5000 (HTTP)
app.Urls.Add("http://localhost:5000");

app.UseCors("AllowAll");
app.MapControllers();

app.Run();
