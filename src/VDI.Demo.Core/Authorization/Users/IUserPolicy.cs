using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace VDI.Demo.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
