using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.TR_DealClosers.Dto;

namespace VDI.Demo.Commission.TR_DealClosers
{
    public interface ITrDealCloserAppService : IApplicationService
    {
        List<GetMemberFromPersonalListDto> GetDropdownMemberFromPersonal();
        //void UpdateDealCloser(MemberFromPersonalInputDto input, int limitAsUplineNo);
        List<GetTasklistDealCloserByProjectListDto> GetTasklistDealCloserByProject(int ProjectId);
        GetDataEditDealCloserListDto GetDataEditDealCloser(string bookNo);
    }
}
