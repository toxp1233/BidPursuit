using Microsoft.OpenApi.Models;

namespace bidPursuit.API.Extensions;

public static class ApiExtensions
{
    public static void AddExtras(this WebApplicationBuilder Builder)
    {
        Builder.Services.AddAuthentication();
        Builder.Services.AddControllers();
        Builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                    },
                    []
                }

            });
        });

        Builder.Services.AddEndpointsApiExplorer();

    }
}
