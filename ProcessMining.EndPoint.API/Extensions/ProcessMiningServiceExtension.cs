using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProcessMining.Core.ApplicationService.Queries;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.ApplicationService.Services.Authenticators;
using ProcessMining.Core.ApplicationService.TokenGenerators;
using ProcessMining.Core.ApplicationService.TokenValidators;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Core.Domain.Models;
using ProcessMining.Infra.EntityFramework.DbContextes;
using ProcessMining.Infra.Tools.Reflections;
using System.Text;

namespace ProcessMining.EndPoint.API.Extensions
{
    public static class ProcessMiningServiceExtension
    {
        private static IServiceCollection AddProcessMiningDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProcessMiningDbContext>(options => options.UseSqlite(connectionString), optionsLifetime: ServiceLifetime.Singleton);
            services.AddDbContextFactory<ProcessMiningDbContext, ProcessMiningDbContextFactory>(options => options.UseSqlite(connectionString));
            return services;
        }

        private static IServiceCollection AddAccessTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddSingleton<TokenGenerator>();
            return services;
        }

        private static IServiceCollection AddProcessMiningServices(this IServiceCollection services)
        {
            // Register base services
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));

            // Get all services corresponding to Registration Required Attribute
            var repositoryTypes = Assemblies.GetServices("ProcessMining.Core.ApplicationService", typeof(RegistrationRequiredAttribute));


            // Register each service
            foreach (var repositoryType in repositoryTypes)
            {
                var repositoryInterface = repositoryType.GetInterfaces().Where(i => !i.IsGenericType).FirstOrDefault();
                services.AddScoped(repositoryInterface, repositoryType);
            }

            // Return extension method value
            return services;
        }

        public static WebApplicationBuilder AddProcessMining(this WebApplicationBuilder builder, out WebApplication app)
        {
            // Authentication configuration
            var authenticationConfiguration = new AuthenticationConfiguration();
            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("sqlite");
            configuration.Bind("Authentication", authenticationConfiguration);
            builder.Services.AddSingleton(authenticationConfiguration);

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    name: JwtBearerDefaults.AuthenticationScheme,
                    securityScheme: new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Description = "Enter the Bearer Authorization : `Bearer Generated-JWT-Token`",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                    );
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                    }
                        ,new string[] {}
                    }
                });
            });

            // Add DbContext
            builder.Services.AddProcessMiningDbContext(connectionString);

            // Add Access Token Generator
            builder.Services.AddAccessTokenGenerator();

            // Add Services
            builder.Services.AddProcessMiningServices();

            app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return builder;
        }
    }
}
