using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electric.API.Dtos.Bills
{
    public class EditBillDto
    {
        public bool Paid { get; set; }
    }
}