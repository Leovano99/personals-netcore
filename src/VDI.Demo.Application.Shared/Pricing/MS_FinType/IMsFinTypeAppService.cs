using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Pricing.MS_FinType.Dto;

namespace VDI.Demo.Pricing.MS_FinType
{
    public interface IMsFinTypeAppService : IApplicationService
    {
        ListResultDto<GetAllMsFinTypeListDto> GetAllMsFinType();
        ListResultDto<string> GetAllFinType();
        void CreateMsFinType(CreateMsFinTypeInputDto input);
        void UpdateMsFinType(UpdateMsFinTypeInputDto input);
        void DeleteMsFinType(int finTypeID);
    }
}
