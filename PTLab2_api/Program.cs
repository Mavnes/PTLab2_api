using Microsoft.EntityFrameworkCore;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Repositories.Implimentations;
using PTLab2_api.Data.Repositories.Interfaces;
using PTLab2_api.Data.Services.implimentations;
using PTLab2_api.Data.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add allowed origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder => builder.AllowAnyOrigin()
                                                                .AllowAnyHeader()
        );
});

// DbContext
var connection = builder.Configuration.GetConnectionString("SupaBase");
builder.Services.AddDbContext<ShopDbContext>(options => options.UseNpgsql(connection));

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// UnitOfWork
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.MapWhen(
    context => context.Request.Path.StartsWithSegments("/"),
    builder => builder.RunProxy(new ProxyOptions
    {
        Scheme = "https",
        Host = "localhost",
        Port = "7045",
    })
);

app.UseAuthorization();

app.MapControllers();

app.Run();
