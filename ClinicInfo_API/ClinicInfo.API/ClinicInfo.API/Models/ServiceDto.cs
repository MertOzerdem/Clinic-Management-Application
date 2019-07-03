using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicInfo.API.Models
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }
        public string Provider { get; set; }
        public double ServiceFee { get; set; }
    }
}
