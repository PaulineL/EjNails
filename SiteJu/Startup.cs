using Infrastructure.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendGrid.Extensions.DependencyInjection;
using SiteJu.Configuration;
using SiteJu.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using SiteJu.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web.UI;
using Microsoft.Identity.Web;
using SiteJu.Data;

namespace SiteJu
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
            // Context Réservation
            services.AddDbContext<ReservationContext>(options =>
               options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // fltre d'exception de la bdddd réservar
            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddAuthentication()
                .AddMicrosoftIdentityWebApp(options =>
                {
                    Configuration.Bind("AzureAd", options);

                    options.Events = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents()
                    {
                        OnAuthenticationFailed = async e =>
                        {

                        },
                        OnAuthorizationCodeReceived = async e =>
                        {

                        },
                        OnRemoteFailure = async e =>
                        {

                        },
                        OnAccessDenied = async e =>
                        {

                        },
                        OnRedirectToIdentityProvider = async e =>
                        {

                        }
                    };
                });


            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI()
                .AddRazorRuntimeCompilation();

            services.Configure<MailOption>(Configuration.GetSection("Mail"));
            services.Configure<Web>(Configuration.GetSection("Web"));


            var mailProvider = Configuration["Mail:Provider"];
            if (mailProvider == "Sendgrid")
            {
                services.AddSendGrid(client =>
                {
                    client.ApiKey = Configuration["Mail:Sendgrid:ApiKey"];
                });
                services.AddSingleton<IMailSender, SendgridMailSender>();
            }
            else if (mailProvider == "Microsoft.Graph")
            {
                services.AddSingleton<IMailSender, MicrosoftGraph>();
            }
            else
            {
                services.AddSingleton<IMailSender, MailSenderDefault>();
            }

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SiteJuIdentityDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<SiteJuIdentityDbContext>();


            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.Map(PathString.FromUriComponent("/robots.txt"), robot => robot.UseMiddleware<RobotMiddleware>());

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
