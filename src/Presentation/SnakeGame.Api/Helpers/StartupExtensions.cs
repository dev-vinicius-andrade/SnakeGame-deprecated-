using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SnakeGame.Api.Configurations;
using SnakeGame.Services;
using SnakeGame.Services.Entities;

namespace SnakeGame.Api.Helpers
{
    internal static class StartupExtensions
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationFilesEntities = configuration.Get<ConfigurationFilesEntities>();
            services.AddSingleton(p=>configurationFilesEntities.PasswordEncryptConfiguration);
            services.AddSingleton(p=>configurationFilesEntities.AvailableUsersConfiguration);
            services.AddSingleton(p=>configurationFilesEntities.GameConfigurations);
            services.AddSingleton<ConfigurationFilesEntities.ConnectedUsers>();
            services.AddScoped<GameService>();
            services.AddScoped<PlayerService>();
            services.AddScoped<SnakeService>();
            services.AddScoped<FoodService>();
            services.AddScoped<RoomService>();
            services.AddSingleton<GameData>();
        }
        public static void AddSwagger(this IServiceCollection services , IConfiguration configuration, string apiName, OpenApiInfo apiInfo)
        {
            services.AddControllers();
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(apiName,  apiInfo);
                });
        }
        public static  void ConfigureCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(options
                =>
            {
                options.AddPolicy(policyName,
                    builder =>
                        builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader()
                            .AllowCredentials());
            });
        }
    }
}
