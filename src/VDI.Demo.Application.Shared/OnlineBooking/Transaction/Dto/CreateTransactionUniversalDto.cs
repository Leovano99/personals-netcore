using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.OnlineBooking.BookingHistory.Dto;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class CreateTransactionUniversalDto
    {
        public int payTypeID { get; set; }
        public string pscode { get; set; }
        public decimal totalAmt { get; set; }
        public int orderHeaderID { get; set; }
        public string orderCode { get; set; }

        public int? tujuanTransaksiID { get; set; }
        public int? sumberDanaID { get; set; }
        public string nomorRekeningPemilik { get; set; }
        public string bankRekeningPemilik { get; set; }

        public List<UnitUniversalResultDto> arrUnit { get; set; }
        public List<PPNUniversalResultDto> arrPP { get; set; }
        //public List<PDFUniversalResultDto> arrPDF { get; set; }

        public long userID { get; set; }
        public string memberCode { get; set; }
        public string memberName { get; set; }
        public string scmCode { get; set; }


        //public string ResiImg { get; set; }
        //public string finishRedirect { get; set; }

        //public string unfinishRedirect { get; set; }
        //public string errorRedirect { get; set; }
        //public string message { get; set; }
    }

    public class PDFUniversalResultDto
    {
        public string urlPdf { get; set; }
    }

    public class PPNUniversalResultDto
    {
        public string PPNo { get; set; }
    }

    public class UnitUniversalResultDto
    {
        public int unitID { get; set; }
        public int termID { get; set; }
        public int renovID { get; set; }
        public int projectID { get; set; }
        public decimal sellingprice { get; set; }
        public decimal bfAmount { get; set; }

    }
}
