using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicInfo.API.Models
{
    public class CartDto
    {
        public int patientId { get; set; }
        public int paymentId { get; set; }
        public string method { get; set; }
        public double amount { get; set; }
    }
}
