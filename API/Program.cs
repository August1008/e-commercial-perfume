using API.Middleware;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartService, CartSerivce>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(config =>
{
    var Endpoint = builder.Configuration.GetValue<string>("RedisSetting:Endpoint") ?? throw new Exception("Cannot connect redis server!");

    ConfigurationOptions configuration = new ConfigurationOptions
    {
        EndPoints = { Endpoint },
        Password = builder.Configuration.GetValue<string>("RedisSetting:Password"),
        Ssl = true
    };
    return ConnectionMultiplexer.Connect(configuration);
});


builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
})
    .AddEntityFrameworkStores<StoreContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200", "https://proud-beach-00b131300.4.azurestaticapps.net"));
app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();
using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    SeedData(context);
}

app.Run();


void SeedData(StoreContext context)
{
    try
    {
        if (!context.Products.Any())
        {
            var data = File.ReadAllText("../Infrastructure/Data/Seeding/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(data);
            if (products == null) return;
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}
