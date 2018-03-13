using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Pricing.TR_BasePrices.Dto;

namespace VDI.Demo.Pricing.TR_BasePrices
{
    public interface ITrBasePriceAppService : IApplicationService
    {
        Task<PagedResultDto<GetAllBasePriceListDto>> GetAllTrBasePrice(GetBasePriceListInputDto input);
        void UploadBasePrice(UploadBasePriceInputDto input);
    }
}
