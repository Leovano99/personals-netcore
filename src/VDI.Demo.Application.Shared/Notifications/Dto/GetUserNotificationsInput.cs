using Abp.Notifications;
using VDI.Demo.Dto;

namespace VDI.Demo.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}