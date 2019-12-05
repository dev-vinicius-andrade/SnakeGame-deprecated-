using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SnakeGame.Api.Configurations;
using SnakeGame.Api.Helpers;
using SnakeGame.Api.Hubs;

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
            .SetBasePath(basePath:$"{AppDomain.CurrentDomain.BaseDirectory}Configurations\\Files\\")
            .AddJsonFile(path:$"logging.json", optional:false,reloadOnChange:true)
            .AddJsonFile(path:$"hubOptions.json",optional:false,reloadOnChange:true)
            .AddJsonFile(path:$"appsettings.json",optional:false,reloadOnChange:true)
            .AddJsonFile(path:$"gameConfigurations.json",optional:false,reloadOnChange:true)
            .AddJsonFile(path:$"availableUsersConfiguration.json")
            .AddJsonFile(path:$"passwordEncryptConfiguration.json")
            .Build();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(p =>
            {
                p.KeepAliveInterval=_appSettings.HubOptions.KeepAliveInterval;
                p.ClientTimeoutInterval=_appSettings.HubOptions.ClientTimeoutInterval;
            });
            services.AddSwagger(_configuration, _apiName, _apiInfo);
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
                endpoints.MapHub<Game>("/game");
                endpoints.MapHub<Food>("/food");
                endpoints.MapHub<Snake>("/snake");
                endpoints.MapHub<Player>("/player");
            });
        }
    }
}
