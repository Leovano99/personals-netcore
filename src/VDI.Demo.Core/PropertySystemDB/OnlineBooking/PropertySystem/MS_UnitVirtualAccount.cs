using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("MS_UnitVirtualAccount")]
    public class MS_UnitVirtualAccount : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return unitCode + "-" + unitNo;
            }
            set
            {
            }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string unitCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string unitNo { get; set; }

        [Required]
        [StringLength(100)]
        public string VA_BankName { get; set; }

        [Required]
        [StringLength(100)]
        public string VA_BankAccNo { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        [StringLength(200)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        [StringLength(200)]
        public string inputUN { get; set; }
    }
}
