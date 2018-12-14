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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            /*   services.AddDefaultIdentity<IdentityUser>()
                   .AddEntityFrameworkStores<ApplicationDbContext>();*/

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            // Política para acesso restrito ao diretor de serviço
            services.AddAuthorization(options => {
                options.AddPolicy("AcessoRestritoDiretorServico",
                    policy => policy.RequireRole("DiretorServico"));
            });

            // Política para acesso restrito ao médico
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AcessoRestritoMedico",
                    policy => policy.RequireRole("Medico"));
            });

            // Política para acesso restrito ao enfermeiro
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AcessoRestritoEnfermeiro",
                    policy => policy.RequireRole("Enfermeiro"));
            });
            

            services.Configure<IdentityOptions>(
                options =>
                {
                    // Definições da password
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredUniqueChars = 3;

                    // Definições de lockout
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                    options.Lockout.MaxFailedAccessAttempts = 3;

                }
                
            );


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<HospitalDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("HospitalDbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, HospitalDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
           SeedData.CreateRolesAndUsersAsync(userManager, roleManager).Wait();

            if (env.IsDevelopment())
            {
                SeedData.CreateRolesAndUsersAsync(userManager, roleManager).Wait();
             //   SeedData.Populate(db);

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
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.Populate(app.ApplicationServices);
        }
    }
}
