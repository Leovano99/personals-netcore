using System.Threading.Tasks;
using Abp.Configuration;

namespace VDI.Demo.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
