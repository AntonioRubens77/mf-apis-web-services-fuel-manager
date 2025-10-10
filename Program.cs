using System;
using Microsoft.EntityFrameworkCore;
using MfApisWebServicesFuelManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Diagnóstico
Console.WriteLine("[diag] After CreateBuilder");

// Handlers globais
AppDomain.CurrentDomain.UnhandledException += (s, e) =>
    Console.WriteLine("[diag] UnhandledException: " + (e.ExceptionObject?.ToString() ?? "<null>"));
TaskScheduler.UnobservedTaskException += (s, e) =>
{
    Console.WriteLine("[diag] UnobservedTaskException: " + e.Exception);
    e.SetObserved();
};

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

// ⭐ Swagger estável + IDs únicos para evitar conflitos de tipos com mesmo nome
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(t => t.FullName); // evita "Conflicting schemaIds"
});

Console.WriteLine("[diag] After services registration");

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure())); // ajuda em falhas transitórias


var app = builder.Build();
Console.WriteLine("[diag] After Build()");

// Lifecycle logs
var lifetime = app.Lifetime;
lifetime.ApplicationStarted.Register(() => Console.WriteLine("[diag] ApplicationStarted"));
lifetime.ApplicationStopping.Register(() => Console.WriteLine("[diag] ApplicationStopping"));
lifetime.ApplicationStopped.Register(() => Console.WriteLine("[diag] ApplicationStopped"));

// ⭐ Ative o Swagger SEM depender do ambiente (para evitar surpresa)
app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    // o.RoutePrefix = string.Empty; // opcional: abrir Swagger em "/"
});

app.UseAuthorization();

Console.WriteLine("[diag] Before MapControllers");
app.MapControllers();

// (Opcional) atalho para a UI
app.MapGet("/", () => Results.Redirect("/swagger"));

Console.WriteLine("[diag] Before Run");
app.Run();