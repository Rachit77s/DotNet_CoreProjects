using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanyPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BethanyPieShop
{
    public class Startup
    {
        //Rachit
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Rachit

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Identity Added
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

            //Dependency Injection
            //AddScoped --> Per request an instance is created and this instance remains active during the entire request. So as soon ass the request goes out of scope, the instance will also be discarded. Like a Singleton per request
            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            //Lambda exp is going to invoke for the user the GetCart method on the ShoppingCart class and pass the IServiceProvider i.e. Service collection that we need to access to in the GetCart method.
            // This is IMPORTANT because, when the user comes to the website , we are going to create a scoped shopping cart using the GetCart method, in other words, the GetCart method is going to be invoked when the user sends a request. That gives us the ability to check if the cartId is already in the session, if not, pass it into this session and return the ShoppingCart itself. 
            //That is --> return new ShoppingCart(context) { ShoppingCartId = cartId };
            //So by this we are sure when the user comes to the website, a shopping cart will be associated with the request. And since it's scoped, it means all the interaction with the same shopping cart, within the same request, we'll use that same shopping cart.
            services.AddScoped<ShoppingCart>( sp => ShoppingCart.GetCart(sp) );
            // To access the session in GetCard method
            services.AddHttpContextAccessor(); 
            services.AddSession();
            //Add MVC to our project
            services.AddControllersWithViews();

            //Identity
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Rachit
            //Order is important

            app.UseHttpsRedirection();
            //MVC searches under wwwroot folder for static files like js css etc
            app.UseStaticFiles();
            //Call session before Routing
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //ASP.NET Identity
                endpoints.MapRazorPages();
            });
        }
    }
}
