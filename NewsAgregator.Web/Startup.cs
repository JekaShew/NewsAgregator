using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using NewsAgregator.Data;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Services.AccountServices;
using NewsAgregator.Abstract.CommentInterfaces;
using NewsAgregator.Services.CommentServices;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Services.ComplaintServices;
using NewsAgregator.Abstract.MessageInterfaces;
using NewsAgregator.Services.MessageServices;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.Services.NewsServices;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.Services.WeatherServices;
using Serilog;
using Hangfire;


namespace NewsAgregator.Web
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Services.AutoMapper));
            //Home
            //services.AddDbContext<AppDBContext>(options => options.UseSqlServer(@"Server=JEKASHEW;Database=NewsAgregator;Trusted_Connection=True;TrustServerCertificate=True"));
            //Work
            //services.AddDbContext<AppDBContext>(options => options.UseSqlServer(@"Server=SHEVTSOV2-10;Database=NewsAgregator;Trusted_Connection=True;TrustServerCertificate=True"));
            services.AddSerilog((services, lc) => lc
                .ReadFrom.Configuration(Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Error)
                .WriteTo.File("NewsAggregatorLogs.log"));
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString: Configuration.GetConnectionString("Home")));

            var jwtIss = Configuration.GetSection("JWT:Iss").Get<string>();
            var jwtAud = Configuration.GetSection("JWT:Aud").Get<string>();
            var jwtKey = Configuration.GetSection("JWT:SecretKey").Get<string>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = jwtIss,

                       ValidateAudience = true,
                       ValidAudience = jwtAud,
                       ValidateLifetime = true,

                       IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey)),
                       ValidateIssuerSigningKey = true,
                   };
               });
            services.AddAuthorization();
            
            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(Configuration.GetConnectionString("Home")));
            services.AddHangfireServer();

            services.AddMvc();
            services.AddCors();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsAgragator Web API" });
                x.TagActionsBy(api =>
                {
                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor == null)
                        throw new InvalidOperationException("Unable to determine tag for endpoint.");

                    if (api.GroupName != null)
                        return new[] { $"{api.GroupName} {controllerActionDescriptor.ControllerName}" };

                    return new[] { controllerActionDescriptor.ControllerName };
                });
                x.DocInclusionPredicate((name, api) => true);
            });

 
            services.AddDbContext<AppDBContext>();
            //services.AddTransient<DbContext>(isp => isp.GetService<AppDBContext>());
           
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IAccountStatusServices, AccountStatusServices>();
            services.AddScoped<IPolicyServices, PolicyServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<IAccountAuthorizationServices, AccountAuthorizationServices>();

            services.AddScoped<ICommentServices, CommentServices>();
           
            services.AddScoped<IComplaintServices, ComplaintServices>();
            services.AddScoped<IComplaintStatusServices, ComplaintStatusServices>();
            services.AddScoped<IComplaintTypeServices, ComplaintTypeServices>();

            services.AddScoped<INotificationMessageServices, NotificationMessageServices>();

            services.AddScoped<INewsServices, NewsServices>();
            services.AddScoped<INewsStatusServices, NewsStatusServices>();

            services.AddScoped<IWeatherServices, WeatherServices>();
            services.AddScoped<IWeatherStatusServices, WeatherStatusServices>();

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDBContext appDBContext)
        {
            app.UseHttpsRedirection();
            appDBContext.Database.EnsureCreated();
            //appDBContext.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true)
                .AllowCredentials().WithExposedHeaders("Set-Cookie", "set-cookie"));
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSerilogRequestLogging();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "NewsAgragator Web API"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

