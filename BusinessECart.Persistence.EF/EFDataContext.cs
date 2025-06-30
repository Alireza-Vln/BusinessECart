using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessECart.Persistence.EF;

public class EFDataContext(DbContextOptions<EFDataContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    
    public EFDataContext(
        string connectionString)
        : this(
            new DbContextOptionsBuilder<EFDataContext>()
                .UseSqlServer(connectionString).Options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
    }
}
