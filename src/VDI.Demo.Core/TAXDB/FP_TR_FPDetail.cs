using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.TAXDB
{
    [Table("FP_TR_FPDetail")]
    public class FP_TR_FPDetail : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                  "-" + coCode +
                  "-" + FPCode +
                  "-" + transNo;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        //[ForeignKeyAttribute("FP_TR_FPHeader")]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        //[ForeignKeyAttribute("FP_TR_FPHeader")]
        [StringLength(5)]
        public string coCode { get; set; }

        [Key]
        [Column(Order = 2)]
        //[ForeignKeyAttribute("FP_TR_FPHeader")]
        [StringLength(20)]
        public string FPCode { get; set; }

        [Key]
        [Column(Order = 3)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short transNo { get; set; }

        [Required]
        [StringLength(200)]
        public string transDesc { get; set; }

        [Column(TypeName = "money")]
        public decimal transAmount { get; set; }

        [Required]
        [StringLength(5)]
        public string currencyCode { get; set; }

        [Column(TypeName = "money")]
        public decimal currencyRate { get; set; }

        [ForeignKey("entityCode,coCode,FPCode")]
        public virtual FP_TR_FPHeader FP_TR_FPHeader { get; set; }
    }
}
