using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.Pricing.MarketingFactor.Dto;

namespace VDI.Demo.Pricing.MarketingFactor
{
    public interface IMarketingFactorAppService : IApplicationService
    {
        FileDto ExportToExcelMarketingFactor(ExportMarketingFactorListDto param);
    }
}
