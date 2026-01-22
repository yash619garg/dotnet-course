using api.Data;
using api.Interfaces;
using api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// This creates an object called builder which is used to configure:
// ✅ services (Dependency Injection)
// ✅ middleware pipeline
// ✅ configuration (appsettings.json)
// ✅ logging, environment, etc.
// 📌 Think of it as: setup phase of the app


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// These add Swagger services so that:
// ✅ your API endpoints are automatically documented
// ✅ you can test API using Swagger UI

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

});

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// (A) builder.Services
// This is the Dependency Injection container.
// Definition:
// DI container is where you register services that your app will use.

// (B) AddDbContext<ApplicationDBContext>()
// This registers your ApplicationDBContext as a service.
// Whenever your code needs ApplicationDBContext, ASP.NET Core will automatically create and provide it.
// This is Dependency Injection in action.

// (C) options.UseSqlServer(...)
// This tells EF Core: Your database provider is SQL Server
// So EF Core will generate SQL queries compatible with SQL Server.

// (D) GetConnectionString("DefaultConnection")
// This fetches connection string from: appsettings.json

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
// You are telling DI container:
// If any class asks for IStockRepository, give an instance of StockRepository.
// This is the core of Repository Pattern with DI.

// Why AddScoped here?
// Scoped lifetime means: One StockRepository instance per HTTP request.

// Why this is perfect?
// Because repository uses DbContext, and DbContext is also scoped.
// So in one request:
// Same DbContext instance is used through repository
// safe + correct EF Core behavior


var app = builder.Build();
// Now the app is built, meaning:
// services setup is done
// now middleware + routes will be configured
// Think: setup complete, now application starts preparing pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Swagger is enabled only when app is in development mode.
// This is done for security & performance reasons.

app.UseHttpsRedirection();
// This middleware forces HTTP requests to redirect to HTTPS. 
// improves security

app.MapControllers();


app.Run();
// Starts the application.
// This begins listening for incoming HTTP requests

