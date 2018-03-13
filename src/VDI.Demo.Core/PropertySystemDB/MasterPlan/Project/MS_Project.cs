using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.MasterPlan.Project
{
    [Table("MS_Project")]
    public class MS_Project : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(5)]
        public string projectCode { get; set; }

        [Required]
        [StringLength(40)]
        public string projectName { get; set; }

        [StringLength(300)]
        public string image { get; set; }

        [StringLength(300)]
        public string webImage { get; set; }

        [Required]
        public bool isPublish { get; set; }

        [Required]
        public short webSort { get; set; }

        [Required]
        [StringLength(200)]
        public string SADManager { get; set; }

        [Required]
        [StringLength(550)]
        public string SADStaff { get; set; }

        [Required]
        [StringLength(500)]
        public string SADContact { get; set; }

        [Required]
        [StringLength(100)]
        public string SADPhone { get; set; }

        [Required]
        [StringLength(100)]
        public string SADFax { get; set; }

        [Required]
        [StringLength(30)]
        public string SADBM { get; set; }

        [Required]
        public bool isConfirmSP { get; set; }
        
        public double? penaltyRate { get; set; }
        
        public int? startPenaltyDay { get; set; }

        [Required]
        public bool isBookingCashier { get; set; }

        [Required]
        public bool isBookingSMS { get; set; }

        [Required]
        public short unitNoGroupLength { get; set; }

        [StringLength(3)]
        public string PROJECT_ID { get; set; }

        [StringLength(2)]
        public string BIZ_UNIT_ID { get; set; }

        [StringLength(5)]
        public string ORG_ID { get; set; }

        [Required]
        [StringLength(3)]
        public string DIV_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string parentProjectName { get; set; }

        [StringLength(5)]
        public string BusinessGroup { get; set; }

        [StringLength(5)]
        public string OperationalGroup { get; set; }

        [StringLength(200)]
        public string TaxGroup { get; set; }

        [StringLength(200)]
        public string SADEmail { get; set; }

        [StringLength(500)]
        public string PGContact { get; set; }

        [StringLength(100)]
        public string PGPhone { get; set; }

        [StringLength(200)]
        public string PGEmail { get; set; }

        [StringLength(200)]
        public string FinanceContact { get; set; }

        [StringLength(200)]
        public string FinanceEmail { get; set; }

        public bool isDMT { get; set; }

        public string DMT_ProjectGroupCode { get; set; }

        public string DMT_ProjectGroupName { get; set; }

        public int? callCenterManagerID { get; set; }

        public int? callCenterStaffID { get; set; }

        public int? bankRelationManagerID { get; set; }

        public int? bankRelationStaffID { get; set; }

        public int? SADBMID { get; set; }

        public int? SADManagerID { get; set; }

        public int? SADStaffID { get; set; }

        public int? PGManagerID { get; set; }

        public int? PGStaffID { get; set; }

        public int? financeManagerID { get; set; }

        public int? financeStaffID { get; set; }

        public int? SADBMStaffID { get; set; }

        public ICollection<MS_Unit> MS_Unit { get; set; }
        public ICollection<MS_MappingFormula> MS_MappingFormula { get; set; }
        public ICollection<MP_OfficerProject> MP_OfficerProject { get; set; }
        public ICollection<MS_ProjectProduct> MS_ProjectProduct { get; set; }
        public ICollection<MP_CompanyProject> MP_CompanyProject { get; set; }
        public ICollection<MS_Term> MS_Term { get; set; }
        public ICollection<MS_Account> MS_Account { get; set; }
        public ICollection<MS_Renovation> MS_Renovation { get; set; }
        public ICollection<MS_UnitTaskList> MS_UnitTaskList { get; set; }
        public ICollection<MS_PriceTaskList> MS_PriceTaskList { get; set; }
        public ICollection<MS_ProjectInfo> MS_ProjectInfo { get; set; }
        public ICollection<MS_PromoOnlineBooking> MS_PromoOnlineBooking { get; set; }
        public ICollection<MS_BankOLBooking> MS_BankOLBooking { get; set; }
        public ICollection<DocNo_Counter> DocNo_Counter { get; set; }
        public ICollection<MS_Cluster> MS_Cluster { get; set; }
        public ICollection<MS_UnitCode> MS_UnitCode { get; set; }
    }
}
