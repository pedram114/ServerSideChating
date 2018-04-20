using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ServerSideSocketClasses;
using Business.Services;
using Common.Model;
using DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication
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
            services.AddDbContext<PedramDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                    optionsBuilder => optionsBuilder.MigrationsAssembly("AspNetIdentityFromScratch")));            
           
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<PedramDbContext>()
                .AddDefaultTokenProviders();
            
            
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;  
                options.SignIn.RequireConfirmedEmail = false;                 
            });
            
            services.AddTransient<IMessageService, MessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,PedramDbContext dbContext,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                dbContext.Database.Migrate(); //this will generate the db if it does not exist

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

           
            var listener=new TCPListener(9990,serviceProvider.GetService<PedramDbContext>());
            listener.AcceptSocket();
           
            var nths = new Thread(CheckMessages);
            nths.Start(serviceProvider);

         

        }

        private static void CheckMessages(object serviceProvider)
        {
            var services = (IServiceProvider)serviceProvider;
            var context = services.GetService<PedramDbContext>();
            var obj = new SendingMessagesHandle(context);
            obj.CheckMessageQueue();

        }
    }
}