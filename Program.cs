using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YourDebtsCore.Base.Autorization;
using YourDebtsCore.Repositories;
using YourDebtsCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("Conn");
var key = builder.Configuration.GetValue<string>("JwtSettings:key");

// Register and login
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>(option => new AuthorizationService(new LoginRepository(connString),value: key));
builder.Services.AddScoped<ILoginRepository, LoginRepository>(option => new LoginRepository(connString));
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IRegisterRepository, RegisterRepository>(option => new RegisterRepository(connString));

// Debtors
builder.Services.AddScoped<IDebtorsService, DebtorsService>();
builder.Services.AddScoped<IDebtorsRepository, DebtorsRepository>(option => new DebtorsRepository(connString));


// Products
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(option => new ProductRepository(connString));


// Debt
builder.Services.AddScoped<IDebtService, DebtService>();
builder.Services.AddScoped<IDebtRepository, DebtRepository>(option => new DebtRepository(connString));


builder.Services.AddHttpContextAccessor();

var keyBytes = Encoding.ASCII.GetBytes(key);    

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
