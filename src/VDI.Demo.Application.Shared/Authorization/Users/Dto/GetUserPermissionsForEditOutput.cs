using System.Collections.Generic;
using VDI.Demo.Authorization.Permissions.Dto;

namespace VDI.Demo.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}