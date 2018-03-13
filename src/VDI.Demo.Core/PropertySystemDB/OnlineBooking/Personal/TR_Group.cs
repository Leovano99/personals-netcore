using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_Group")]
    public class TR_Group : Entity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotMapped]
        public override string Id
        {
            get
            {
                return groupCode + "-" + psCode;
            }
            set
            {
            }
        }

        [StringLength(5)]
        [Key]
        [Required]
        [Column(Order = 1)]
        public string groupCode { get; set; }

        [StringLength(1)]
        [Key]
        [Required]
        [Column(Order = 0)]
        public string entityCode { get; set; }

        [StringLength(8)]
        [Key]
        [Required]
        [Column(Order = 2)]
        public string psCode { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }
    }
}
