using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.API.Helpers;
using Chess.API.Hubs;
using Chess.API.Persistence.Implementation;
using Chess.API.Persistence.Interfaces;
using Chess.API.Services;
using Chess.API.Services.Interfaces;
using Chess.Logic;
using Chess.Logic.Interfaces;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chess.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000");
            }));
            
            services.AddSignalR();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IGameManager, GameManager>();
            services.AddSingleton<ITableService, TableService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IExceptionHandler, ExceptionHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseSignalR(routes =>
            {
                routes.MapHub<TableHub>("/Hubs/Tables");
                routes.MapHub<GameHub>("/Hubs/Games");
            });
            app.UseMvc();
        }
    }
}
