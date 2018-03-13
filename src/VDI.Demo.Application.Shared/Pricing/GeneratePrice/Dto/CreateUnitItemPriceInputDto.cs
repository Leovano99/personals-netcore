using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class CreateUnitItemPriceInputDto
    {
        public int projectId { get; set; }
        public int termId { get; set; }
        public int clusterId { get; set; }
        public List<CreateUnitItemEtcInputDto> inputUnitItemPrice { get; set; }

        public class CreateUnitItemEtcInputDto
        {
            public string unitCode { get; set; }
            public string unitNo { get; set; }
            public string renovCode { get; set; }
            public string itemCode { get; set; }
            public decimal grossPrice { get; set; }
        }
    }
}
