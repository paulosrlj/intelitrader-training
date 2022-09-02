using user_api.database;
using user_api.Services;
using user_api.Repositories;
using user_api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvcCore().AddDataAnnotations();
builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  var host = builder.Configuration["DBHOST"] ?? "localhost";
  var port = builder.Configuration["DBPORT"] ?? "3306";
  var password = builder.Configuration["MY_SQL_PASSWORD"] ?? "123";
  var userId = builder.Configuration["MYSQL_USER"] ?? "root";
  var usersDatabase = builder.Configuration["MYSQL_DATABASE"] ?? "user-api-db";

  connectionString = $"server={host};port={port};database={usersDatabase};uid={userId};password={password}";

  options.UseMySql(connectionString, ServerVersion.Parse("8.0.30-mysql"));
}, ServiceLifetime.Singleton);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll", b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});

builder.Services.AddRazorPages();

// Injeção de dependências
// lifecycle e middleware
builder.Services.AddSingleton<DbContext, ApplicationDbContext>();
builder.Services.AddSingleton<IUserRepository<User>, UserRepository>();
builder.Services.AddSingleton<IUserService<User>, UserService>();

var app = builder.Build();
app.UseHttpLogging();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Run Migrations
using (var scope = app.Services.CreateScope())
{
  var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  dataContext.Database.Migrate();
}

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
  endpoints.MapDefaultControllerRoute();
});

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");
  endpoints.MapHub<UserService>("/users-hub");
});

app.UseStaticFiles();

app.Run();
