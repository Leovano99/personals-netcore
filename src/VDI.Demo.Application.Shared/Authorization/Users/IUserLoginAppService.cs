using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.Authorization.Users.Dto;

namespace VDI.Demo.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
