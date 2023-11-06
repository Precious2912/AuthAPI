using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AuthService.API.ServiceExtension
{
    public static class RegisterServicesExtension
    {
       public static void ConfigureJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])
                            )
                    };
                });
        }
        public static void ConfigureBadRequest(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(a =>
            {
                a.InvalidModelStateResponseFactory = context =>
                {

                    return new BadRequestObjectResult(new CustomBadRequest(context))
                    {
                        ContentTypes = { "application /json", "application/xml" },
                    };
                };
            });
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                c.EnableAnnotations();

                // Define the security scheme (e.g., Bearer token)
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    Description = "JWT Authorization header using the Bearer scheme",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };
                c.AddSecurityDefinition("Bearer", securityScheme);

                // Define the security requirement to access endpoints with the [Authorize] attribute
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                };
                c.AddSecurityRequirement(securityRequirement);
            });
        }

    }
}
