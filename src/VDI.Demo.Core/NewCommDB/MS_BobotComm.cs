using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("MS_BobotComm")]
    public class MS_BobotComm : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return projectCode +
                    "-" + clusterCode +
                    "-" + scmCode;
            }
            set { /* nothing */ }
        }
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string projectCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string clusterCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string scmCode { get; set; }

        [Key]
        [Column(Order = 3)]
        public string entityCode { get; set; }

        public double pctBobot { get; set; }

        public bool isActive { get; set; }

        public bool isComplete { get; set; }

        public DateTime? modifTime { get; set; }

        [StringLength(40)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [StringLength(40)]
        public string inputUN { get; set; }
    }
}
