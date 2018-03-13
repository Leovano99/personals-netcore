using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using VDI.Demo.Authorization.Users;
using VDI.Demo.MultiTenancy;

namespace VDI.Demo.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
