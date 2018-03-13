using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("MS_Group")]
    public class MS_Group : Entity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotMapped]
        public override string Id
        {
            get
            {
                return groupCode;
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

        [StringLength(5)]
        [Key]
        [Required]
        [Column(Order = 1)]
        public string groupCode { get; set; }

        [Required]
        [StringLength(5)]
        public string groupParentCode { get; set; }

        [Required]
        [StringLength(50)]
        public string groupName { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }

    }
}
