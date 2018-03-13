using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.Authorization.Permissions.Dto;

namespace VDI.Demo.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
