using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class CheckDataUploadGrossPriceInputDto
    {
        public int ProjectId { get; set; }
        public int ClusterId { get; set; }
        public List<UnitInput> DataUnit { get; set; }
        public class UnitInput
        {
            public string UnitCode { get; set; }
            public string UnitNo { get; set; }
            public string ItemCode { get; set; }
            public string RenovCode { get; set; }
        }
    }
}
