using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class TrBookingHeaderInputDto
    {
        public long userID { get; set; }

        public int unitID { get; set; }

        public string psCode { get; set; }

        public int termID { get; set; }

        public int? sumberDanaID { get; set; }

        public int? tujuanTransaksiID { get; set; }

        public string nomorRekeningPemilik { get; set; }

        public string bankRekeningPemilik { get; set; }

        public decimal sellingPrice { get; set; }

        public string memberCode { get; set; }

        public string memberName { get; set; }

    }
}
