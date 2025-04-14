using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ReceiptReimbursementAPI.Application.Interfaces;
using ReceiptReimbursementAPI.Application.Services;
using ReceiptReimbursementAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IReceiptService, ReceiptService>();

// Connect to SQL Server
builder.Services.AddDbContext<ReimbursementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ReceiptReimbursementAPI.Data")
    ));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ReimbursementDbContext>();
        db.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

// Serve files from the "Uploads" directory
var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Uploads"
});

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();