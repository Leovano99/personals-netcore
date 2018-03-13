using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.Personal_Members.Dto;
using VDI.Demo.Personals.Personals.Dto;

namespace VDI.Demo.Personals.Personal_Members
{
    public interface IPersonalMemberAppService : IApplicationService
    {
        ListResultDto<GetAllPersonalMemberDto> GetAllPersonalMemberList(string keyword, string scmCode);
        string GenerateMemberCode(string scmCode, bool isInstitute);
        JObject UpdateMember(CreateMemberDto input);
        void DeleteMember(GetDeleteInputDto input);
    }
}
