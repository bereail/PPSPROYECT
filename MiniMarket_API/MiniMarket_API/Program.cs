using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.OpenApi.Models;
using MiniMarket_API.Application.Services.Interfaces;
using MiniMarket_API.Model;
using MiniMarket_API.Data.Interfaces;
using MiniMarket_API.Application.Profiles;
using MiniMarket_API.Data.Repositories;
using MiniMarket_API.Application.Services.Implementations;
using MiniMarket_API.Application.Filters;
using Serilog;
using MiniMarket_API.Middlewares;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MiniMarket_API.Application.Events.ProductEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region Serilog Config
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Application/Logs/MiniMarket_API_Log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Error()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion


builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ExceptionFilter());
});

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    //Setting up Swagger support for Tokens.
    setupAction.AddSecurityDefinition("MiniMarketApiBearerAuth", new OpenApiSecurityScheme() 
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Paste Token here after login."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "MiniMarketApiBearerAuth" } 
                    }, new List<string>() }
        });
});

builder.Services.AddDbContext<MarketDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MarketConnectionString")));     //DbContext injection + Connection string established for SqlServer

#region Mappings
builder.Services.AddAutoMapper(typeof(MappingProfiles));
#endregion

#region EventManagers
builder.Services.AddScoped<IProductEventManager, ProductEventManager>();
#endregion

#region Services
builder.Services.AddScoped<ICustomAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IDeliveryAddressService, DeliveryAddressService>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IMpOrderService, MpOrderService>();
builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
builder.Services.AddScoped<IPriceStockService, PriceStockService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleOrderService, SaleOrderService>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region Repositories
builder.Services.AddScoped<ICompanyCodeRepository, CompanyCodeRepository>();
builder.Services.AddScoped<IDeliveryAddressRepository, DeliveryAddressRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<ISaleOrderRepository, SaleOrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    //AllowAll should only be used for testing, nothing else.

    options.AddPolicy("UnsafeAllowAll", builder =>
                     builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());

    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost",
                                "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
#endregion

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    })
    .AddJwtBearer("PasswordRecovery", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Recovery:Issuer"],
            ValidAudience = builder.Configuration["Recovery:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Recovery:SecretForKey"]))
        };
    });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
