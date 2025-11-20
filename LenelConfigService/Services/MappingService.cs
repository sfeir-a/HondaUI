using LenelConfigService.Models;
using System.Linq;

namespace LenelConfigService.Services
{
    public class MappingService : IMappingService
    {
        // Convert entity to DTO for UI
        public ExtractConfigurationDto ToDto(ExtractConfiguration e)
        {
            var active = new List<string>();

            if (e.EmplId) active.Add("emplId");
            if (e.BadgeId) active.Add("badgeId");
            if (e.LastName) active.Add("lastName");
            if (e.FirstName) active.Add("firstName");
            if (e.MiddleName) active.Add("middleName");
            if (e.ContractNumber) active.Add("contractNumber");
            if (e.NetId) active.Add("netId");
            if (e.CompanyName) active.Add("companyName");
            if (e.CreateProgram) active.Add("createProgram");
            if (e.CreateTimestamp) active.Add("createTimestamp");
            if (e.RowId) active.Add("rowId");
            if (e.RowStamp) active.Add("rowStamp");
            if (e.LastChanged) active.Add("lastChanged");
            if (e.BadgeType) active.Add("badgeType");

            return new ExtractConfigurationDto
            {
                Id = e.Id,
                EndpointName = e.EndpointName,
                Frequency = e.Frequency,
                Url = e.Url,
                Status = e.Status,

                CredentialUser = e.CredentialUser,
                CredentialPassword = null,

                HasCredentialPassword = e.CredentialPassword != null &&
                                        e.CredentialPassword.Length > 0,

                ActiveFields = active.ToArray()
            };
        }

        // Convert DTO to new entity (used in POST)
        public ExtractConfiguration ToEntity(ExtractConfigurationDto dto)
        {
            var entity = new ExtractConfiguration
            {
                Id = dto.Id,
                EndpointName = dto.EndpointName,
                Frequency = dto.Frequency,
                Url = dto.Url,
                Status = dto.Status,
                CredentialUser = dto.CredentialUser
            };

            ApplyActiveFieldsToEntity(entity, dto.ActiveFields);
            return entity;
        }

        // Update existing entity (used in PUT)
        public void UpdateEntityFromDto(ExtractConfiguration entity, ExtractConfigurationDto dto)
        {
            entity.EndpointName = dto.EndpointName;
            entity.Frequency = dto.Frequency;
            entity.Url = dto.Url;
            entity.Status = dto.Status;
            entity.CredentialUser = dto.CredentialUser;

            ApplyActiveFieldsToEntity(entity, dto.ActiveFields);
        }

        // Helper: take string[] ActiveFields and set all booleans
        private void ApplyActiveFieldsToEntity(ExtractConfiguration entity, string[] active)
        {
            entity.EmplId         = active.Contains("emplId");
            entity.BadgeId        = active.Contains("badgeId");
            entity.LastName       = active.Contains("lastName");
            entity.FirstName      = active.Contains("firstName");
            entity.MiddleName     = active.Contains("middleName");
            entity.ContractNumber = active.Contains("contractNumber");
            entity.NetId          = active.Contains("netId");
            entity.CompanyName    = active.Contains("companyName");
            entity.CreateProgram  = active.Contains("createProgram");
            entity.CreateTimestamp= active.Contains("createTimestamp");
            entity.RowId          = active.Contains("rowId");
            entity.RowStamp       = active.Contains("rowStamp");
            entity.LastChanged    = active.Contains("lastChanged");
            entity.BadgeType      = active.Contains("badgeType");
        }
    }
}
