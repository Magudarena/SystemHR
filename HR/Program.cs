using HR.Services.JWT;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoapCore;
using System.Text;
using SystemHR.Models;
using SystemHR.Services.Soap;

var builder = WebApplication.CreateBuilder(args);

// Configuration
Console.WriteLine($"Current configuration: {builder.Environment.EnvironmentName}");

// Konfiguracja baz danych
builder.Services.AddDbContext<SystemHRContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SystemHRConnection")));

// Rejestracja us�ug
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthentication(options =>
{
     options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/Logowanie/Logowanie";
    options.AccessDeniedPath = "/Logowanie/Odmowa";
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var secret = builder.Configuration["API:Secret"] ?? throw new InvalidOperationException("API:Secret configuration is missing.");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "HRSystem",
        ValidAudience = "HRSystemClient",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});

builder.Services.AddTransient<IJwtGenerator, JwtGenerator>();

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// SOAP
builder.Services.AddSoapCore();
builder.Services.AddSingleton<IHRSoapService, HRSoapService>();

var app = builder.Build();

// Middleware
app.UseMiddleware<HeaderMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSoapEndpoint<IHRSoapService>("/soap", new SoapEncoderOptions());

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
