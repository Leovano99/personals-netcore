using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_IDFamily")]
    public class TR_IDFamily : Entity<string>

    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotMapped]
        public override string Id
        {
            get
            {
                return psCode + "-" + familyRefID + "-" + refID;
            }
            set
            {
            }
        }

        [StringLength(8)]
        [Key]
        [Required]
        [Column(Order = 0)]
        public string psCode { get; set; }
        public virtual Personals Personals { get; set; }

        [Key]
        [Column(Order = 1)]
        public int familyRefID { get; set; }

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

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }

    }
}
