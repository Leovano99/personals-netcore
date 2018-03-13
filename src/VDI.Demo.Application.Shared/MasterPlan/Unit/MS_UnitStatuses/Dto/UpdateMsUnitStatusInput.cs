using System;
namespace VDI.Demo.MasterPlan.Unit.MS_UnitStatuses.Dto
{
    public class UpdateMsUnitStatusInput
    {
        public int Id { get; set; }
        public string unitStatusCode { get; set; }

        public string unitStatusName { get; set; }

        public bool isActive { get; set; }
    }
}
