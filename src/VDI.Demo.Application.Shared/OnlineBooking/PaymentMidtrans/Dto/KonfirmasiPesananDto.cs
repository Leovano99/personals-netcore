using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans.Dto
{
    public class KonfirmasiPesananDto
    {
        //public string UnitCode { get; set; }
        //public string UnitNo { get; set; }
        public string orderCode { get; set; }
        public string BookCode { get; set; }
        public string imageProject { get; set; }
        public string kodePelanggan { get; set; }
        public string tanggalBooking { get; set; }
        public string psName { get; set; }
        public string noIdentitas { get; set; }
        public string birthDate { get; set; }
        public string noNPWP { get; set; }
        public string noHpPembeli { get; set; }
        public string email { get; set; }
        public List<unitDto> listUnit { get; set; }
        public string hargaJual { get; set; }
        public string namaBank { get; set; }
        public string caraPembayaran { get; set; }
        public string noRekening { get; set; }
        public string tujuanTransaksi { get; set; }
        public string sumberDanaPembelian { get; set; }
        public string lblHandOver { get; set; }
        public string lblGracePeriod { get; set; }
        public string rekComName { get; set; }
        public List<listBankDto> listBank { get; set; }
        public string namaBankVA { get; set; }
        public string noAccVA { get; set; }
        public string bfAmount { get; set; }
        public string noDealCloser { get; set; }
        public string noHp { get; set; }
        public string namaDealCloser { get; set; }
        public List<listIlustrasiPembayaran> ilustrasiPembayaran { get; set; }
        public string imageLippo { get; set; }
        public int unitID { get; set; }
        public int renovID { get; set; }
    }
    public class listBankDto
    {
        public string bankName { get; set; }
        public string noVA { get; set; }
    }
    public class unitDto
    {
        public string UnitCode { get; set; }
        public string UnitNo { get; set; }
        public string luas { get; set; }
        public string tipe { get; set; }
        public string renovation { get; set; }
        public string cluster { get; set; }
        public string category { get; set; }
    }
    public class listIlustrasiPembayaran
    {
        public int termID { get; set; }
        public string termName { get; set; }
        public decimal bookingFee { get; set; }
        public DateTime tglJatuhTempo { get; set; }
        public List<listDpDto> listDP { get; set; }
        public List<listCicilanDto> listCicilan { get; set; }
    }
    public class listDpDto
    {
        public short DPNo { get; set; }
        public DateTime tglJatuhTempo { get; set; }
        public decimal amount { get; set; }
    }
    public class listCicilanDto
    {
        public int cicilanNo { get; set; }
        public DateTime tglJatuhTempo { get; set; }
        public decimal amount { get; set; }
    }
}
