using Microsoft.EntityFrameworkCore;

namespace Fallout.Models
{
  public class FalloutContext : DbContext
  {
    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<Survivor> Survivors { get; set; }
    public DbSet<ShelterSurvivor> ShelterSurvivor { get; set; }

    public FalloutContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}