using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class KPPage2InputDto
    {
        public List<listIlustrasiPembayaranDto> ilustrasiPembayaran { get; set; }
    }
    public class listIlustrasiPembayaranDto
    {
        public int orderHeaderID { get; set; }
        public int termID { get; set; }
        public string termName { get; set; }
        public decimal bookingFee { get; set; }
        public decimal sellingPrice { get; set; }
        public DateTime tglJatuhTempo { get; set; }
        public List<listDP> listDP { get; set; }
        public List<listCicilan> listCicilan { get; set; }
    }
    public class listDP
    {
        public short DPNo { get; set; }
        public DateTime tglJatuhTempo { get; set; }
        public decimal amount { get; set; }
    }
    public class listCicilan
    {
        public int cicilanNo { get; set; }
        public DateTime tglJatuhTempo { get; set; }
        public string amount { get; set; }
        public string pelunasan1 { get; set; }
        public string pelunasan2 { get; set; }
    }
}
