using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.MS_PointPercentage.Dto;

namespace VDI.Demo.Commission.MS_PointPercentage
{
    public interface IMsPointPctAppService : IApplicationService
    {
        void CreateMsPointPct(List<InputPointPctDto> input);
        void UpdateMsPointPct(InputPointPctDto input);
        void DeleteMsPointPct(int Id);
        ListResultDto<GetAllPointPctListDto> GetMsPointPctBySchemaId(int schemaID);
    }
}
