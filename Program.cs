using System.Text.Json.Serialization;
using  E_Commerce_Api.Models;
using  E_Commerce_Api.Data;
using  E_Commerce_Api.Seed;
using  E_Commerce_Api;
using Microsoft.EntityFrameworkCore;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
 builder.Services.AddTransient<Seed>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICartItemRepository,CartItemRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IInvetoryRepository,InvetoryRepository>();
builder.Services.AddScoped<IPaymentDetailsRepository,PaymentDetailsRepository>();
builder.Services.AddScoped<IOrderItemRepository,OrderItemRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IShoppingSessionRepository,ShoppingSessionRepository>();
builder.Services.AddScoped<IOrderDetailRepository,OrderDetailRepository>();
builder.Services.AddScoped<IUserAddressRepository,UserAddressRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserPaymentRepository,UserPaymentRepository>();
builder.Services.AddScoped<IProductCategoryRepository,ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IRoleRepository,RoleRepository>();
builder.Services.AddScoped<IPermissionRepository,PermissionRepository>();
builder.Services.AddScoped<IPasswordResetTokenRepository,PasswordResetTokenRepository>();


builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers().AddJsonOptions(X => X.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    seedData(app);

void seedData(IHost app){
    var scopedFactory =app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

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
