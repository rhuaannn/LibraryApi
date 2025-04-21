using Library_Application.Interfaces;
using Library_Application.Services;
using Library_Infra.Connect;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<DBConnection>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Library-Infra")
    ));

builder.Services.AddControllers();
builder.Services.AddScoped<IBook, BookServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(BookMappingProfile));
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
