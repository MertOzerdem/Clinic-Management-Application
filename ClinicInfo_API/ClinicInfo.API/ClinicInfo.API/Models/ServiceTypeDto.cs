using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicInfo.API.Models
{
    public class ServiceTypeDto
    {
        public int ServiceTypeId { get; set; }
        public double Fee { get; set; }
        public string Type { get; set; }
    }
}
