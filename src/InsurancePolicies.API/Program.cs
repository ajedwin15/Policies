using src.InsurancePolicies.Infrastructure.Data;
using src.InsurancePolicies.Infrastructure.Data.Context;
using src.InsurancePolicies.Infrastructure.Data.Interface;
using src.InsurancePolicies.Infrastructure.Repositories.Mongo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoSettings>(options =>
{
    options.ConnectionString = builder.Configuration.GetSection("MONGO_STRING_CONNECTION").Value;
    options.DatabaseName = builder.Configuration.GetSection("MONGO_DB_NAME_USER").Value;
});


// singleton for setting
builder.Services.AddSingleton<MongoSettings>();

builder.Services.AddTransient<IInsurancePoliciesContex, InsurancePoliciesContex>();

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddHttpClient();

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

