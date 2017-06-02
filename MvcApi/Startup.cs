using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using id.data;
using id.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using id.middleware.common;
using id.data.Repositories;
using id.application.Entities;
using id.application.Managers;

namespace MvcApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy //.WithOrigins("http://localhost:5003")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddRouting();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(option=>
            {
                option.Password.RequireUppercase = false;
                option.User.RequireUniqueEmail = true;
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddConfigurationStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddAspNetIdentity<ApplicationUser>();


            //inject repository, service
            services.AddTransient<DbContext, ApplicationDbContext>();
            services.AddTransient<IGenericRepository<dynamic>, GenericRepository<dynamic>>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IXMapper, XMapper>();
            services.AddTransient<IAccountManager, AccountManager>();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                
                options.AddPolicy("APIWRITE", policy => policy.RequireClaim("scope", "apis_fullaccess__write"));
            });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = "http://localhost:5000",
            //    RequireHttpsMetadata = false,
            //    AllowedScopes = { "openid" },
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});

            // this uses the policy called "default"
            app.UseCors("default");

            app.UseIdentity();
            app.UseIdentityServer();
            
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = Configuration.GetValue<string>("AppSettings:IdServerEndPoint"),
                RequireHttpsMetadata = false,

                //ApiName = "apis_fullaccess", // token trả về phải có aud chứa Resource này ( ApiName) thì mới xác thực, Không qui định thì là ALL, ko ràng buộc
                //AllowedScopes = { "apis_fullaccess__read", "apis_fullaccess__write" } // client phải request token với 1 trong các scope này mới dc chứng thực, KHONG QUI DINH -> ALL, ko ràng buộc

            });


            app.UseProcessingTimeMiddleware();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
