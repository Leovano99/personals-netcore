using Abp.Application.Services.Dto;
using System;
using VDI.Demo.Personals.MS_StatusMembers.Dto;
using System.Linq;
using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.Personals.MS_StatusMembers
{
    public class MsStatusMemberAppService : DemoAppServiceBase, IMsStatusMemberAppService
    {
        private readonly NewCommDbContext _context;

        public MsStatusMemberAppService(NewCommDbContext context)
        {
            _context = context;
        }

        public ListResultDto<GetAllMsStatusMemberDropdownList> GetAllMsStatusMemberDropdown(string scmCode)
        {
            var result = (from x in _context.MS_StatusMember
                          where x.scmCode == scmCode
                          select new GetAllMsStatusMemberDropdownList
                          {
                              statusCode = x.statusCode,
                              statusName = x.statusName
                          }).ToList();

            return new ListResultDto<GetAllMsStatusMemberDropdownList>(result);
        }        
    }
}
