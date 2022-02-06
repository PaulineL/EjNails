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
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using SiteJu.Helpers;

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
            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddAuthentication("AzureAD")
                .AddMicrosoftIdentityWebApp(Configuration, "AzureAd", "AzureAD", cookieScheme: null, displayName: "Azure AD");

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, "AzureAD");
                    policy.RequireAuthenticatedUser();
                });
            });

            services
                .AddControllersWithViews()
                .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles)
                .AddMicrosoftIdentityUI()
                .AddRazorRuntimeCompilation();

            services.AddRazorPages();

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
            services.AddTransient<IEmailSender, IdentityMailSender>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SiteJuIdentityDbContext>(options => options.UseSqlite(connectionString));
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<SiteJuIdentityDbContext>();

            // Context Réservation
            services.AddDbContext<ReservationContext>(options => options.UseSqlite(connectionString));
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
                endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}"
                    );
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}
