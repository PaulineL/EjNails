using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteJu.Data;

namespace SiteJu.Areas.Identity.Data;

public class SiteJuIdentityDbContext : IdentityDbContext<Client, IdentityRole<int>, int>
{
    public SiteJuIdentityDbContext(DbContextOptions<SiteJuIdentityDbContext> options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
