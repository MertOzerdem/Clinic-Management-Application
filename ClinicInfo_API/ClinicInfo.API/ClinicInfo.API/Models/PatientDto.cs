using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicInfo.API.Entities;

namespace ClinicInfo.API.Models
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Balance { get; set; }
        public string Surname { get; set; }

        public ICollection<Payment> transactions { get; set; } = new List<Payment>();

        //public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        //public ICollection<Service> Services { get; set; } = new List<Service>();

        // add here 
        // dept calculated set function
    }
}
