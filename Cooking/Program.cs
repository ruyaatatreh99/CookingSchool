using Cooking.middleware;
using Cooking.Model;
using Cooking.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    opton => {
        opton.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
         Description="Standard Authorization header using the secret",
         In=ParameterLocation.Header,
         Name="Authorization",
         Type=SecuritySchemeType.ApiKey
        });
        opton.OperationFilter<SecurityRequirementsOperationFilter>();
    }
        );
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opton => {
    opton.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddDbContext<DBContext>();
builder.Services.AddScoped<IAdmin, AdminRepos>();
builder.Services.AddScoped<IStudent, StudentRepos>();
builder.Services.AddScoped<ITeacher, TeacherRepos>();
//builder.Services.AddTransient<UserMiddleware>();
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<Middleware>();
//app.UseMiddleware<UserMiddleware>();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.MapControllers();
});
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();



app.Run();