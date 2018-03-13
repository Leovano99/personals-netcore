using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.Common.Dto;
using VDI.Demo.Editions.Dto;

namespace VDI.Demo.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}