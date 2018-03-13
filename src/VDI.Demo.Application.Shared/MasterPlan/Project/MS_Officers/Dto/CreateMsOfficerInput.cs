using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
//using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.MasterPlan.Project.MS_Officers.Dto
{
    //[AutoMapFrom(typeof(MS_Officer))]
    public class CreateMsOfficerInput : EntityDto
    {
        public int? id { get; set; }
        [Required]
        [MaxLength(25)]
        public string officerName { get; set; }

        [Required]
        [MaxLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        public string handphone { get; set; }

        public int positionID { get; set; }
        public int departmentID { get; set; }
        public Boolean isActive { get; set; }
    }
}
