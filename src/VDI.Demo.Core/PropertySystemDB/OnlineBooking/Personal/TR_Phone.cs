using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_Phone")]
    public class TR_Phone : Entity<string>
    {
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
        [ForeignKey("Personals")]
        [Column(Order = 0)]
        public string entityCode { get; set; }

        [StringLength(8)]
        [Key]
        [ForeignKey("Personals")]
        [Column(Order = 1)]
        public string psCode { get; set; }
        public virtual Personals Personals { get; set; }

        [Key]
        [Column(Order = 2)]
        public int refID { get; set; }

        [StringLength(1)]
        public string phoneType { get; set; }

        [StringLength(30)]
        public string number { get; set; }

        public DateTime modifTime { get; set; }

        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        public string inputUN { get; set; }

        [StringLength(100)]
        public string remarks { get; set; }

    }
}
