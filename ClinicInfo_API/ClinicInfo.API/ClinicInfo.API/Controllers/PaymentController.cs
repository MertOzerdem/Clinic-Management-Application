using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClinicInfo.API.Entities;
using ClinicInfo.API.Models;
using ClinicInfo.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ClinicInfo.API.Controllers
{
    [Route("api/payments")]
    [EnableCors("CorsPolicy")]
    public class PaymentController : Controller
    {
        private IPatientInfoRepository _patientInfoRepository;

        public PaymentController(IPatientInfoRepository patientInfoRepository)
        {
            _patientInfoRepository = patientInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetAllPayments()
        {
            var payments = _patientInfoRepository.GetPayments();
            var results = Mapper.Map<List<SinglePaymentDto>>(payments);
            return Ok(results);
        }

        [HttpGet("alltransactions")]
        public IActionResult GetTransactions()
        {
            var transactions = _patientInfoRepository.GetInclusiveTransactions();
            var results = new List<CartDto>();

            foreach(var transaction in transactions)
            {
                results.Add(new CartDto
                {
                    patientId = transaction.Patient.PatientId,
                    paymentId = transaction.Payment.PaymentId,
                    amount = transaction.Payment.Amount,
                    method = transaction.Payment.Method
                });
            }

            return Ok(results);
        }

        [HttpGet("{paymentId}")]
        public IActionResult GetSinglePayment(int paymentId)
        {
            if (!_patientInfoRepository.isPaymentExist(paymentId))
            {
                return NotFound();
            }

            var transactions = _patientInfoRepository.GetInclusiveTransactions();
            var payment = _patientInfoRepository.GetPayment(paymentId);

            var result = new CartDto();

            foreach (var transaction in transactions)
            {
                if (payment == transaction.Payment)
                {
                    result.patientId = transaction.Patient.PatientId;
                    result.paymentId = transaction.Payment.PaymentId;
                    result.amount = transaction.Payment.Amount;
                    result.method = transaction.Payment.Method;
                }
            }

            return Ok(result);
        }

        [HttpDelete("{paymentId}")]
        public IActionResult DeletePayment(int paymentId)
        {
            if (!_patientInfoRepository.isPaymentExist(paymentId))
            {
                return NotFound();
            }

            _patientInfoRepository.DeletePayment(paymentId);

            return NoContent();
        }

        [HttpPost("newpayment")]
        public IActionResult CreatePayment([FromBody] PaymentCreatorDto payment)
        {
            if (payment == null)
            {
                return BadRequest("cannot be null");
            }

            if (payment.Amount < 0 || payment.Method == null 
                || payment.Method.Length > 50 || payment.Method.Length < 2)
            {
                return BadRequest("cannot pay negative amount");
            }

            var newPayment = CreateNewPayment(payment);
            _patientInfoRepository.AddPayment(newPayment);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            if (!_patientInfoRepository.isPatientExist(payment.patientId))
            {
                return NotFound();
            }

            var targetPatient = _patientInfoRepository.GetPatient(payment.patientId);
            // patient balance update
            targetPatient.Balance = targetPatient.Balance + payment.Amount;

            var transaction = new Transaction()
            {
                Patient = targetPatient,
                Payment = newPayment
            };

            _patientInfoRepository.AddTransaction(transaction);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var result = Mapper.Map<SinglePaymentDto>(newPayment);

            return Created("http://localhost:59721/api/payments/" + result.PaymentId, result);

        }

        public Payment CreateNewPayment(PaymentCreatorDto _Payment)
        {
            var payment = new Payment()
            {
                Method = _Payment.Method,
                Amount = _Payment.Amount
            };
            return payment;
        }

        [HttpPatch("{paymentId}")]
        [EnableCors("CorsPolicy")]
        public IActionResult PatchPayment(int paymentId, [FromBody] JsonPatchDocument<CartDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("cannot be null");
            }

            if (!_patientInfoRepository.isPaymentExist(paymentId))
            {
                return NotFound();
            }

            var patient = _patientInfoRepository.GetPatientFromPayment(paymentId);
            var payment = _patientInfoRepository.GetPayment(paymentId);
            var firstAmount = payment.Amount;
            var paymentToPatch = Mapper.Map<CartDto>(payment);
            patchDoc.ApplyTo(paymentToPatch, ModelState);

            if (paymentToPatch.method == null || paymentToPatch.amount < 0 
                || paymentToPatch.method.Length > 50 || paymentToPatch.method.Length < 2)
            {
                return BadRequest("extended max limit or negative index");
            }

            TryValidateModel(paymentToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.Map(paymentToPatch, payment);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var lastAmount = payment.Amount;
            // Balance update ( save is required )
            patient.Balance = patient.Balance + (lastAmount - firstAmount);

            if (!_patientInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
