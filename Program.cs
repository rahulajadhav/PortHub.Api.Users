using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortHub.Api.Users.Data;
using PortHub.Api.Users.Data.Models;
using PortHub.Api.Users.Interface;
using PortHub.Api.Users.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<appDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddHttpClient("TokenGenetator", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:TokenAuthorization"]);

});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<appDbContext>().AddDefaultTokenProviders();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Authentication & Authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
