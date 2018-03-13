using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.BookingHistory.Dto
{
    public class DetailBookingHistoryResultDto
    {
        public int orderHeaderID { get; set; }
        public string orderCode { get; set; }
        public string oldOrderCode { get; set; }
        public int payTypeID { get; set; }
        public string payType { get; set; }
        public string membercode { get; set; }
        public string membername { get; set; }
        public string pscode { get; set; }
        public string psname { get; set; }
        public string custemail { get; set; }
        public string custphone { get; set; }
        public string scmcode { get; set; }
        public int statusID { get; set; }
        public string status { get; set; }
        public string IDNo { get; set; }
        public string address { get; set; }
        public DateTime orderDate { get; set; }

        public int? tujuanTransaksiID { get; set; }
        public int? sumberDanaID { get; set; }
        public string bankRekeningPemilik { get; set; }
        public string nomorRekeningPemilik { get; set; }
        public decimal totalAmount { get; set; }


        public List<UnitResultDto> arrUnit { get; set; }
        public List<PPNResultDto> arrPP { get; set; }

        public string username { get; set; }

        public string message { get; set; }
    }
    public class UnitResultDto
    {
        public string unitcode { get; set; }
        public string unitno { get; set; }
        public int unitID { get; set; }
        public int renovID { get; set; }
        public string renovcode { get; set; }
        public int termID { get; set; }
        public int termno { get; set; }
        public string termName { get; set; }
        public decimal sellingprice { get; set; }
        public decimal bfamount { get; set; }
        public string remarks { get; set; }
        public double? disc1 { get; set; }
        public double? disc2 { get; set; }
        public int projectID { get; set; }
    }
    public class PPNResultDto
    {
        public string PPNo { get; set; }
    }
}
