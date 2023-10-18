using dotenv.net;
using MongoDB.Driver;
using Trivia.Application;
using Trivia.Application.IServices;
using Trivia.DataAccess.Entities;

var builder = WebApplication.CreateBuilder(args);

var mongoConnectionString = builder.Configuration["Trivia:ConnectionString"];
var mongoDatabaseName = builder.Configuration["Trivia:DatabaseName"];
var mongoCollectionName = builder.Configuration["Trivia:CollectionName"];

var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);
var mongoCollection = mongoDatabase.GetCollection<Questions>(mongoCollectionName);
builder.Services.AddSingleton(mongoCollection);
builder.Services.AddScoped<ITriviaService, TriviaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
