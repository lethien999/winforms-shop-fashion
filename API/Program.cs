using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinFormsFashionShop.Business.Composition;
using WinFormsFashionShop.Data;
using System.Text.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fashion Shop API",
        Version = "v1",
        Description = "API cho hệ thống quản lý shop thời trang - Hỗ trợ thanh toán PayOS"
    });
    
    // Include XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// CORS configuration - cho phép WinForms gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWinForms", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register Business Layer services
var businessServices = ServicesComposition.Create();
builder.Services.AddSingleton(businessServices.OrderService);
builder.Services.AddSingleton(businessServices.AuthService);

// Register Repositories for Payment Service
var orderRepo = new WinFormsFashionShop.Data.Repositories.OrderRepository();
builder.Services.AddSingleton<WinFormsFashionShop.Data.Repositories.IOrderRepository>(orderRepo);

// Register Payment Service
builder.Services.AddScoped<API.Services.IPaymentService, API.Services.PaymentService>();

// Configure JSON serialization
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowWinForms");
app.UseAuthorization();
app.MapControllers();

app.Run();

