using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_Email")]
    public class TR_Email : Entity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotMapped]
        public override string Id
        {
            get
            {
                return psCode;
            }
            set
            {
            }
        }

        [StringLength(1)]
        [Key]
        [Required]
        [ForeignKey("Personals")]
        [Column(Order = 0)]
        public string entityCode { get; set; }

        [StringLength(8)]
        [Key]
        [Required]
        [ForeignKey("Personals")]
        [Column(Order = 1)]
        public string psCode { get; set; }
        public virtual Personals Personals { get; set; }

        public int refID { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        public DateTime modifTime { get; set; }

        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        public string inputUN { get; set; }

    }
}
