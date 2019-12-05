using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SnakeGame.Api.Configurations;
using SnakeGame.Api.Helpers;
using SnakeGame.Api.Hubs;
using SnakeGame.Infrastructure.Models;
using SnakeGame.Services;
using SnakeGame.Services.Entities;

namespace SnakeGame.Api
{
    public class Startup
    {
        private const string CorsPolicyName = "cors_policy";
        private readonly IConfiguration _configuration;
        private readonly ConfigurationFilesEntities _appSettings;
        private readonly OpenApiInfo _apiInfo;
        private readonly string _apiName;

        public Startup()
        {
            _configuration = BuildConfiguration();
            _appSettings = _configuration.Get<ConfigurationFilesEntities>();
            _apiInfo = new OpenApiInfo
                {Title = "Snake Game Admin Panel", Description = "Use this for configuring your application", Version = "v1"};
            _apiName = "snakeGameConfiguration";
        }

        public IConfiguration BuildConfiguration() => new ConfigurationBuilder()
            .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}Configurations\\Files\\")
            .AddJsonFile($"logging.json")
            .AddJsonFile($"hubOptions.json")
            .AddJsonFile($"appsettings.json")
            .AddJsonFile($"gameConfigurations.json")
            .AddJsonFile($"availableUsersConfiguration.json")
            .AddJsonFile($"passwordEncryptConfiguration.json")
            .Build();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(p =>
            {
                p.KeepAliveInterval=_appSettings.HubOptions.KeepAliveInterval;
                p.ClientTimeoutInterval=_appSettings.HubOptions.ClientTimeoutInterval;
            });
            services.AddSwagger(_configuration,_apiName,_apiInfo);
            services.AddDependencies(_configuration);
            services.ConfigureCors(CorsPolicyName);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{_apiName}/swagger.json",
                    _apiInfo.Description);
            });

            app.UseRouting();
            app.UseCors(CorsPolicyName);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GameHub>("/game");
            });
        }
    }
}
