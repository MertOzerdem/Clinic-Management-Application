using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicInfo.API.Entities;

namespace ClinicInfo.API
{
    public static class PatientInfoExtensions
    {
        public static void EnsureSeedDataForContext(this PatientInfoContext context)
        {
            if (context.Patients.Any())
            {
                return;
            }

            // PATIENTS
            var patients = new List<Patient>()
            {
                new Patient()
                {
                    Name = "Mahmut",
                    Surname = "Altan",
                    Image = "PlaceHolder.png",
                    Balance = 0,

                },
                new Patient()
                {
                    Name = "Burhan",
                    Surname = "Altıntop",
                    Image = "PlaceHolder.png",
                    Balance = 10000.123,
                }
            };
            var pat1 = patients.First(item => item.Name == "Mahmut");
            var pat2 = patients.First(item => item.Name == "Burhan");

            //var patients = new Patient
            //{
            //    Name = "ahmet",
            //    Surname = "sokole",
            //    Image = "placeholder.png",
            //    Balance = 0
            //};

            // PAYMENTS  
            var payments = new List<Payment>()
            {
                new Payment()
                {
                    Method = "Visa",
                    Amount = 10,
                    //Date = ""
                },
                new Payment()
                {
                    Method = "MasterCard",
                    Amount = 213000,
                    //Date = ""
                }
            };
            var pay1 = payments.First(item => item.Method == "Visa");
            var pay2 = payments.First(item => item.Method == "MasterCard");

            //var payments = new Payment
            //{
            //    Method = "Visa",
            //    Amount = 1768670
            //};


            // TRANSACTİONS
            var transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Patient = pat1,
                    Payment = pay1
                },
                new Transaction()
                {
                    Patient = pat1,
                    Payment = pay2
                }
            };

            //  SERVİCES
            var services = new List<Service>()
            {
                new Service()
                {
                    ServiceFee = 123123,
                    Provider = "Monster inc."
                },

                new Service()
                {
                    ServiceFee = 0,
                    Provider = "Abuzer comp."
                }

            };
            var ser1 = services.First(item => item.Provider == "Monster inc.");
            var ser2 = services.First(item => item.Provider == "Abuzer comp.");

            //  SERVİCETYPES
            var servicetypes = new List<ServiceType>()
            {
                new ServiceType()
                {
                    Fee = 2139.921,
                    Type = "General health care"
                },
                new ServiceType()
                {
                    Fee = 0,
                    Type = "Checkup"
                },
                new ServiceType()
                {
                    Fee = 0,
                    Type = "Not Gonna Happen"
                }
            };
            var sertype1 = servicetypes.First(item => item.Type == "Checkup");
            var sertype2 = servicetypes.First(item => item.Type == "General health care");
            var sertype3 = servicetypes.First(item => item.Type == "Not Gonna Happen");

            // SERVİCEORDERS
            var serviceorders = new List<ServiceOrder>()
            {
                new ServiceOrder()
                {
                    Service = ser1,
                    ServiceType = sertype1
                },
                new ServiceOrder()
                {
                    Service = ser1,
                    ServiceType = sertype2
                },
                new ServiceOrder()
                {
                    Service = ser2,
                    ServiceType = sertype3
                }
            };

            // PATİENTSERVİCES
            var patientservices = new List<PatientService>()
            {
                new PatientService()
                {
                    Patient = pat1,
                    Service = ser1
                },
                new PatientService()
                {
                    Patient = pat1,
                    Service = ser2
                }
            };

            // NOTİFİCATİONS
            var notifications = new List<Notification>()
            {
                new Notification()
                {
                    Text = "Rise and shine Mr.Freeman",
                    Patient = pat2
                    //Date
                },
                new Notification()
                {
                    Text = "Wake Up",
                    Patient = pat2
                    //Date
                }
            };

            context.Payments.AddRange(payments);
            context.Patients.AddRange(patients);
            context.Transactions.AddRange(transactions);
            context.Services.AddRange(services);
            context.ServiceTypes.AddRange(servicetypes);
            context.ServiceOrders.AddRange(serviceorders);
            context.PatientServices.AddRange(patientservices);
            context.Notifications.AddRange(notifications);
            context.SaveChanges();
        }

    }
}
