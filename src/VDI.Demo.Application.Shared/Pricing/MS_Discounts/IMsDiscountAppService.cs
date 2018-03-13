using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Pricing.MS_Discounts.Dto;

namespace VDI.Demo.Pricing.MS_Discounts
{
    public interface IMsDiscountAppService : IApplicationService
    {
        ListResultDto<GetAllMsDiscountListDto> GetAllMsDiscount();
        int CreateMsDiscount(CreateMsDiscountInput input);
        JObject UpdateMsDiscount(CreateMsDiscountInput input);
        void DeleteMsDiscount(int Id);
    }
}
