using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PET.Application.Builders;
using PET.Application.Services;
using PET.API.Services.Authorization;
using PET.Domain.Models;
using PET.Ef.DbContexts;
using PET.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace PET.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services
                .AddDbContext<AnimalDbContext>(
                    options => options
                        .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Version = "v1",
                        Title = "PET API"
                    });
                })
                .AddScoped<IAnimalDtoBuilder, AnimalDtoBuilder>()
                .AddScoped<IDataService<User>, UserDataService>()
                .AddScoped<IDataService<Animal>, AnimalDataService>()
                .AddScoped<IFileStorageService, FileStorage.FileStorage>()
                .AddScoped<IAnimalBuilder, AnimalBuilder>()
                .AddScoped<IFileBuilder, FileBuilder>()
                .AddScoped<IUserDtoBuilder, UserDtoBuilder>()
                .AddScoped<IUserBuilder, UserBuilder>()
                .AddScoped<IAnimalSpecificationBuilder, AnimalSpecificationBuilder>()
                .AddScoped<FileAppService>()
                .AddScoped<AnimalAppService>()
                .AddScoped<UserAppService>()
                .AddTransient<IAuthorizationHandler, MustOwnAnimalHandler>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/account");
                    options.Cookie.HttpOnly = false;
                });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(nameof(MustOwnAnimalRequirement),
                    policy => policy.Requirements.Add(new MustOwnAnimalRequirement()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseCors(b => b
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
                .UseSwagger()
                .UseAuthentication()
                .UseSwaggerUI(swaggerUiOptions =>
                    swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment registration API"))
                .UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}