using Microsoft.EntityFrameworkCore;
using PortfolioAPI;
using PortfolioAPI.Login;
using PortfolioAPI.Project;

var builder = WebApplication.CreateBuilder(args);

var corsPolicyName = "AllowFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        corsPolicyName,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// ✅ Register ProjectService (now correctly using PortfolioAPI.Project)
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// ✅ Ensure correct database connection setup
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.SetPostgresVersion(13, 0)
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Step 2: Apply CORS Before Routing
app.UseCors(corsPolicyName);
app.UseHttpsRedirection();
app.MapControllers(); // Maps attribute-routed controllers
app.Run();
