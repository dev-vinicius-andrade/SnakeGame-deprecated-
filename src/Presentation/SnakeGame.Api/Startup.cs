using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SnakeGame.Api.hubs;
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton(p=>_configuration.Get<AppSettings>().GameConfigurations);
            services.AddSingleton<GameService>();
            services.AddSingleton<PlayerService>();
            services.AddSingleton<SnakeService>();
            services.AddSingleton<FoodService>();
            services.AddSingleton<RoomService>();
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(CorsPolicyName);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<PlayerHub>("/player");
                endpoints.MapHub<SnakeHub>("/snake");
                endpoints.MapHub<FoodHub>("/food");
                endpoints.MapHub<GameHub>("/game");
            });
        }
    }
}
