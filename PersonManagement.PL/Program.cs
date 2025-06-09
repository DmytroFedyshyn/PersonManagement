using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonManagement.BLL;
using PersonManagement.BLL.Constants;
using PersonManagement.BLL.Validators;
using PersonManagement.DAL;
using PersonManagement.PL.Extensions;
using PersonManagement.PL.Infrastructure;
using PersonManagement.PL.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Person Management API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<PersonCreateValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.RegisterBusinessLogicServices();
builder.Services.RegisterDataAccessServices();

builder.AddProjectCors();

var app = builder.Build();

app.UseCors(ApplicationConstants.CorsPolicySectionName);

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

    if (env.IsDevelopment())
    {
        await DbSeeder.SeedAsync(db);
    }
}

await app.RunAsync();