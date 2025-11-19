using LenelConfigService.Data;
using LenelConfigService.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using LenelConfigService.Attributes;
using System.Text.Json.Serialization;

namespace LenelConfigService.Services
{
    public class ConfigService : IConfigService
    {
        private readonly ConfigContext _db;
        public ConfigService(ConfigContext db) => _db = db;

        // GET ALL
        public async Task<IEnumerable<ExtractConfiguration>> GetAllAsync() =>
            await _db.Configurations
                .OrderBy(c => c.EndpointName)
                .ToListAsync();

        // GET BY ID
        public async Task<ExtractConfiguration?> GetAsync(int id) =>
            await _db.Configurations.FindAsync(id);

        // CREATE
        public async Task<ExtractConfiguration> CreateAsync(ExtractConfiguration config)
        {
            _db.Configurations.Add(config);
            await _db.SaveChangesAsync();
            return config;
        }

        // UPDATE BY ID
        public async Task<bool> UpdateAsync(int id, ExtractConfiguration config)
        {
            var existing = await _db.Configurations.FindAsync(id);
            if (existing == null) return false;

            // Preserve primary key
            config.Id = existing.Id;

            // EF will map new values onto the tracked entity
            _db.Entry(existing).CurrentValues.SetValues(config);

            await _db.SaveChangesAsync();
            return true;
        }

        // DELETE BY ID
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Configurations.FindAsync(id);
            if (existing == null) return false;

            _db.Configurations.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        //DEACTIVATE
        public async Task<bool> DeactivateAsync(int id)
        {
            var existing = await _db.Configurations.FindAsync(id);
            if (existing == null) return false;

            // Set all field toggles to false
            existing.EmplId = false;
            existing.BadgeId = false;
            existing.LastName = false;
            existing.FirstName = false;
            existing.MiddleName = false;
            existing.ContractNumber = false;
            existing.NetId = false;
            existing.CompanyName = false;
            existing.CreateProgram = false;
            existing.CreateTimestamp = false;
            existing.RowId = false;
            existing.RowStamp = false;
            existing.LastChanged = false;
            existing.BadgeType = false;

            await _db.SaveChangesAsync();
            return true;
        }

        // GET FIELD NAMES
        public Task<IEnumerable<string>> GetAllFieldNamesAsync()
        {
            var fieldNames = typeof(ExtractConfiguration)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<FieldAttribute>() != null)
                .Select(p =>
                    p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name
                    ?? char.ToLowerInvariant(p.Name[0]) + p.Name[1..])
                .ToList();

            return Task.FromResult<IEnumerable<string>>(fieldNames);
        }
    }
}
