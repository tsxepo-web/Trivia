using dotenv.net;
using MongoDB.Driver;
using Trivia.Application;
using Trivia.Application.IServices;
using Trivia.DataAccess.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DotEnv.Load();
var envKeys = DotEnv.Read();
var mongoConnectionString = "mongodb://trivia-app:9Oqvib8Namzi5UO7ec4vunXZZynfyoD5HRwiogvZKrm1ITuZAWiDA2DbV1VSNiLbj48ALkfvSJylACDbzljhTw%3D%3D@trivia-app.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@trivia-app@";
var mongoDatabaseName = "trivia";
var mongoCollectionName = "questions";
var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseName);
var mongoCollection = mongoDatabase.GetCollection<Questions>(mongoCollectionName);
builder.Services.AddSingleton(mongoCollection);
builder.Services.AddScoped<ITriviaService, TriviaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
