using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("TR_Document")]
    public class TR_Document : Entity<string>
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

        [StringLength(25)]
        [Required]
        [ForeignKey("MS_Document")]
        public string documentType { get; set; }
        public virtual MS_Document MS_Document { get; set; }

        public int? documentRef { get; set; }

        public byte[] DocumentBinary { get; set; }

        public DateTime modifTime { get; set; }

        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        public string inputUN { get; set; }

        [StringLength(10)]
        public string documentPicType { get; set; }
    }
}
