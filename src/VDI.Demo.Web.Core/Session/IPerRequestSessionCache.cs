using System.Threading.Tasks;
using VDI.Demo.Sessions.Dto;

namespace VDI.Demo.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
