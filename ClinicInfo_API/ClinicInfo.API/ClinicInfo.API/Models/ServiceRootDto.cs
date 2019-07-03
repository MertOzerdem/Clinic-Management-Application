using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicInfo.API.Models
{
    public class ServiceRootDto
    {
        public int discount { get; set; }
        public int patientId { get; set; }
        public string provider { get; set; }
        public List<string> providedServices { get; set; }
    }
}
