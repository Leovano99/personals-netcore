using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_BankAccount")]
    public class TR_BankAccount : Entity<string>
    {

        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id
        {
            get
            {
                return entityCode + "-" + psCode + "-" + refID + "-" + BankCode;
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

        [StringLength(5)]
        [Key]
        [Required]
        [Column(Order = 3)]
        public string BankCode { get; set; }

        [Required]
        [StringLength(40)]
        public string accountNo { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }
    }
}
