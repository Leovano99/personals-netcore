using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.OnlineBooking.Personal;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("TR_PriorityPass")]
    public class TR_PriorityPass : CreationAuditedEntity<int>
    {

        [Required]
        [StringLength(6)]
        public string PPNo { get; set; }

        [ForeignKey("MS_BatchEntry")]
        public int batchSeq { get; set; }
        public virtual MS_BatchEntry MS_BatchEntry { get; set; }

        [Required]
        [StringLength(1)]
        [Column(Order = 0)]
        public string entityCode { get; set; }

        [Required]
        [StringLength(8)]
        [Column(Order = 1)]
        public string psCode { get; set; }

        [Required]
        [StringLength(3)]
        [Column(Order = 2)]
        public string scmCode { get; set; }

        [Required]
        [StringLength(12)]
        [Column(Order = 3)]
        public string memberCode { get; set; }

        [StringLength(100)]
        public string idCard { get; set; }

        public DateTime? regTime { get; set; }

        public DateTime? dealingTime { get; set; }

        [Required]
        [StringLength(1)]
        public string PPStatus { get; set; }

        [StringLength(20)]
        public string PaymentType { get; set; }

        [Required]
        [StringLength(30)]
        public string CardNo { get; set; }

        [StringLength(20)]
        public string bank { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }

        public DateTime buyDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Kamar { get; set; }

        [Required]
        [StringLength(50)]
        public string Lantai { get; set; }

        [Required]
        [StringLength(50)]
        public string KPA { get; set; }

        [Required]
        [StringLength(6)]
        public string oldPPNo { get; set; }

        public int TableNo { get; set; }

        public int? idSetting { get; set; }

        public int? Token { get; set; }

        [StringLength(300)]
        public string remarks { get; set; }

        [StringLength(50)]
        public string inputFrom { get; set; }
    }
}
