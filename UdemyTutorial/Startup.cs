using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using UdemyTutorial.DAL;
using UdemyTutorial.Services.IRepository;
using UdemyTutorial.Services.Repository;

namespace UdemyTutorial
{
    public class Startup
    {
        public IConfiguration Config { get; set; }

        public Startup(IConfiguration _config)
        {
            Config = _config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options=> {
                options.UseSqlServer(Config.GetConnectionString("DefaultConnection"));
            });
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IAccountInitialize, AccountInitialize>();

            services.AddMvc();
            services.AddPaging(options => {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options=>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization(options=> 
            {
                    options.AddPolicy("OnlyAdmin",policy=> policy.RequireRole("Admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAccountInitialize accountInitialize)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();
            accountInitialize.SeedData();

            DbInitializer.Seed(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default",
                    pattern: "{controller=Home}/{action=Index}/{int?}");
            });
        }
    }
}
