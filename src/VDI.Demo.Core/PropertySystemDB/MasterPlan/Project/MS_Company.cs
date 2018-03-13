using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Company")]
    public class MS_Company : AuditedEntity
    {
        public int entityID { get; set; }

        //unique
        [Required]
        [StringLength(5)]
        public string coCode { get; set; }

        [Required]
        [StringLength(100)]
        public string coName { get; set; }

        [Required]
        [StringLength(500)]
        public string address { get; set; }

        [Required]
        [StringLength(20)]
        public string city { get; set; }

        [Required]
        [StringLength(30)]
        public string NPWP { get; set; }

        [Required]
        [StringLength(30)]
        public string PKP { get; set; }

        public DateTime PKPDate { get; set; }

        public string image { get; set; }

        [Required]
        [StringLength(50)]
        public string accountNo { get; set; }

        [Required]
        [StringLength(50)]
        public string bankName { get; set; }

        [Required]
        [StringLength(50)]
        public string bankBranch { get; set; }

        [Required]
        [StringLength(200)]
        public string mailAddress { get; set; }

        [Required]
        [StringLength(30)]
        public string phoneNo { get; set; }

        [Required]
        [StringLength(30)]
        public string faxNo { get; set; }

        [ForeignKey("MS_PostCode")]
        public int postCodeID { get; set; }
        public virtual MS_PostCode MS_PostCode { get; set; }

        [StringLength(50)]
        public string leasingManager { get; set; }

        [StringLength(50)]
        public string centerManager { get; set; }

        [Required]
        [StringLength(300)]
        public string NPWPAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string KPP_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string KPP_TTD { get; set; }

        [Required]
        [StringLength(5)]
        public string coCodeParent { get; set; }

        [Required]
        [StringLength(20)]
        public string APServer { get; set; }

        [Required]
        [StringLength(6)]
        public string APcoCode { get; set; }

        [Required]
        [StringLength(6)]
        public string APLogin { get; set; }

        public bool isCA { get; set; }

        public bool isActive { get; set; }

        [StringLength(5)]
        public string ORG_ID { get; set; }

        [StringLength(10)]
        public string PPATK_PBJ_code { get; set; }

        [StringLength(100)]
        public string website { get; set; }

        [StringLength(100)]
        public string workHour { get; set; }

        public string companyEmail { get; set; }

        public string country { get; set; }

        public ICollection<MP_CompanyProject> MP_CompanyProject { get; set; }

        public ICollection<MS_Account> MS_Account { get; set; }
        public ICollection<DocNo_Counter> DocNo_Counter { get; set; }
    }
}
