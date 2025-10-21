using Microsoft.EntityFrameworkCore;
using LenelConfigService.Models;

namespace LenelConfigService.Data
{
    public class ConfigContext : DbContext
    {
        public ConfigContext(DbContextOptions<ConfigContext> options) : base(options) { }

        public DbSet<Configuration> Configurations => Set<Configuration>();
    }
}
