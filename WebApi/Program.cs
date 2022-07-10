using DbModel;
using Microsoft.EntityFrameworkCore;
using Services;

var myLocalHostCorsPolicy = "_myLocalHostCorsPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(myLocalHostCorsPolicy,
        builder =>
        {
            builder.WithOrigins("http://localhost:8080").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("AppDb");

builder.Services.AddDbContext<AirportDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddSingleton<Services.IServiceProviderSingleton, Services.ServiceProviderSingleton>();
builder.Services.AddScoped<Services.IServiceProvider, Services.ServiceProvider>();
builder.Services.BuildServiceProvider();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseCors(myLocalHostCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
