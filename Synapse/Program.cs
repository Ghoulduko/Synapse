using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Synapse.Application.Interfaces;
using Synapse.Application.Interfaces.RepositoryInterfaces;
using Synapse.Application.Interfaces.ServiceInterfaces;
using Synapse.Application.Services;
using Synapse.Application.Validators;
using Synapse.Infrastructure;
using Synapse.Infrastructure.Authentication;
using Synapse.Infrastructure.Database;
using Synapse.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SynapseDbContext>(i => i.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// General Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFriendRequestService, FriendRequestService>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();

// Auth
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Fluent Validations
builder.Services.AddValidatorsFromAssembly(typeof(RegisterValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(LoginValidator).Assembly);

var jwtKey = builder.Configuration["JwtConfig:Key"] ?? throw new InvalidOperationException("Jwt key is not configured.");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://ltdluka.ge/",
            ValidAudience = "https://ltdluka.ge/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cyber Commerce API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

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