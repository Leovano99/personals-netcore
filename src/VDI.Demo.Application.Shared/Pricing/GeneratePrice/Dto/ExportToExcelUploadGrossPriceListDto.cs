using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class ExportToExcelUploadGrossPriceListDto
    {
        public List<ExportToExcelUploadPriceListDto> priceList { get; set; }
        public CreateUnitItemPriceInputDto unitItemPrice { get; set; }
    }
}
