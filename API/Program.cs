using DAL.Repositories;
using BL.Operations;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
var primaryKey = builder.Configuration["CosmosDbPrimaryKey"];
var endpoint = builder.Configuration["CosmosDbEndpoint"];
var token = builder.Configuration["Token"];

builder.Services.AddScoped<PatientOperations>();
builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped(sp => new CosmosClient(endpoint, primaryKey));
builder.Services.AddScoped(sp => token);
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
