using LenelConfigService.Models;

namespace LenelConfigService.Services
{
    public interface IConfigService
    {
        Task<IEnumerable<ExtractConfiguration>> GetAllAsync();
        Task<ExtractConfiguration?> GetAsync(int id);
        Task<ExtractConfiguration> CreateAsync(ExtractConfiguration config);
        Task<bool> UpdateAsync(int id, ExtractConfiguration config);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<string>> GetAllFieldNamesAsync();
    }
}
