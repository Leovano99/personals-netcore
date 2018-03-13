using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.Price.Dto;
using VDI.Demo.PSAS.Term.Dto;

namespace VDI.Demo.PSAS.Price
{
    public interface IPSASPriceAppService : IApplicationService
    {
        GetPSASResultDto GetParameter(GetPSASParamsDto input);

        GetPSASPriceListDto GetPSASPrice(GetPSASParamsDto input);

        GetPSASPriceListDto GetMarketingPrice(GetPSASParamsDto input);

        GetPSASPriceListDto GetCommisionPrice(GetPSASParamsDto input);

        GetUniversalListDto GetUniversalPrice(GetPSASParamsDto input);

        List<GetAddDiscListDto> GetDiscountPrice(GetPSASParamsDto input);

        List<GetAddDiscListDto> GetDiscountPriceMKT(GetPSASParamsDto input);

        List<GetAddDiscListDto> GetDiscountPriceComm(GetPSASParamsDto input);

        void UpdatePSASPrice(UpdatePSASParamsDto input);

        void UpdateMarketingPrice(UpdatePSASParamsDto input);

        void UpdateCommisionPrice(UpdatePSASParamsDto input);

        void CreateUpdateDiscountPrice(CreateUpdatePriceParamsDto input);

        void CreateUpdateDiscountPriceMKT(CreateUpdatePriceParamsDto input);

        void CreateUpdateDiscountPriceComm(CreateUpdatePriceParamsDto input);

        void GeneratePrice(UpdateTermInputDto input);

    }
}
