using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class GetUnitItemPriceResultDto
    {
        public int itemID { get; set; }

        public string coCode { get; set; }

        public decimal amount { get; set; }

        public double pctDisc { get; set; }

        public double pctTax { get; set; }

        public double area { get; set; }

        public int finTypeID { get; set; }

        public int entityID { get; set; }

        public short finStartDue { get; set; }
    }
    public class bookingDetailIDDto
    {
        public int bookingHeaderID { get; set; }

        public int bookingDetailID { get; set; }

        public int itemID { get; set; }

        public double area { get; set; }

        public int bookNo { get; set; }

        public string coCode { get; set; }

        public double pctTax { get; set; }

        public bool result { get; set; }
    }
}
