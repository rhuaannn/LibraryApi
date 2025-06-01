using Library_Application.Interfaces;
using Library_Application.Services;
using Library_Domain.Model;
using Library_Infra.Connect;
using Library_Infra.Redis;
using Library_Infra.RepositoryBook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<DBConnection>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Library-Infra")
    ));

builder.Services.AddScoped<ICachingService, Caching>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    var redisHost = builder.Configuration["Redis:Host"];
    var redisPort = builder.Configuration["Redis:Port"];
    options.Configuration = $"{redisHost}:{redisPort}";
    options.InstanceName = "instance";
});


builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DBConnection>();

var JwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(JwtSettingsSection);

var jwtSettings = JwtSettingsSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true, 
        ValidAudience = jwtSettings.Audience, 
        ValidIssuer = jwtSettings.Issuer 
    };
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
