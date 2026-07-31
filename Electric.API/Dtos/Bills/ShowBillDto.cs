using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electric.API.Dtos.Meters;

namespace Electric.API.Dtos.Bills
{
    public class ShowBillDto : BillDto
    {
        public MeterDto? MeterInfo { get; set; }

        public int Consumption { get; set; }
    }
}