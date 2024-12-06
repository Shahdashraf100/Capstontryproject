using Capstontryproject;
using Capstontryproject.Models;
using Capstontryproject.Servses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ????? IPasswordHasher ?? DI container
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddHttpContextAccessor();

// ????? ????? ???? ??? DbContext ? Services
var s = builder.Configuration.GetConnectionString("connect");
builder.Services.AddDbContext<dbcontext>(x => x.UseSqlServer(s));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<Order>();

// ????? Controllers ? Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ????? ??? HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
