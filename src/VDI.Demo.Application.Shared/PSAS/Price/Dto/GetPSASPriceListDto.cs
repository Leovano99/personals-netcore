using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Price.Dto
{
    public class GetPSASPriceListDto
    {
        public int? bookingHeaderID { get; set; }

        public string projectCode { get; set; }

        public GetAreaLlistDto area { get; set; }

        public GetGrosspriceLlistDto grossPrice { get; set; }

        public GetDiscountLlistDto discount { get; set; }

        public GetNetpriceLlistDto netPrice { get; set; }

        public GetAddDiscLlistDto addDisc { get; set; }

        public List<GetDiscountAlistDto> discountA { get; set; }

        public GetNetNetPriceLlistDto netNetPrice { get; set; }

        public GetVATLlistDto VATPrice { get; set; }

        public GetInterestLlistDto interest { get; set; }

        public GetTotalLlistDto total { get; set; }
    }
    public class GetAreaLlistDto
    {
        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }
    }
    public class GetDiscountLlistDto
    {
        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }
    }
    public class GetGrosspriceLlistDto
    {
        public decimal discount { get; set; }

        public decimal bangunan { get; set; }

        public decimal total { get; set; }
    }
    public class GetNetpriceLlistDto
    {
        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }
    }
    public class GetAddDiscLlistDto
    {
        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }
    }
    public class GetDiscountAlistDto
    {
        public int bookingDetailAddDiscID { get; set; }

        public int bookingDetailID { get; set; }

        public string discountName { get; set; }

        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }

        public bool isAmount { get; set; }
    }
    public class GetNetNetPriceLlistDto
    {
        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }
    }
    public class GetVATLlistDto
    {
        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }
    }
    public class GetTotalLlistDto
    {
        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }
    }
    public class GetInterestLlistDto
    {
        public double discount { get; set; }

        public double bangunan { get; set; }

        public double total { get; set; }
    }
}
