using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicInfo.API.Models;
using ClinicInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ClinicInfo.API.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace ClinicInfo.API.Controllers
{
    [Route("api/patients")]
    public class PatientsController : Controller
    {
        private IPatientInfoRepository _patientInfoRepository;

        public PatientsController(IPatientInfoRepository patientInfoRepository)
        {
            _patientInfoRepository = patientInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetPatients()
        {
            var patientEntities = _patientInfoRepository.GetPatients();
            var results = new List<PatientSimpleDto>();

            foreach (var patientEntity in patientEntities)
            {
                results.Add(new PatientSimpleDto
                {
                    PatientId = patientEntity.PatientId,
                    Name = patientEntity.Name,
                    Surname = patientEntity.Surname,
                    Image = patientEntity.Image,
                    Balance = patientEntity.Balance
                });
            }
            return Ok(results);
        }

        [HttpGet("{patientId}")]
        public IActionResult GetPatient(int patientId)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            var patientEntity = _patientInfoRepository.GetPatient(patientId);
            var result = new PatientSimpleDto()
            {
                PatientId = patientEntity.PatientId,
                Name = patientEntity.Name,
                Surname = patientEntity.Surname,
                Image = patientEntity.Image,
                Balance = patientEntity.Balance
            };

            return Ok(result);
        }

        [HttpDelete("{patientId}")]
        public IActionResult DeletePatient(int patientId)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            _patientInfoRepository.DeletePatient(patientId);

            return NoContent();
        }

        [HttpPatch("{patientId}")]
        public IActionResult PatchPatient(int patientId, [FromBody] JsonPatchDocument<PatientSimpleDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("cannot be null");
            }

            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            var patientEntity = _patientInfoRepository.GetPatient(patientId);
            var patientToPatch = Mapper.Map<PatientSimpleDto>(patientEntity);
            patchDoc.ApplyTo(patientToPatch, ModelState);

            if (patientToPatch.Name.Length > 50 || patientToPatch.Surname.Length > 50 
                || patientToPatch.Name == null || patientToPatch.Surname == null)
            {
                return BadRequest("extended max limit");
            }

            TryValidateModel(patientToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.Map(patientToPatch, patientEntity);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpPost("patientadd")]
        public IActionResult CreateNewPatient([FromBody] PatientCreatorDto patient)
        {
            if (patient == null)
            {
                return BadRequest("cannot be null");
            }

            if ( patient.Name.Length > 50 || patient.Surname.Length > 50
                || patient.Name == null || patient.Surname == null
                || patient.Name.Length < 2 || patient.Surname.Length < 2)
            {
                return BadRequest("extended max limit");
            }

            var newPatient = Mapper.Map<Entities.Patient>(patient);
            newPatient.Balance = 0;
            newPatient.Image = "";

            _patientInfoRepository.AddPatient(newPatient);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var result = Mapper.Map<Models.PatientSimpleDto>(newPatient);

            return Created("http://localhost:59721/api/patients/" + result.PatientId, result);
        }

        [HttpGet("{patientId}/payments/{paymentId}")]
        public IActionResult GetSinglePayment(int patientId, int paymentId)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            if (!_patientInfoRepository.isPaymentExist(paymentId))
            {
                return NotFound();
            }

            var payment = _patientInfoRepository.GetPayment(patientId, paymentId);

            var result = new SinglePaymentDto()
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                Method = payment.Method
            };

            return Ok(result);

        }

        [HttpGet("{patientId}/payments")]
        public IActionResult GetAllPayments(int patientId)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            var payments = _patientInfoRepository.GetPayments(patientId);

            var paymentsResult = new List<MultiPaymentDto>();

            foreach (var a in payments)
            {
                paymentsResult.Add(new MultiPaymentDto()
                {
                    PaymentId = a.PaymentId,
                    Amount = a.Amount,
                    Method = a.Method
                });
            }

            return Ok(paymentsResult);
        }

        [HttpPost("{patientId}/newnotification")]
        public IActionResult CreateNotification(int patientId, [FromBody] NotificationCreatorDto notification)
        {
            if (notification == null)
            {
                return BadRequest("cannot be null");
            }

            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return BadRequest("already exist");
            }

            if (notification.Text == null || notification.Text.Length > 200)
            {
                return BadRequest("extended max limit");
            }

            var targetPatient = _patientInfoRepository.GetPatient(patientId);
            var newNotification = CreateNewNotification(targetPatient, notification);

            _patientInfoRepository.AddNotification(newNotification);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var result = Mapper.Map<Models.NotificationDto>(newNotification);

            return Created("http://localhost:59721/api/patients/" + patientId + "/notification/" + result.NotificationId, result);
        }

        public Notification CreateNewNotification(Patient _Patient, NotificationCreatorDto _Notification)
        {
            var notification = new Notification()
            {
                Patient = _Patient,
                Text = _Notification.Text
            };

            return notification;
        }

        [HttpGet("{patientId}/notification")]
        public IActionResult GetNotifications(int patientId)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            var notify = _patientInfoRepository.GetNotifications(patientId);

            var notifyResult = new List<NotificationDto>();

            foreach(var a in notify)
            {
                notifyResult.Add(new NotificationDto()
                {
                    NotificationId = a.NotificationId,
                    Text = a.Text
                    //Date
                });
            }

            return Ok(notifyResult);
        }

        [HttpGet("{patientId}/notification/{notifyId}")]
        public IActionResult GetNotification(int patientId, int notifyId)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            // add notify check later

            var notify = _patientInfoRepository.GetNotification(patientId, notifyId);

            var result = new NotificationDto()
            {
                NotificationId = notify.NotificationId,
                Text = notify.Text
            };

            return Ok(result);

        }

        [HttpPatch("{patientId}/notification/{notifyId}")]
        public IActionResult PatchNotification(int patientId, int notifyId, [FromBody] JsonPatchDocument<NotificationCreatorDto> patchDoc)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            if(patchDoc == null)
            {
                return BadRequest("cannot be null");
            }

            // add notify check later

            var notification = _patientInfoRepository.GetNotification(patientId, notifyId);
            var notificationToPatch = Mapper.Map<NotificationCreatorDto>(notification);
            patchDoc.ApplyTo(notificationToPatch, ModelState);

            if (notificationToPatch.Text == null || notificationToPatch.Text.Length > 200)
            {
                return BadRequest("extended max limit");
            }

            TryValidateModel(notificationToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.Map(notificationToPatch, notification);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
        
        [HttpGet("{patientId}/services")]
        public IActionResult GetServices(int patientId)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            var services = _patientInfoRepository.GetServices(patientId);

            var servicesResult = new List<ServiceDto>();

            foreach(var a in services)
            {
                servicesResult.Add(new ServiceDto()
                {
                    ServiceId = a.ServiceId,
                    Provider = a.Provider,
                    ServiceFee = a.ServiceFee
                });
            }

            return Ok(servicesResult);
        }

        [HttpGet("{patientId}/services/{serviceId}")]
        public IActionResult GetService(int patientId, int serviceId)
        {
            if (!_patientInfoRepository.isPatientExist(patientId))
            {
                return NotFound();
            }

            if (!_patientInfoRepository.isServiceExist(serviceId))
            {
                return NotFound();
            }

            var service = _patientInfoRepository.GetService(patientId, serviceId);

            var serviceResult = new ServiceDto()
            {
                ServiceId = service.ServiceId,
                Provider = service.Provider
            };

            return Ok(serviceResult);
        }

        // Use this as later frontEnd type generator
        [HttpGet("servicetypes")]
        public IActionResult GetServiceTypes()
        {
            var serviceType = _patientInfoRepository.GetAllServiceTypes();

            var serviceTypeReturn = new List<AllServiceTypesDto>();

            foreach (var a in serviceType)
            {
                serviceTypeReturn.Add(new AllServiceTypesDto()
                {
                    Type = a.Type
                });
            }

            return Ok(serviceTypeReturn);
        }

        [HttpGet("services/{serviceId}/servicetypes")]
        public IActionResult GetMultipleRawServiceTypeData(int serviceId)
        {
            if (!_patientInfoRepository.isServiceExist(serviceId))
            {
                return NotFound();
            }

            var serviceTypes = _patientInfoRepository.GetServiceTypes(serviceId);

            var serviceTypesResult = new List<ServiceTypeDto>();

            foreach(var a in serviceTypes)
            {
                serviceTypesResult.Add(new ServiceTypeDto()
                {
                    ServiceTypeId = a.ServiceTypeId,
                    Fee = a.Fee,
                    Type = a.Type
                });
            }

            return Ok(serviceTypesResult);
        }

        [HttpGet("services/{serviceId}/servicetypes/{servicetypeId}")]
        public IActionResult GetSingleRawServiceTypeData(int serviceId, int servicetypeId)
        {
            if (!_patientInfoRepository.isServiceExist(serviceId))
            {
                return NotFound();
            }

            if (!_patientInfoRepository.isServiceTypeExist(servicetypeId))
            {
                return NotFound();
            }

            var serviceType = _patientInfoRepository.GetServiceType(serviceId, servicetypeId);

            var serviceTypeResult = new ServiceTypeDto()
            {
                ServiceTypeId = serviceType.ServiceTypeId,
                Fee = serviceType.Fee,
                Type = serviceType.Type
            };

            return Ok(serviceTypeResult);
        }

    }
}