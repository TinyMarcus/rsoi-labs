using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.PaymentService.Core.Repositories;
using HotelsBookingSystem.Services.PaymentService.Dto;
using Microsoft.AspNetCore.Mvc;
using PaymentDto = HotelsBookingSystem.Services.PaymentService.Dto.PaymentDto;

namespace HotelsBookingSystem.Services.PaymentService.Controllers
{
    [ApiController]
    [Route("api/v1/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        [Route("{paymentUid}")]
        public async Task<IActionResult> GetPayment([FromRoute] Guid paymentUid)
        {
            try
            {
                var payment = await _paymentRepository.FindPaymentAsync(paymentUid);

                if (payment == null)
                    return NotFound();
                
                var paymentDto = new PaymentDto(payment!.Status, payment.Price);
                return Ok(paymentDto);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddPayment([FromBody] PaymentDto paymentDto)
        {
            try
            {
                var paymentUid = await _paymentRepository.AddPaymentAsync(paymentDto.Status, paymentDto.Price);

                var paymentDtoWithUid = new PaymentDtoWithUid(paymentUid, paymentDto.Status, paymentDto.Price);

                return CreatedAtAction("AddPayment", paymentDtoWithUid);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete]
        [Route("{paymentUid}")]
        public async Task<IActionResult> DeletePayment([FromRoute][Required] Guid paymentUid)
        {
            try
            {
                var res = await _paymentRepository.DeletePaymentAsync(paymentUid);

                if (res != null)
                    return Ok(res.PaymentUid);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}