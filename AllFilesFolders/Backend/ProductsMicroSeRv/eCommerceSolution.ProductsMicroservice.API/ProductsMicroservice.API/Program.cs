using eCommerce.ProductsMicroservice.API;
using eCommerce.ProductsService.BusinessLogicLayer;
using eCommerce.ProductsService.DataAccessLayer;
using eCommerce.ProductsMicroService.API.APIEndpoints;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

//add DAL , BAL services
builder.Services.AddDataAccessLayerServices(builder.Configuration);
builder.Services.AddBusinessLogicLayerServices();   
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy( builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseExceptionalHandlingMiddleware();
app.UseRouting();

//cors
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();


//Auth


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapProductAPIEndpoints();
app.MapControllers();


//app.MapGet("/", () => "Hello World!");

app.Run();
