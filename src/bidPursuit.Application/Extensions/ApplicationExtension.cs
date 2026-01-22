using bidPursuit.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace bidPursuit.Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection Services, IConfiguration Configuration)
    {
        var Assembly = typeof(ApplicationExtension).Assembly;
        Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly));
        Services.AddAutoMapper(cfg => cfg.AddMaps(Assembly));
        Services.AddValidatorsFromAssembly(Assembly).AddFluentValidationAutoValidation();
        Services.AddScoped<IAuctionLifecycleService, AuctionLifecycleService>();
        var jwtKey = Configuration["TokenSettings:Key"];
        if (string.IsNullOrEmpty(jwtKey))
            throw new Exception("TokenSettings: Key not found in configuration");

        Services.AddAuthentication("Bearer")
          .AddJwtBearer("Bearer", options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = Configuration["TokenSettings:Issuer"],
                  ValidAudience = Configuration["TokenSettings:Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
              };
          });

        Services.AddAuthorization();
        return Services;
    }
}
