using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicInfo.API.Models
{
    public class PaymentCreatorDto
    {
        public int patientId { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
    }
}
