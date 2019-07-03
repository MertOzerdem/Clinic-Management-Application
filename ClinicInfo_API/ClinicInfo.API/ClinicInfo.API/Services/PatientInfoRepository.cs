using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicInfo.API.Services
{
    public class PatientInfoRepository : IPatientInfoRepository
    {
        private PatientInfoContext _context;
        public PatientInfoRepository(PatientInfoContext context)
        {
            _context = context;
        }

        ///////////////////////////////////////////////
        // ALL PATIENT RELATED INFORMATİON BELOW
        ///////////////////////////////////////////////

        public bool isPatientExist(int _PatientId)
        {
            return _context.Patients.Any(p => p.PatientId == _PatientId);
        }

        // Returns all patients
        public IEnumerable<Patient> GetPatients()
        {
            return _context.Patients.OrderBy(c => c.PatientId).ToList();
        }

        // Returns patient with given index
        public Patient GetPatient(int PatientId)
        {
            return _context.Patients.Where(c => c.PatientId == PatientId).FirstOrDefault();
        }

        // Delete patient with id and all of its links
        public void DeletePatient(int _PatientId)
        {
            // if something is null do not attempt to remove it from database
            var patient = GetPatient(_PatientId);

            var transactions = GetTransactions(_PatientId);
            var payments = GetPayments(_PatientId);

            if (transactions.Any())
            {
                foreach (var transaction in transactions)
                {
                    _context.Transactions.Remove(transaction);
                }
            }
            //_context.SaveChanges();

            if (payments.Any())
            {
                foreach (var payment in payments)
                {
                    _context.Payments.Remove(payment);
                }
            }
            //_context.SaveChanges();

            var patientServices = _context.PatientServices.Where(p => p.Patient.PatientId == _PatientId)
                                    .Include(o => o.Service).ToList();

            var services = GetServices(_PatientId);

            var serviceOrders = new List<ServiceOrder>();

            var notifications = _context.Notifications.Where(p => p.Patient == patient);

            if (services.Any())
            {
                foreach(var service in services)
                {
                    serviceOrders.AddRange(_context.ServiceOrders.Where(p => p.Service == service)
                                        .Include(o=> o.ServiceType).ToList());
                }
            }

            // Remove from database
            if (serviceOrders.Any())
            {
                foreach (var serviceOrder in serviceOrders)
                {
                    _context.ServiceOrders.Remove(serviceOrder);
                }
            }
            //_context.SaveChanges();

            if (patientServices.Any())
            {
                foreach (var patientService in patientServices)
                {
                    _context.PatientServices.Remove(patientService);
                }
            }
            //_context.SaveChanges();

            if (services.Any())
            {
                foreach (var service in services)
                {
                    _context.Services.Remove(service);
                }
            }
            //_context.SaveChanges();

            if (notifications.Any())
            {
                foreach (var notification in notifications)
                {
                    _context.Notifications.Remove(notification);
                }
            }
            //_context.SaveChanges();

            _context.Patients.Remove(patient);
            _context.SaveChanges();

        }

        // Get transaction list with patients's Id
        public List<Transaction> GetTransactions(int Id)
        {
            var patient = _context.Patients.Where(x => x.PatientId == Id).FirstOrDefault();

            var results = new List<Transaction>();
            results = _context.Transactions.Where(p => p.Patient == patient).ToList();

            return results;
        }

        public List<Transaction> GetInclusiveTransactions()
        {
            var transactions = _context.Transactions.Include(p => p.Patient)
                                .Include(o => o.Payment).OrderBy(a => a.Patient.PatientId).ToList();
            return transactions;
        }

        // Returns Patient's payments array from Id
        public List<Payment> GetPayments(int Id)
        {
            //var results = new List<Transaction>();
            var transactions = _context.Transactions.Where(p => p.Patient.PatientId == Id)
                                .Include(o => o.Payment).ToList();

            var payments = new List<Payment>();

            foreach(var a in transactions)
            {
                payments.Add(a.Payment);
            }

            return payments;
        }

        public List<Payment> GetPayments()
        {
            var payments = _context.Payments.OrderBy(p => p.PaymentId).ToList();
            return payments;
        }

        // Retruns Patient's single payment with payment Id and patient Id
        public Payment GetPayment(int _PatientId, int _PaymentId)
        {
            //var payment = _context.Payments.Where(x => x.PaymentId == Id).FirstOrDefault();

            var payment = GetPayments(_PatientId);

            var result = payment.Where(p => p.PaymentId == _PaymentId).FirstOrDefault();

            return result;
        }

        public Payment GetPayment(int _PaymentId)
        {
            var payment = GetPayments().Where(p => p.PaymentId == _PaymentId).FirstOrDefault();
            return payment;
        }

        public Patient GetPatientFromPayment(int _PaymentId)
        {
            var transaction = _context.Transactions.Where(o => o.Payment.PaymentId == _PaymentId)
                                .Include(p => p.Patient).FirstOrDefault();
            return transaction.Patient;
        }

        public void DeletePayment(int _PaymentId)
        {
            var payment = GetPayment(_PaymentId);

            var transaction = _context.Transactions.Where(p => p.Payment == payment).Include(o => o.Patient).FirstOrDefault();

            var patientId = transaction.Patient.PatientId;

            var patient = GetPatient(patientId);

            patient.Balance = patient.Balance - payment.Amount;

            _context.Remove(transaction);
            _context.Remove(payment);

            _context.SaveChanges();
        }

        public void AddPayment(Payment _Payment)
        {
            _context.Payments.Add(_Payment);
        }

        public void AddTransaction(Transaction _Transaction)
        {
            _context.Transactions.Add(_Transaction);
        }

        // Return all services that is provided to the patient
        public List<Service> GetServices(int _PatientId)
        {
            var patientServices = _context.PatientServices.Where(p => p.Patient.PatientId == _PatientId)
                                    .Include(o => o.Service).ToList();

            var services = new List<Service>();

            foreach(var a in patientServices)
            {
                services.Add(a.Service);
            }

            return services;
        }

        public bool isPaymentExist(int _PaymentId)
        {
            return _context.Payments.Any(p => p.PaymentId == _PaymentId);
        }

        // Return single service that is provided to the patient
        public Service GetService(int _PatientId, int _ServiceId)
        {
            var service = GetServices(_PatientId);

            var result = service.Where(o => o.ServiceId == _ServiceId).FirstOrDefault();

            return result;
        }

        // Return all ServiceTypes which service uses
        public List<ServiceType> GetServiceTypes(int _ServiceId)
        {
            var serviceOrders = _context.ServiceOrders.Where(p => p.Service.ServiceId == _ServiceId)
                                .Include(o => o.ServiceType).ToList();

            var results = new List<ServiceType>();

            foreach(var a in serviceOrders)
            {
                results.Add(a.ServiceType);
            }
            return results;
        }

        // Return single ServiceType which service uses
        public ServiceType GetServiceType(int _ServiceId, int _ServiceTypeId)
        {

            var serviceType = GetServiceTypes(_ServiceId);

            var result = serviceType.Where(p => p.ServiceTypeId == _ServiceTypeId).FirstOrDefault();

            return result;
        }

        public void AddPatient(Patient _Patient)
        {
            _context.Patients.Add(_Patient);
        }



        ///////////////////////////////////////////////
        // ALL PATIENT RELATED INFORMATİON ABOVE
        ///////////////////////////////////////////////



        ///////////////////////////////////////////////
        // ALL SERVICE WİTHOUT PATİENT REQUEST HANDLERS
        ///////////////////////////////////////////////

        public List<Service> GetAllServices()
        {
            var results = _context.Services.OrderBy(p => p.ServiceId).ToList();

            return results;
        }

        public Service GetSingleService(int _ServiceId)
        {
            var services = GetAllServices();

            var result = services.Where(p => p.ServiceId == _ServiceId).FirstOrDefault();

            return result;
        }

        public List<Service> GetAllRelatedServices(int _ServiceTypeId)
        {
            var serviceOrders = _context.ServiceOrders.Where(p => p.ServiceType.ServiceTypeId == _ServiceTypeId)
                                    .Include(o => o.Service).ToList();
            var services = new List<Service>();

            foreach (var a in serviceOrders)
            {
                services.Add(a.Service);
            }
            return services;
        }

        public List<ServiceType> GetAllServiceTypes()
        {
            var result = _context.ServiceTypes.OrderBy(o => o.ServiceTypeId).ToList();

            return result;
        }

        public ServiceType GetSingleServiceType(int _ServiceTypeId)
        {
            var serviceTypes = GetAllServiceTypes();

            var result = serviceTypes.Where(p => p.ServiceTypeId == _ServiceTypeId).FirstOrDefault();

            return result;
        }

        public ServiceType GetSingleServiceType(string _Type)
        {
            var serviceTypes = GetAllServiceTypes();

            var result = serviceTypes.Where(p => p.Type == _Type).FirstOrDefault();

            return result;
        }

        public bool isServiceExist(int _serviceId)
        {
            return _context.Services.Any(p => p.ServiceId ==_serviceId);
        }

        public bool IsServiceTypeValid(string _Type)
        {
            var serviceTypes = GetAllServiceTypes();

            foreach(var a in serviceTypes)
            {
                if (_Type == a.Type)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddServiceType(ServiceType _ServiceType)
        {
            _context.ServiceTypes.Add(_ServiceType);
        }

        public void AddService(Service _Service)
        {
            _context.Services.Add(_Service);
        }

        public void AddServiceOrder(ServiceOrder _ServiceOrder)
        {
            _context.ServiceOrders.Add(_ServiceOrder);
        }

        public void AddPatientService(PatientService _PatientService)
        {
            _context.PatientServices.Add(_PatientService);
        }

        ///////////////////////////////////////////////
        // ALL SERVICE WİTHOUT PATİENT REQUEST HANDLERS
        ///////////////////////////////////////////////

        // Returns Notifications
        public List<Notification> GetNotifications(int _PatientId)
        {
            var notify = _context.Notifications.Where(p => p.Patient.PatientId == _PatientId).ToList();

            return notify;
        }

        // Returns single notification with given patient's Id
        public Notification GetNotification(int _PatientId, int _notifyId)
        {
            var notify = _context.Notifications.Where(p => p.Patient.PatientId == _PatientId).ToList();

            var count = 1;
            var targetId = 0;
            foreach (var a in notify)
            {
                if (count == _notifyId)
                {
                    targetId = a.NotificationId;
                    break;
                }
            }

            var result = notify.Where(o => o.NotificationId == targetId).FirstOrDefault();

            return result;
        }

        public void AddNotification(Notification _Notification)
        {
            _context.Notifications.Add(_Notification);
        }

        public bool isServiceTypeExist(int _ServiceTypeId)
        {
            return _context.ServiceTypes.Any(c => c.ServiceTypeId == _ServiceTypeId);
        }

        public bool isServiceTypeInUse(int _ServiceTypeId)
        {
            ServiceType _ServiceType = _context.ServiceTypes.Where(p => p.ServiceTypeId == _ServiceTypeId).FirstOrDefault();
            if (_ServiceType == null) // saçma kod
            {
                return false;
            }
            return _context.ServiceOrders.Any(c => c.ServiceType == _ServiceType);
        }

        public void DeleteServiceType(ServiceType _ServiceType)
        {
            _context.ServiceTypes.Remove(_ServiceType);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
