using Abp.Application.Services.Dto;

namespace VDI.Demo.Localization.Dto
{
    public class ApplicationLanguageListDto : FullAuditedEntityDto
    {
        public virtual int? TenantId { get; set; }
        
        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Icon { get; set; }

        public bool IsDisabled { get; set; }
    }
}