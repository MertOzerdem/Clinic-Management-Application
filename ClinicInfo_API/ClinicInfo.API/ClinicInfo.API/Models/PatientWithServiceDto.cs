using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicInfo.API.Entities;

namespace ClinicInfo.API.Models
{
    public class PatientWithServiceDto
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public double Balance { get; set; }
        public ICollection<PatientService> PatientServices { get; set; } = new List<PatientService>();
    }
}
