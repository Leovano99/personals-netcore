using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_Company")]
    public class TR_Company : Entity<string>
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
        [Required]
        [Column(Order = 2)]
        public int refID { get; set; }

        [Required]
        [StringLength(200)]
        public string coName { get; set; }

        [Required]
        [StringLength(500)]
        public string coAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string coCity { get; set; }

        [Required]
        [StringLength(10)]
        public string coPostCode { get; set; }

        [Required]
        [StringLength(50)]
        public string coCountry { get; set; }

        [Required]
        [StringLength(100)]
        public string coType { get; set; }

        [Required]
        [StringLength(100)]
        public string jobTittle { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }
    }
}
