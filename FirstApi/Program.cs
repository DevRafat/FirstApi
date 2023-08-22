using System.Text;
using FirstApi;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FirstApi.Filters;
using FirstApi.MiddleWare;
using FirstApi.Service.Cache;
using FirstApi.Service.loggedIn;
using FirstApi.Service.Major;
using FirstApi.Service.User;
using Hangfire;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AplicationDbContext>(c =>
    c.UseSqlServer(connStr));
//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();//Memory Cache register
builder.Services.AddScoped<IMajorService, MajorService>();
builder.Services.AddScoped<ICache, MemoryCache>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoggedInService, LoggedInService>();
builder.Services.AddScoped<CustomFilter>();




builder.Services.AddHangfire(x => x.UseSqlServerStorage(connStr));
builder.Services.AddHangfireServer();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors();
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//var allowedDomains = new[] { "http://aaa.somewhere.com", "https://aaa.somewhere.com", "http://bbb.somewhere.com", "https://bbb.somewhere.com" };

app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHangfireDashboard("/dashboard");
app.UseMiddleware<MiddleWareAuth>();
app.UseMiddleware<JwtMiddleWare>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
