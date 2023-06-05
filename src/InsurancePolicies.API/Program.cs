using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using src.InsurancePolicies.Application.Auth;
using src.InsurancePolicies.Application.Polices;
using src.InsurancePolicies.Domain.Domain.Polices;
using src.InsurancePolicies.Domain.Entities.Security;
using src.InsurancePolicies.Infrastructure.Data;
using src.InsurancePolicies.Infrastructure.Data.Context;
using src.InsurancePolicies.Infrastructure.Data.Interface;
using src.InsurancePolicies.Infrastructure.Repositories.Mongo;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // ADD AUTHORIZER SWAGGER
    options.AddSecurityDefinition(name: "Basic", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Authorizer JWT token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Basic",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Basic",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.Configure<MongoSettings>(options =>
{
    options.ConnectionString = builder.Configuration.GetSection("MONGO_STRING_CONNECTION").Value;
    options.DatabaseName = builder.Configuration.GetSection("MONGO_DB_NAME_USER").Value;
});


// singleton for setting
builder.Services.AddSingleton<MongoSettings>();

builder.Services.AddTransient<IInsurancePoliciesContex, InsurancePoliciesContex>();

builder.Services.AddTransient<IPolicesApplication, PolicesApplication>();
builder.Services.AddTransient<IAuthApplication, AuthApplication>();

builder.Services.AddTransient<IPolicesDomain, PolicesDomain>();

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddHttpClient();

// Add LoginUser to the LoginUser.
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWT_ISSUER"],
            ValidAudience = builder.Configuration["JWT_AUDIENCE"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_KEY"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
    }
    else
    {
        await next();
    }
});

app.MapControllers();

app.Run();

