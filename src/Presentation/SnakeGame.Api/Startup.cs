using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly AppSettings _appSettings;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _appSettings = configuration.Get<AppSettings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(p =>
            {
                p.KeepAliveInterval=_appSettings.HubOptions.KeepAliveInterval;
                p.ClientTimeoutInterval=_appSettings.HubOptions.ClientTimeoutInterval;
            });
            services.AddSingleton(p=>_configuration.Get<AppSettings>().GameConfigurations);
            services.AddScoped<GameService>();
            services.AddScoped<PlayerService>();
            services.AddScoped<SnakeService>();
            services.AddScoped<FoodService>();
            services.AddScoped<RoomService>();
            services.AddSingleton<GameData>();

            services.AddCors(options
                =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder =>
                        builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader()
                            .AllowCredentials());
            });



        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseCors(CorsPolicyName);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GameHub>("/game");
            });
        }
    }
}
