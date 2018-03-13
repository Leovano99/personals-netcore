using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("SYS_Counter")]
    public class Sys_Counter : Entity<string>
    {
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [Key]
        [StringLength(1)]
        public string entityCode { get; set; }

        [StringLength(8)]
        public string psCode { get; set; }

        public DateTime? modifTime { get; set; }

        public string modifUN { get; set; }

        public DateTime? inputTime { get; set; }

        public string inputUN { get; set; }
    }
}
