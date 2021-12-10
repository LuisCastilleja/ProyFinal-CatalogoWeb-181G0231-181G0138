using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoTenisWeb.Models;
using Microsoft.EntityFrameworkCore;
using CatalogoTenisWeb.Services;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace CatalogoTenisWeb
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatalogoTenisContext>(options =>
                options.UseMySql("server=localhost;user=root;password=root;database=CatalogoTenis", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql")));

            services.AddScoped<MarcasService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Environment = env;
            app.UseDeveloperExceptionPage();
          
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });


            app.UseStaticFiles();
           
        }
    }
}
