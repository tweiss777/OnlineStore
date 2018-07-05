using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStoreMVC.Models;

namespace OnlineStoreMVC
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
            //Cookie Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options => {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Home/Index";
            });
            services.AddMvc();

            //The following two lines are used for session based browsing...
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name =".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(3); //sessions clear after 3 minutes. Note this has nothing to do with cookie expirations
            });


            
            //register PersonContext as a service
            services.Add(new ServiceDescriptor(typeof(PersonContext),new PersonContext(Configuration.GetConnectionString("connection1"))));

            //register CarContext as a service
            services.Add(new ServiceDescriptor(typeof(CarContext), new CarContext(Configuration.GetConnectionString("connection1"))));


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
            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            //Adds MVC to the request pipeline
            app.UseMvc(routes =>
            {
            routes.MapRoute(//route for admin
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            routes.MapRoute(
                name: "admin",
                template: "{controller=Admin}/{action=CustomerIndex}/{id?}" );
            
            });


        }
    }
}
