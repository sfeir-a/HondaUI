using LenelConfigService.Models;

namespace LenelConfigService.Services
{
    public interface IConfigService
    {
        Task<IEnumerable<Configuration>> GetAllAsync();
        Task<Configuration?> GetAsync(string endpointName);
        Task<Configuration> CreateAsync(Configuration config);
        Task<bool> UpdateAsync(string endpointName, Configuration config);
        Task<bool> DeleteAsync(string endpointName);
    }
}
