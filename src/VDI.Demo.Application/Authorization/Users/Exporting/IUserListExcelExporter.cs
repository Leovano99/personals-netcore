using System.Collections.Generic;
using VDI.Demo.Authorization.Users.Dto;
using VDI.Demo.Dto;

namespace VDI.Demo.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}