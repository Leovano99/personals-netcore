using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("MS_Scheme")]
    public class MS_Scheme : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return scmCode;
            }
            set
            {
            }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Required]
        [StringLength(50)]
        public string scmName { get; set; }

        [Required]
        [StringLength(2)]
        public string digitMemberCode { get; set; }

        public bool isPointCalc { get; set; }

        public bool isFix { get; set; }

        public bool isOverRiding { get; set; }

        public bool isCommHold { get; set; }

        public bool isAcc { get; set; }

        public int accPeriod { get; set; }

        [Required]
        [StringLength(4)]
        public string accPeriodType { get; set; }

        public bool isCapacity { get; set; }

        public bool isTeam { get; set; }

        public bool isHaveCD { get; set; }

        public bool isHaveACD { get; set; }

        public bool isCDGetComm { get; set; }

        public bool isFixCD { get; set; }

        public bool isAccCD { get; set; }

        public int accCDPeriod { get; set; }

        [StringLength(4)]
        public string accCDPeriodType { get; set; }

        public bool isACDGetComm { get; set; }

        public bool isFixACD { get; set; }

        public bool isAccACD { get; set; }

        public int accACDPeriod { get; set; }

        [StringLength(4)]
        public string accACDPeriodType { get; set; }

        public bool isClub { get; set; }

        public bool isActive { get; set; }

        public bool isBudget { get; set; }

        public bool isAutomaticMemberStatus { get; set; }

        [Required]
        public bool isSendSMSPaid { get; set; }

        [Column("modifTime")]
        public override DateTime? LastModificationTime { get; set; }

        [Column("modifUN")]
        public override long? LastModifierUserId { get; set; }

        [Column("inputTime")]
        public override DateTime CreationTime { get; set; }

        [Column("inputUN")]
        public override long? CreatorUserId { get; set; }

        //public ICollection<TR_BookingHeader> TR_BookingHeader { get; set; }
    }
}
