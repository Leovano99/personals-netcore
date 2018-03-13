using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class UpdateTRUnitReserved
    {
        public string psCode { get; set; }

        public List<UnitTrUnitReservedDto> unit { get; set; }
    }

    public class UnitTrUnitReservedDto
    {
        public int unitID { get; set; }
    }
}
