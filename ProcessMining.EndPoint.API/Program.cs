using ProcessMining.Core.Domain.Models;
using ProcessMining.EndPoint.API.Extensions;

namespace ProcessMining.EndPoint.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Authentication configuration
            var authenticationConfiguration = new AuthenticationConfiguration();
            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            configuration.Bind("Authentication", authenticationConfiguration);
            builder.Services.AddSingleton(authenticationConfiguration);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext
            builder.Services.AddProcessMiningDbContext();

            // Add Access Token Generator
            builder.Services.AddAccessTokenGenerator();

            // Add Services
            builder.Services.AddProcessMiningServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}