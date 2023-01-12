using DAL.Repositories;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
var primaryKey = builder.Configuration["CosmosDbPrimaryKey"];
var endpoint = builder.Configuration["CosmosDbEndpoint"];

builder.Services.AddScoped<IndividualRepository>();
builder.Services.AddScoped(sp => new CosmosClient(endpoint, primaryKey));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
