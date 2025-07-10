using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Services;
using WebApplication5.Validations;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApplication5Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication5Context") ?? throw new InvalidOperationException("Connection string 'WebApplication5Context' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CryptoValidation>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICryptoService,CryptoService>();
builder.Services.AddSwaggerGen();

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
