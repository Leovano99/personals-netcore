using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_ID")]
    public class TR_ID : Entity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotMapped]
        public override string Id
        {
            get
            {
                return psCode + "-" + refID;
            }
            set
            {
            }
        }

        [StringLength(1)]
        [Key]
        [Required]
        [Column(Order = 0)]
        public string entityCode { get; set; }

        [StringLength(8)]
        [Key]
        [Required]
        [Column(Order = 1)]
        public string psCode { get; set; }
        public virtual Personals Personals { get; set; }

        [Key]
        [Required]
        [Column(Order = 2)]
        public int refID { get; set; }

        [Required]
        [ForeignKey("LK_IDType")]
        [StringLength(1)]
        public string idType { get; set; }
        public virtual LK_IDType LK_IDType { get; set; }

        [Required]
        [StringLength(50)]
        public string idNo { get; set; }

        public DateTime? expiredDate { get; set; }

        public DateTime modifTime { get; set; }

        public string modifUN { get; set; }

        public DateTime? inputTime { get; set; }

        public string inputUN { get; set; }

    }
}
