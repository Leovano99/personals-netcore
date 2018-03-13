using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_Family")]
    public class TR_Family : Entity<string>
    {

        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id
        {
            get
            {
                return entityCode + "-" + psCode + "-" + refID;
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

        [Key]
        [Column(Order = 2)]
        public int refID { get; set; }

        [StringLength(100)]
        public string familyName { get; set; }

        [StringLength(50)]
        public string familyStatus { get; set; }

        public DateTime birthDate { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }
    }
}
