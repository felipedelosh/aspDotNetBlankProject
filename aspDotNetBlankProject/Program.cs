using Domain.Entities;
using Domain.Ports;
using Domain.Services;
using Infrastructure.Adapters.BASE;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Infraestructure;
using System;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PersitenceContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddTransient<IDbConnection>(
    (sp) => new SqlConnection(connectionString)
    );

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Application.Example.Commands.CommandCreateExampleHandler).Assembly));

builder.Services.AddScoped<IGenericRepository<Example>, GenericRepository<Example>>();

builder.Services.AddScoped<ExampleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
