using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Unit.MS_UnitCodes.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_UnitCodes
{
    public interface IMsUnitCodeAppService : IApplicationService
    {
        ListResultDto<GetAllMsUnitCodeListDto> GetAllMsUnitCode(int projectID);
        void CreateMsUnitCode(List<CreateOrUpdateMsUnitCodeInputDto> input);
        JObject UpdateMsUnitCode(CreateOrUpdateMsUnitCodeInputDto input);
        void DeleteMsUnitCode(int id);
    }
}
