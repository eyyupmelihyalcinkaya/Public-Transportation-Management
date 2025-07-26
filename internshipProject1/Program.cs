using internshipproject1.Application;
using internshipProject1.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;
using internshipProject1.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetValue<string>("Redis:ConnectionString")));

//Swagger Authentication
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter te Bearer Authorization : `Bearer Generated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme {
            Reference= new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id=JwtBearerDefaults.AuthenticationScheme
            }
        },  new string[]{}
      }
    });
});

// Infrastructure ve Application servislerini register et
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

/* 
 *  $infraPath = "C:\Users\Melih\Desktop\Asis\gitLAB\project1\internshipproject1\internshipProject1.Infrastructure\internshipProject1.Infrastructure.csproj"
    $webApiPath = "C:\Users\Melih\Desktop\Asis\gitLAB\project1\internshipproject1\internshipProject1\internshipProject1.WebAPI.csproj"
    dotnet ef migrations add InitialCreate --project $infraPath --startup-project $webApiPath 

    dotnet ef database update --project $infraPath --startup-project $webApiPath

 */
//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
        policy.WithOrigins("https://localhost:7156")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddTransient<ErrorMiddleware>();
builder.Services.AddTransient<LogMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorMiddleware>();
app.UseMiddleware<LogMiddleware>();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();