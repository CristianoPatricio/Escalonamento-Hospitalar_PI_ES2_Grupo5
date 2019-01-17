using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EscalonamentoHospitalar.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EscalonamentoHospitalar.Models;
using SeedData = EscalonamentoHospitalar.Data.SeedData;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EscalonamentoHospitalar
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            
            

            services.AddDbContext<HospitalUsersDataBase>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            /*   services.AddDefaultIdentity<IdentityUser>()
                   .AddEntityFrameworkStores<ApplicationDbContext>();*/

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<HospitalUsersDataBase>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            
            //services.AddAuthorization(options => {
            //    options.AddPolicy("AcessoRestritoAdministrador",
            //        policy => policy.RequireRole("Administrador"));
            //});

            //// Política para acesso restrito ao diretor de serviço
            //services.AddAuthorization(options => {
            //    options.AddPolicy("AcessoRestritoDiretorServico",
            //        policy => policy.RequireRole("DiretorServico"));
            //});

            //// Política para acesso restrito ao médico
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AcessoRestritoMedico",
            //        policy => policy.RequireRole("Medico"));
            //});

            //// Política para acesso restrito ao enfermeiro
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AcessoRestritoEnfermeiro",
            //        policy => policy.RequireRole("Enfermeiro"));
            //});
            

            //services.Configure<IdentityOptions>(
            //    options =>
            //    {
            //        // Definições da password
            //        options.Password.RequireUppercase = true;
            //        options.Password.RequireDigit = true;
            //        options.Password.RequiredLength = 6;
            //        options.Password.RequireUppercase = true;
            //        options.Password.RequiredUniqueChars = 3;

            //        // Definições de lockout
            //        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            //        options.Lockout.MaxFailedAccessAttempts = 3;
            //        options.Lockout.AllowedForNewUsers = true;

            //    }
                
            //);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession();
            var connection = @"Server=(localdb)\mssqllocaldb;Database=HospitalDataBase;Trusted_Connection=True";

            /*  services.AddDbContext<HospitalDbContext>(options =>
                      options.UseSqlServer(Configuration.GetConnectionString("HospitalDbContext")));*/
            services.AddDbContext<HospitalDbContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            HospitalDbContext db, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager
            )
        {
        //  UsersSeedData.EnsurePopulatedAsync(userManager, roleManager).Wait();

            if (env.IsDevelopment())
            {
              //  UsersSeedData.EnsurePopulatedAsync(userManager, roleManager).Wait();
               // SeedData(db);

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

           // SeedData.Populate(app.ApplicationServices);
        }
    }
}
