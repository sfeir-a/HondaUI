namespace LenelConfigService.Services
{
    using LenelConfigService.Models;

    public interface IMappingService
    {
        ExtractConfigurationDto ToDto(ExtractConfiguration entity);
        ExtractConfiguration ToEntity(ExtractConfigurationDto dto);
        void UpdateEntityFromDto(ExtractConfiguration entity, ExtractConfigurationDto dto);
    }
}
