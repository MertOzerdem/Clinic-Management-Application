using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicInfo.API.Models
{
    public class ServiceCreatorDto
    {
        public int patientId { get; set; }
        public string provider { get; set; }
        public int discount { get; set; }
        public List<TypeDto> providedServices { get; set; }
    }
}
