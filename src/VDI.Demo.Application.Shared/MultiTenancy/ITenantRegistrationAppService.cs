using System.Threading.Tasks;
using Abp.Application.Services;
using VDI.Demo.Editions.Dto;
using VDI.Demo.MultiTenancy.Dto;

namespace VDI.Demo.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}