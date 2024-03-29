using System.Text.Json;
using DbModel;
using DocumentDbModel.AirportDocument;
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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = configuration.GetConnectionString("AppDb");
string documentDbConnectionString = configuration.GetSection("DocumentDb")["ConnectionString"];

string documentDbDatabase = configuration.GetSection("DocumentDb")["Database"];
string collectionName = configuration.GetSection("DocumentDb")["CollectionName"];

builder.Services.AddDbContext<AirportDbContext>(options =>
    options.UseSqlServer(connectionString)
);

//
// Mongo DB settings
//
DocumentDbSettings mongoDbSettings = new DocumentDbSettings
{
    MongoDbConnectionString = documentDbConnectionString,
    MongoDbDatabase = documentDbDatabase,
    CollectionName = collectionName
};
builder.Services.Configure<DocumentDbSettings>(options =>
{
    options.MongoDbConnectionString = mongoDbSettings.MongoDbConnectionString;
    options.MongoDbDatabase = mongoDbSettings.MongoDbDatabase;
    options.CollectionName = mongoDbSettings.CollectionName;
});


builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
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
