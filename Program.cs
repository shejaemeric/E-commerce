using System.Text.Json.Serialization;
using  E_Commerce_Api.Models;
using  E_Commerce_Api.Data;
using  E_Commerce_Api.Seed;
using E_Commerce_Api.helpers;
using  E_Commerce_Api;
using Microsoft.EntityFrameworkCore;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
 builder.Services.AddTransient<Seed>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


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



var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

    builder.Services.AddSingleton<IConfiguration>(configuration);



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"],
        ValidIssuer = configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("JWT:SecretKey")))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("Manager", policy =>
    {
        policy.RequireRole("Manager");
    });

    options.AddPolicy("User", policy =>
    {
        policy.RequireRole("User");
    });

    options.AddPolicy("Admin/Manager", policy =>
    {
        policy.RequireRole("Admin","manager");
    });

    options.AddPolicy("Admin/Manager/Owner", policy =>
    {
        policy.RequireRole("Admin","manager");
        policy.Requirements.Add(new UserAuthorizationRequirement());
    });
});


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
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
