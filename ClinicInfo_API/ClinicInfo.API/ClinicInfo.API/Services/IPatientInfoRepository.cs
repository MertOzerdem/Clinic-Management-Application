using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicInfo.API.Entities;

namespace ClinicInfo.API.Services
{
    public interface IPatientInfoRepository
    {
        //  PATIENT RELATED METHODS
        IEnumerable<Patient> GetPatients();
        Patient GetPatient(int PatientId);
        List<Transaction> GetTransactions(int Id);
        List<Notification> GetNotifications(int _PatientId);
        Notification GetNotification(int _PatientId, int _notifyId);
        List<Service> GetServices(int _PatientId);
        Service GetService(int _PatientId, int _ServiceId);
        List<ServiceType> GetServiceTypes(int _ServiceId);
        ServiceType GetServiceType(int _ServiceId, int _ServiceTypeId);
        void AddPatient(Patient _Patient);
        void DeletePatient(int _PatientId);
        bool isPatientExist(int _PatientId);
        //List<ServiceType> GetServiceTypesFromPatient(int _PatientId, int _ServiceId);

        // SERVICE RELATED METHODS
        List<ServiceType> GetAllServiceTypes();
        ServiceType GetSingleServiceType(int _ServiceTypeId);
        ServiceType GetSingleServiceType(string _Type);
        bool IsServiceTypeValid(string _Type);
        void AddServiceType(ServiceType serviceType);
        List<Service> GetAllServices();
        Service GetSingleService(int _ServiceId);
        void AddServiceOrder(ServiceOrder _ServiceOrder);
        void AddService(Service _Service);
        void AddPatientService(PatientService _PatientService);
        void DeleteServiceType(ServiceType _ServiceType);
        bool isServiceTypeExist(int _ServiceTypeId);
        bool isServiceTypeInUse(int _ServiceTypeId);
        List<Service> GetAllRelatedServices(int _ServiceTypeId);
        bool isServiceExist(int _serviceId);

        // PAYMENT RELATED METHODS
        List<Payment> GetPayments();
        List<Payment> GetPayments(int Id);
        Payment GetPayment(int _PatientId, int _PaymentId);
        Payment GetPayment(int _PaymentId);
        void AddPayment(Payment _Payment);
        void AddTransaction(Transaction _Transaction);
        List<Transaction> GetInclusiveTransactions();
        void DeletePayment(int _PaymentId);
        Patient GetPatientFromPayment(int _PaymentId);
        bool isPaymentExist(int _PaymentId);

        // NOTIFICATION RELATED METHODS
        void AddNotification(Notification _Notification);


        bool Save();
    }
}
