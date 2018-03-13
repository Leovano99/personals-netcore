using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("MS_Document")]
    public class MS_Document : Entity<string>
    {
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id
        {
            get
            {
                return documentType;
            }
            set
            {
            }
        }

        [StringLength(25)]
        [Key]
        [Required]
        public string documentType { get; set; }

        [StringLength(50)]
        public string documentName { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }

        public ICollection<TR_Document> TR_Document { get; set; }
    }
}
