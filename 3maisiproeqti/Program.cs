using Microsoft.AspNetCore.Authentication.JwtBearer;
using _3maisiproeqti.Database.Context;
using _3maisiproeqti.Interfaces;
using _3maisiproeqti.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineBookStore"));
});
builder.Services.AddTransient<IBookRepository, BookService>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {

        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET core web api"
    });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWTBearer",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n  ",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        } ,
        new string[] {}
    }
    });
});

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //aq shecdomas migdebs

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:44371",
        ValidAudience = "https://localhost:44371",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DFELEERLAUIF3982qef34t6457hyueruw")),
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseAuthentication();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authorization API v1");
});

app.Run();



