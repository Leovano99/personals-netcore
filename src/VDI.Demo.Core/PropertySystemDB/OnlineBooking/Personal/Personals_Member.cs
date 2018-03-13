using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.Personal
{
    [Table("PERSONALS_MEMBER")]
    public class Personals_Member : Entity<string>
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

        [Key]
        [ForeignKey("Personals")]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [ForeignKey("Personals")]
        [Column(Order = 1)]
        [StringLength(8)]
        public string psCode { get; set; }
        public virtual Personals Personals { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string memberCode { get; set; }


        [StringLength(20)]
        public string parentMemberCode { get; set; }

        [StringLength(1)]
        public string bankType { get; set; }

        [StringLength(5)]
        public string bankCode { get; set; }

        [StringLength(30)]
        public string bankAccNo { get; set; }

        [StringLength(40)]
        public string bankAccName { get; set; }

        [StringLength(50)]
        public string bankBranchName { get; set; }

        [StringLength(1)]
        public string specCode { get; set; }

        [StringLength(12)]
        public string CDCode { get; set; }

        [StringLength(12)]
        public string ACDCode { get; set; }

        [StringLength(50)]
        public string PTName { get; set; }

        [StringLength(50)]
        public string PrincName { get; set; }

        [StringLength(50)]
        public string mothName { get; set; }

        [StringLength(50)]
        public string spouName { get; set; }

        [StringLength(500)]
        public string remarks { get; set; }

        public DateTime? regDate { get; set; }

        public DateTime? joinDate { get; set; }


        [StringLength(20)]
        public string userName { get; set; }


        [StringLength(20)]
        public string password { get; set; }


        [StringLength(200)]
        public string remarks1 { get; set; }


        [StringLength(200)]
        public string remarks2 { get; set; }


        [StringLength(200)]
        public string remarks3 { get; set; }


        [StringLength(5)]
        public string memberStatusCode { get; set; }

        public bool? isCD { get; set; }

        public bool? isACD { get; set; }

        public bool? isActive { get; set; }

        public bool? isInstitusi { get; set; }

        public bool? isPKP { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        public string inputUN { get; set; }

        public bool? isMember { get; set; }

        [StringLength(50)]
        public string franchiseGroup { get; set; }

        public bool? isActiveEmail { get; set; }

        public int? id_role { get; set; }

        //public ICollection<TR_PriorityPass> TR_PriorityPass { get; set; }

    }
}
