using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_ReminderLetter")]
    public partial class TR_ReminderLetter : AuditedEntity
    {
        public int entityID { get; set; }

        [ForeignKey("TR_BookingHeader")]
        public int bookingHeaderID { get; set; }
        public virtual TR_BookingHeader TR_BookingHeader { get; set; }

        [Required]
        [StringLength(25)]
        public string letterNo { get; set; }

        public DateTime letterDate { get; set; }

        [Required]
        [StringLength(3)]
        public string letterType { get; set; }

        public DateTime dueDate { get; set; }

        [Column(TypeName = "money")]
        public decimal totAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal payedAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal outAmt { get; set; }

        [Column(TypeName = "money")]
        public decimal overDue { get; set; }

        public int penAge { get; set; }

        [Column(TypeName = "money")]
        public decimal penAmt { get; set; }

        [ForeignKey("LK_LetterStatus")]
        public int letterStatusID { get; set; }
        public virtual LK_LetterStatus LK_LetterStatus { get; set; }

        [Required]
        [StringLength(150)]
        public string remarks { get; set; }

        public DateTime mailDate { get; set; }

        public DateTime receiveDate { get; set; }

        public DateTime clearDate { get; set; }

        public DateTime printDate { get; set; }

        [Required]
        [StringLength(5)]
        public string bankBranchCode { get; set; }

        [Required]
        [StringLength(5)]
        public string coCode { get; set; }

        [Required]
        [StringLength(20)]
        public string bankRekNo { get; set; }

        [Required]
        [StringLength(50)]
        public string sadOfficer1 { get; set; }

        [Required]
        [StringLength(50)]
        public string sadPosition1 { get; set; }

        [Required]
        [StringLength(50)]
        public string sadOfficer2 { get; set; }

        [Required]
        [StringLength(50)]
        public string sadPosition2 { get; set; }
        
    }
}
