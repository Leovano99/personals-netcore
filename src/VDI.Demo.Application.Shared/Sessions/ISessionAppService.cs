using System.Threading.Tasks;
using Abp.Application.Services;
using VDI.Demo.Sessions.Dto;

namespace VDI.Demo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
        void SetSessionData(string key, string value);
        string GetSessionData(string key);
    }
}
