using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.OnlineBooking.PPOnline;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("TR_UnitOrderHeader")]
    public class TR_UnitOrderHeader : AuditedEntity
    {
        [StringLength(20)]
        public string orderCode { get; set; }

        public DateTime orderDate { get; set; }

        [Required]
        [StringLength(8)]
        public string psCode { get; set; }

        [Required]
        [StringLength(200)]
        public string psName { get; set; }

        [Required]
        [StringLength(200)]
        public string psEmail { get; set; }

        [StringLength(200)]
        public string psPhone { get; set; }

        [Required]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Required]
        [StringLength(200)]
        public string memberName { get; set; }

        [Column(TypeName = "money")]
        public decimal totalAmt { get; set; }

        //public int payType { get; set; }

        [ForeignKey("LK_PaymentType")]
        public int paymentTypeID { get; set; }
        public virtual LK_PaymentType LK_PaymentType { get; set; }

        //public int status { get; set; }

        [ForeignKey("LK_BookingOnlineStatus")]
        public int statusID { get; set; }
        public virtual LK_BookingOnlineStatus LK_BookingOnlineStatus { get; set; }

        public long userID { get; set; }

        [StringLength(3)]
        public string scmCode { get; set; }

        [StringLength(20)]
        public string oldOrderCode { get; set; }

        [ForeignKey("MS_SumberDana")]
        public int? sumberDanaID { get; set; }
        public virtual MS_SumberDana MS_SumberDana { get; set; }

        [ForeignKey("MS_TujuanTransaksi")]
        public int? tujuanTransaksiID { get; set; }
        public virtual MS_TujuanTransaksi MS_TujuanTransaksi { get; set; }

        [Required]
        [StringLength(50)]
        public string bankRekeningPemilik { get; set; }

        [Required]
        [StringLength(50)]
        public string nomorRekeningPemilik { get; set; }

        public ICollection<TR_UnitOrderDetail> TR_UnitOrderDetail { get; set; }

        public ICollection<TR_PaymentOnlineBook> TR_PaymentOnlineBook { get; set; }

    }
}
