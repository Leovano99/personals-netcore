using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_Projects.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_Projects
{
    public interface IMsProjectAppService : IApplicationService
    {
        void CreateMsProject(CreateMsProjectInputDto input);
        void UpdateMsProject(CreateMsProjectInputDto input);
        ListResultDto<GetAllProjectListDto> GetAllMsProject();
        //void ModifyDMT(GetUpdateDmtValueInputDto input);
        //void ModifyCorsec(GetUpdateDmtValueInputDto input);
        //GetUpdateDmtValueInputDto GetDMT();
        //GetUpdateDmtValueInputDto GetCorsec();
        //GetDetailMsProjectListDto GetDetailMsProject(int Id);
        //List<GetMappingDMTListDto> GetMappingDMT();
        //List<GetMappingCorsecListDto> GetMappingCorsec();
        void DeleteMsProject(int Id);
    }
}
