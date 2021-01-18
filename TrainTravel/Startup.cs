using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using TrainTravel.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainTravel.DataAcess;
using TrainTravel.ApplicationLogic.Abstractions;
using TrainTravel.ApplicationLogic.Services;
using TrainTravel.ViewModels;

namespace TrainTravel
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<TrainTravelDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ITrainRouteRepository, TrainRouteRepository>();
            services.AddTransient<ITrainScheduleRepository, trainScheduleRepository>();
            services.AddTransient<ITrainTypeRepository, TrainTypeRepository>();
            services.AddTransient<ITicketHolderRepository, TicketHolderRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddScoped<AdminServices>();
            services.AddScoped<ClientServices>();


            services.AddControllersWithViews();


            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Client");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Client"));
            }

            var roleCheck1 = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck1)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            // ApplicationUser user1 = await UserManager.FindByEmailAsync("syedshanumcain@gmail.com");
            //var User = new ApplicationUser();
            // await UserManager.AddToRoleAsync(user1, "Admin");
        }
    }
}
