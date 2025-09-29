using FlashCards.Api.Bl.Installers;
using FlashCards.Api.Bl.Mappers;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Installers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(
    cfg => cfg.LicenseKey = builder.Configuration.GetSection("Licenses")["Automapper"],
    typeof(CardMapperProfile));

ConfigureCors(builder.Services);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAuthentication(builder.Services, builder.Configuration);

var app = builder.Build();

UseDevelopmentSettings(app);
UseSecurityFeatures(app);
UserAuthorization(app);

app.MapControllers();

app.Run();

void ConfigureCors(IServiceCollection serviceCollection)
{
    serviceCollection.AddCors(options =>
    {
        options.AddDefaultPolicy(o =>
            o.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    // SQL SERVER
    var connectionStringDal = configuration.GetConnectionString("DefaultConnection")
                              ?? throw new ArgumentException("The connection string for app is missing");
    ApiDalInstaller.Install(serviceCollection, connectionStringDal);
    
    ApiBlInstaller.Install(serviceCollection);
}

void ConfigureAuthentication(IServiceCollection serviceCollection, IConfiguration configuration)
{
    serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {
            opt.Authority = configuration.GetSection("Urls")["IdentityProvider"];
            opt.TokenValidationParameters.ValidateAudience = false;
        });

    serviceCollection.AddAuthorization(opt => opt.AddPolicy("AdminRole", policy => policy.RequireRole("admin")));
    serviceCollection.AddAuthorization(opt => opt.AddPolicy("UserRole", policy => policy.RequireRole("user")));
    
    serviceCollection.AddHttpContextAccessor();
}

void UseDevelopmentSettings(WebApplication application)
{
    if (application.Environment.IsDevelopment())
    {
        application.MapOpenApi();
        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }
}
void UseSecurityFeatures(IApplicationBuilder application)
{
    application.UseCors();
    application.UseHttpsRedirection();
}
void UserAuthorization(IApplicationBuilder application)
{
    application.UseAuthentication();
    application.UseAuthorization();
}