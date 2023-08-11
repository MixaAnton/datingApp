using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Text;
using webapi.Data;
using webapi.Extensions;
using webapi.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddCors(
options =>
{
 options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
               {
                   policy.AllowAnyHeader()
                   .AllowAnyMethod()
                   .WithOrigins("https://localhost:4200");
               });
}
);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
