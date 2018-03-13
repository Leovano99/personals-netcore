using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("PERSONALS")]
    public class Personals : Entity<string>
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
        [Key, Column(Order = 0)]
        public string entityCode { get; set; }


        [StringLength(8)]
        [Key, Column(Order = 1)]
        public string psCode { get; set; }

        [StringLength(8)]
        public string parentPSCode { get; set; }


        [StringLength(100)]
        public string name { get; set; }


        [StringLength(1)]
        public string sex { get; set; }

        public DateTime? birthDate { get; set; }


        [StringLength(30)]
        public string birthPlace { get; set; }

        [StringLength(1)]
        public string marCode { get; set; }


        [StringLength(1)]
        public string relCode { get; set; }


        [StringLength(1)]
        public string bloodCode { get; set; }

        [StringLength(3)]
        public string occID { get; set; }


        [StringLength(3)]
        public string nationID { get; set; }


        [StringLength(1)]
        public string familyStatus { get; set; }


        [StringLength(30)]
        public string NPWP { get; set; }


        [StringLength(2)]
        public string FPTransCode { get; set; }


        [StringLength(1)]
        public string grade { get; set; }


        public bool isActive { get; set; }

        [StringLength(500)]
        public string remarks { get; set; }


        [StringLength(10)]
        public string mailGroup { get; set; }

        public DateTime modifTime { get; set; }

        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        public string inputUN { get; set; }

        [StringLength(50)]
        public string UploadContentType { get; set; }

        [StringLength(100)]
        public string UploadContentImage { get; set; }

        public ICollection<TR_Phone> TR_Phone { get; set; }

        public TR_Email TR_Email { get; set; }

        public TR_Document TR_Document { get; set; }


    }
}
