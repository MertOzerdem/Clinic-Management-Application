using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClinicInfo.API.Entities;
using ClinicInfo.API.Models;
using ClinicInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicInfo.API.Controllers
{
    [Route("api/services")]
    public class ServiceController : Controller
    {
        private IPatientInfoRepository _patientInfoRepository;

        public ServiceController(IPatientInfoRepository patientInfoRepository)
        {
            _patientInfoRepository = patientInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetServices()
        {
            var services = _patientInfoRepository.GetAllServices();
            var results = Mapper.Map<List<ServiceDto>>(services);

            return Ok(results);
        }

        [HttpGet("{serviceId}")]
        public IActionResult GetService(int serviceId)
        {
            if (!_patientInfoRepository.isServiceExist(serviceId))
            {
                return NotFound();
            }

            var service = _patientInfoRepository.GetSingleService(serviceId);
            var result = Mapper.Map<ServiceDto>(service);

            return Ok(result);
        }

        [HttpPost("newservice")]
        public IActionResult CreateService([FromBody] ServiceRootDto service)
        {
            if (service == null)
            {
                return BadRequest("cannot be null");
            }

            if (service.provider == null || service.discount < 0 
                || service.provider.Length > 50 || service.provider.Length < 2)
            {
                return BadRequest("cannot appyl negative amount of discount or empty request");
            }

            double totalFee = 0;
            var newService = CreateNewService(service.provider);

            var serviceOrders = new List<ServiceOrder>();
            _patientInfoRepository.AddService(newService);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            foreach (var a in service.providedServices)
            {
                ServiceType _Type = _patientInfoRepository.GetSingleServiceType(a);
                totalFee = totalFee + _Type.Fee;
                _patientInfoRepository.AddServiceOrder(CreateNewServiceOrder(newService, _Type));
            }

            if (totalFee < service.discount)
            {
                return BadRequest("Discount cannot be higher than total service fee.");
            }

            // total service fee assignment
            totalFee = totalFee - service.discount;
            newService.ServiceFee = totalFee;

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            // After service created link with patient
            var targetPatient = _patientInfoRepository.GetPatient(service.patientId);

            // drop serviceFee from patient's balance
            targetPatient.Balance = targetPatient.Balance - totalFee;

            var newPatientService = new PatientService()
            {
                Patient = targetPatient,
                Service = newService
            };

            _patientInfoRepository.AddPatientService(newPatientService);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var result = Mapper.Map<Models.ServiceDto>(newService);

            return Created("http://localhost:59721/api/services/" + result.ServiceId, result);

        }

        public ServiceOrder CreateNewServiceOrder(Service _Service, ServiceType _ServiceType)
        {
            var serviceOrder = new ServiceOrder()
            {
                Service = _Service,
                ServiceType = _ServiceType
            };

            return serviceOrder;
        }

        public Service CreateNewService(string _Provider)
        {
            var service = new Service()
            {
                Provider = _Provider
            };

            return service;
        }

        [HttpGet("{serviceId}/servicetypes")]
        public IActionResult GetMultipleRawServiceTypeData(int serviceId)
        {
            if (!_patientInfoRepository.isServiceExist(serviceId))
            {
                return NotFound();
            }

            var serviceTypes = _patientInfoRepository.GetServiceTypes(serviceId);

            var serviceTypesResult = new List<ServiceTypeDto>();

            foreach (var a in serviceTypes)
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

        [HttpGet("servicetypes")]
        public IActionResult GetServiceTypes()
        {
            var serviceTypes = _patientInfoRepository.GetAllServiceTypes();
            var results = Mapper.Map<List<ServiceTypeDto>>(serviceTypes);

            return Ok(results);
        }

        [HttpGet("servicetypes/{serviceTypeId}")]
        public IActionResult GetServiceType(int serviceTypeId)
        {
            if (!_patientInfoRepository.isServiceTypeExist(serviceTypeId))
            {
                return NotFound();
            }

            var serviceType = _patientInfoRepository.GetSingleServiceType(serviceTypeId);

            var result = Mapper.Map<ServiceType>(serviceType);

            return Ok(result);
        }

        [HttpGet("servicetypes/{serviceTypeId}/relatedservices")]
        public IActionResult GetRelatedServices(int serviceTypeId)
        {
            if (!_patientInfoRepository.isServiceTypeExist(serviceTypeId))
            {
                return NotFound();
            }
            var services = _patientInfoRepository.GetAllRelatedServices(serviceTypeId);

            var results = Mapper.Map<List<ServiceDto>>(services);

            return Ok(results);
        }

        [HttpPost("servicetypes/addservicetype")]
        public IActionResult CreateServiceType([FromBody] ServiceTypeForCreateDto serviceType)
        {
            if (serviceType == null)
            {
                return BadRequest("empty request");
            }

            if (serviceType.Type == null || serviceType.Type.Length > 50 
                || serviceType.Fee < 0 || serviceType.Type.Length < 2)
            {
                return BadRequest("cannot be null");
            }

            var finalServiceType = Mapper.Map<Entities.ServiceType>(serviceType);
            var _Type = finalServiceType.Type;

            if (!_patientInfoRepository.IsServiceTypeValid(_Type))
            {
                return BadRequest("Service Type already exists");
            }
            
            _patientInfoRepository.AddServiceType(finalServiceType);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var result = Mapper.Map<Models.ServiceTypeDto>(finalServiceType);

            return Created("http://localhost:59721/api/services/servicetypes/" + result.ServiceTypeId, result);
        }

        [HttpDelete("servicetypes/{serviceTypeId}")]
        public IActionResult DeleteServiceType(int serviceTypeId)
        {
            if (!_patientInfoRepository.isServiceTypeExist(serviceTypeId))
            {
                return NotFound();
            }

            if (_patientInfoRepository.isServiceTypeInUse(serviceTypeId))
            {
                return BadRequest("This service type is used for some service therefore cannot be removed from system");
            }

            bool s = _patientInfoRepository.isServiceTypeInUse(serviceTypeId);
            // what if city does not exist
            var serviceType = _patientInfoRepository.GetSingleServiceType(serviceTypeId);

            _patientInfoRepository.DeleteServiceType(serviceType);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();

        }

        
    }
}
