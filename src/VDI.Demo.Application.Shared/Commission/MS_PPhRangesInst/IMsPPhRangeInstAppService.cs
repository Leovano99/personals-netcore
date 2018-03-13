using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.MS_PPhRangesInst.Dto;

namespace VDI.Demo.Commission.MS_PPhRangesInst
{
    public interface IMsPPhRangeInstAppService : IApplicationService
    {
        ListResultDto<CreateOrUpdatePPhRangeInstListDto> GetMsPPhRangeInstBySchemaId(int schemaID);
        void CreateMsPPhRangeInst(List<CreateOrUpdatePPhRangeInstListDto> input);
        void UpdateMsPPhRangeInst(CreateOrUpdatePPhRangeInstListDto input);
        void DeleteMsPPhRange(int Id);
    }
}
