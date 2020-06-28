// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Diary_Sample.Entities;
using Diary_Sample.Repositories;
using Diary_Sample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Diary_Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
#pragma warning disable CA1822 // メンバーを static に設定します
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // メンバーを static に設定します
        {
            services.AddControllersWithViews();
            services.AddSingleton<IMenuService, MenuService>();
            services.AddSingleton<ICreateService, CreateService>();
            services.AddSingleton<IDiaryRepository, DiaryRepository>();
            services.AddSingleton<DiarySampleContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CA1822 // メンバーを static に設定します
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore CA1822 // メンバーを static に設定します
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Menu/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Menu}/{action=Index}");
            });
        }
    }
}