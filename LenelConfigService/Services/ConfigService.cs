using LenelConfigService.Data;
using LenelConfigService.Models;
using Microsoft.EntityFrameworkCore;

namespace LenelConfigService.Services
{
    public class ConfigService : IConfigService
    {
        private readonly ConfigContext _db;
        public ConfigService(ConfigContext db) => _db = db;

        public async Task<IEnumerable<Configuration>> GetAllAsync() =>
          await _db.Configurations.OrderBy(c => c.EndpointName).ToListAsync();

        public async Task<Configuration?> GetAsync(string endpointName) =>
          await _db.Configurations.FindAsync(endpointName);

        public async Task<Configuration> CreateAsync(Configuration config)
        {
            _db.Configurations.Add(config);
            await _db.SaveChangesAsync();
            return config;
        }

        public async Task<bool> UpdateAsync(string endpointName, Configuration config)
        {
            var existing = await _db.Configurations.FindAsync(endpointName);
            if (existing == null) return false;

            // Copy incoming values onto the tracked entity (preserves key)
            var key = existing.EndpointName;
            _db.Entry(existing).CurrentValues.SetValues(config);
            existing.EndpointName = key;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string endpointName)
        {
            var existing = await _db.Configurations.FindAsync(endpointName);
            if (existing == null) return false;

            _db.Configurations.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
