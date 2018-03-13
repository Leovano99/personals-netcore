using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("TR_CommPaymentPph")]
    public class TR_CommPaymentPph : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                    "-" + scmCode +
                    "-" + devCode;
            }
            set { /* nothing */ }
        }
        [Required]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Required]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Required]
        [StringLength(5)]
        public string propCode { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string devCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string bookNo { get; set; }

        [Key]
        [Column(Order = 2)]
        public short asUplineNo { get; set; }

        [Key]
        [Column(Order = 3)]
        public short commNo { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(3)]
        public string commTypeCode { get; set; }

        [Key]
        [Column(Order = 6)]
        public byte reqNo { get; set; }

        [Key]
        [Column(Order = 7)]
        public short pphNo { get; set; }

        public bool isInst { get; set; }

        public bool isPKP { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        [Required]
        [StringLength(15)]
        public string AWT_GROUP_NAME { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string isHold { get; set; }

        public double PPHRange { get; set; }
    }
}
