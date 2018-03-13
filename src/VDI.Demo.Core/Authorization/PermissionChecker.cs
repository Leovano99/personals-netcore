using Abp.Authorization;
using VDI.Demo.Authorization.Roles;
using VDI.Demo.Authorization.Users;

namespace VDI.Demo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
