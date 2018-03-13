using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.Pricing.GeneratePrice.Dto;

namespace VDI.Demo.Pricing.GeneratePrice
{
    public interface IGeneratePriceAppService : IApplicationService
    {
        List<GetGeneratePriceListTermListDto> GetGeneratePriceListTermByTermID(int termID);
        void CreatePriceTaskList(CreatePriceTaskListInputDto input);
        List<GetPriceTaskListDto> GetPriceTaskList(int projectID);

        // upload gross price
        FileDto GenerateExcelUploadGrossPrice(GetMsUnitByProjectClusterCategoryProductTermMain input);
        FileDto ExportToExcelUploadGrossPrice(ExportToExcelUploadGrossPriceListDto input);

        // upload price list
        FileDto GenerateExcelUploadPriceList(GetMsUnitByProjectIdClusterIdDto input);
        FileDto ExportToExcelUploadPriceList(List<ExportToExcelUploadPriceListDto> input);
    }
}
