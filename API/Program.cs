using DAL.Repositories;
using BL.Operations;
using Microsoft.Azure.Cosmos;
using Common.ApiResponseModels;
using API;

var builder = WebApplication.CreateBuilder(args);
var primaryKey = builder.Configuration["CosmosDbPrimaryKey"];
var endpoint = builder.Configuration["CosmosDbEndpoint"];

builder.Services.AddAuthentication()
    .AddCookie(Constants.PatientCookie, options =>
    {
        options.Events.OnRedirectToAccessDenied = options.Events.OnRedirectToLogin = op =>
        {
            op.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };

        options.SlidingExpiration = true;
        options.LoginPath = "/api/patient/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    })
    .AddCookie(Constants.DoctorCookie, options =>
    {
        options.Events.OnRedirectToAccessDenied = options.Events.OnRedirectToLogin = op =>
        {
            op.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };

        options.SlidingExpiration = true;
        options.LoginPath = "/api/doctor/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    })
    .AddCookie(Constants.HospitalCookie, options =>
    {
        options.Events.OnRedirectToAccessDenied = options.Events.OnRedirectToLogin = op =>
        {
            op.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };

        options.SlidingExpiration = true;
        options.LoginPath = "/api/hospial/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Constants.PatientPolicy, configurePolicy =>
    {
        configurePolicy.AddAuthenticationSchemes(Constants.PatientCookie);
        configurePolicy.RequireRole(Roles.Patient);
    });
    options.AddPolicy(Constants.DoctorPolicy, configurePolicy =>
    {
        configurePolicy.AddAuthenticationSchemes(Constants.DoctorCookie);
        configurePolicy.RequireRole(Roles.Doctor);
    });
    options.AddPolicy(Constants.HospitalPolicy, configurePolicy =>
    {
        configurePolicy.AddAuthenticationSchemes(Constants.HospitalCookie);
        configurePolicy.RequireRole(Roles.Hospital);
    });
});

builder.Services.AddScoped<PatientOperations>();
builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped<DoctorOperations>();
builder.Services.AddScoped<DoctorRepository>();
builder.Services.AddScoped<HospitalOperations>();
builder.Services.AddScoped<HospitalRepository>();
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
