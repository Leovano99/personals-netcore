using System;
namespace VDI.Demo.MasterPlan.Unit.MS_UnitStatuses.Dto
{
    public class CreateMsUnitStatusInput
    {
        public string unitStatusCode { get; set; }

        public string unitStatusName { get; set; }

        public bool isActive { get; set; }
    }
}
