using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Threading.Tasks;

namespace SiteJu.Middleware
{
    public class RobotMiddleware
    {
        private readonly IFileProvider fileProvider;

        public RobotMiddleware(RequestDelegate _)
        {
            fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            IFileInfo robotFile;
            if (bool.TryParse(configuration["EnableCrawler"], out bool isEnabled) && isEnabled)
            {
                robotFile = fileProvider.GetFileInfo("robots.enable.txt");
            }
            else
            {
                robotFile = fileProvider.GetFileInfo("robots.disable.txt");
            }
            await context.Response.SendFileAsync(robotFile, context.RequestAborted);
        }
    }
}
