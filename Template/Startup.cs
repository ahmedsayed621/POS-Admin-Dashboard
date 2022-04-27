using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tamplate.DAL.Database;
using Template.BL.Interface;
using Template.BL.Mapper;
using Template.BL.Repository;
using Template.Controllers;
using Template.Language;

namespace Template
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

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<AccountController>>();
            services.AddSingleton(typeof(ILogger), logger);



            services.AddControllersWithViews()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization(options =>
                    {
                        options.DataAnnotationLocalizerProvider = (type, factory) =>
                            factory.Create(typeof(SharedResource));
                    })
                    .AddNewtonsoftJson(opt => {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddDbContextPool<TemplateContext>(opts =>
           opts.UseSqlServer(Configuration.GetConnectionString("TemplateConnection")));

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));


            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<TemplateContext>()
              .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

            services.AddAuthentication().AddIdentityCookies(o =>
            {
                o.TwoFactorRememberMeCookie.Configure(a => a.Cookie.Expiration = new TimeSpan(10,00,00,00));
            });

            // Way 1  ==> Transient
            //services.AddTransient<IDepartmentRep, DepartmentRep>();


            // Way 2  ==> Scoped
            services.AddScoped<IDepartmentRep, DepartmentRep>();
            services.AddScoped<IEmployeeRep, EmployeeRep>();
            services.AddScoped<ICountryRep, CountryRep>();
            services.AddScoped<ICityRep, CityRep>();
            services.AddScoped<IDistrictRep, DistrictRep>();


            // Way 3  ==> SingleTone
            //services.AddSingleton<IDepartmentRep, DepartmentRep>();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.useco UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    ExpireTimeSpan = TimeSpan.FromHours(1),
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });



        }
    }
}
