using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("LK_IDType")]
    public class LK_IDType : Entity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotMapped]
        public override string Id
        {
            get
            {
                return idType;
            }
            set
            {
            }
        }

        [StringLength(1)]
        [Key]
        [Required]
        public string idType { get; set; }

        [Required]
        [StringLength(8)]
        public string idTypeName { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }

        public ICollection<TR_ID> TR_ID { get; set; }

        public ICollection<TR_IDFamily> TR_IDFamily { get; set; }

    }
}
