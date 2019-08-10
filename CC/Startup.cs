using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CC.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using CC.Models;

namespace CC
{
	public class Startup
	{
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("ccContext"));
            //{
            //    Password = Configuration["Secret:DbPassword"]
            //};
            services.AddDbContext<CCContext>(options =>
                                                options.UseSqlServer(builder.ConnectionString));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<ISecretSettings>(
                // TODO: add secrests.
                new SecretSettings(Configuration["Secret:MapCredantials"], 
                                   Configuration["Secret:DbPassword"],
                                   Configuration["Secret:WeatherKey"]));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
            });
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
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

            app.UseSession();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
