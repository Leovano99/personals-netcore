using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.Pricing.GeneratePrice.Dto;

namespace VDI.Demo.Pricing.GeneratePrice.Exporter
{
    public interface IGeneratePriceListExcelExporter
    {
        FileDto GenerateExcelUploadGrossPrice(List<GenerateTemplateExcelListDto> generatePriceListDtos);
        FileDto GenerateExcelUploadPriceList(List<GenerateTemplateExcelListDto> generatePriceListDtos);
        //FileDto ExportToExcelUploadGrossPrice(List<ExportToExcelGeneratePriceListDto> generatePriceListDtos);
        FileDto ExportToExcelUploadGrossPrice(List<ExportToExcelUploadPriceListDto> exportToExcelUploadPriceListDto);
        FileDto ExportToExcelUploadPriceList(List<ExportToExcelUploadPriceListDto> exportToExcelUploadPriceListDto);
    }
}
