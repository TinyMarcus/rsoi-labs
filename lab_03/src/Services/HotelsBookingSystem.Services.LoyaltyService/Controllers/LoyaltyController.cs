using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.LoyaltyService.Core.Repositories;
using HotelsBookingSystem.Services.LoyaltyService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HotelsBookingSystem.Services.LoyaltyService.Controllers
{
    [ApiController]
    [Route("api/v1/loyalty")]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyRepository _loyaltyRepository;

        public LoyaltyController(ILoyaltyRepository loyaltyRepository)
        {
            _loyaltyRepository = loyaltyRepository;
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetLoyaltyForUser([FromHeader][Required] string username)
        {
            try
            {
                var loyalty = await _loyaltyRepository.FindLoyaltyAsync(username);

                if (loyalty == null)
                    return NotFound();
                
                var convertedLoyalty = new LoyaltyDto(loyalty!.ReservationCount,
                        loyalty!.Status, 
                        loyalty!.Discount);

                return Ok(convertedLoyalty);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> IncreaseLoyalty([FromHeader][Required] string username)
        {
            try
            {
                var loyalty = await _loyaltyRepository.FindLoyaltyAsync(username);

                if (loyalty == null)
                {
                    await _loyaltyRepository.AddLoyaltyAsync(username, 0, "BRONZE", 5);
                }
                else
                {
                    loyalty.ReservationCount += 1;

                    if (loyalty.ReservationCount >= 10 && loyalty.ReservationCount < 20)
                    {
                        loyalty.Status = "SILVER";
                        loyalty.Discount = 7;
                    }
                    else if (loyalty.ReservationCount >= 20)
                    {
                        loyalty.Status = "GOLD";
                        loyalty.Discount = 10;
                    }

                    await _loyaltyRepository.UpdateLoyaltyAsync(loyalty.Id,
                        loyalty.Username,
                        loyalty.ReservationCount,
                        loyalty.Status,
                        loyalty.Discount);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DecreaseLoyalty([FromHeader][Required] string username)
        {
            try
            {
                var loyalty = await _loyaltyRepository.FindLoyaltyAsync(username);

                if (loyalty == null)
                {
                    return NotFound();
                }
                else
                {
                    loyalty.ReservationCount -= 1;

                    if (loyalty.ReservationCount >= 10 && loyalty.ReservationCount < 20)
                    {
                        loyalty.Status = "SILVER";
                        loyalty.Discount = 7;
                    }
                    else if (loyalty.ReservationCount >= 20)
                    {
                        loyalty.Status = "GOLD";
                        loyalty.Discount = 10;
                    }

                    await _loyaltyRepository.UpdateLoyaltyAsync(loyalty.Id,
                        loyalty.Username,
                        loyalty.ReservationCount,
                        loyalty.Status,
                        loyalty.Discount);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}