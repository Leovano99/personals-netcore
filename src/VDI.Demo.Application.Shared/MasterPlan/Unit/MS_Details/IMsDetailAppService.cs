using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Unit.MS_Details.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Details
{
    public interface IMsDetailAppService : IApplicationService
    {
        ListResultDto<GetAllMsDetailListDto> GetAllMsDetail();
        void CreateMsDetail(CreateOrUpdateMsDetailInputDto input);
        JObject UpdateMsDetail(CreateOrUpdateMsDetailInputDto input);
        void DeleteMsDetail(int id);
    }
}
