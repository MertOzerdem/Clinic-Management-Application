using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicInfo.API.Models
{
    public class MultiPaymentDto
    {
        public int PaymentId { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
    }
}
