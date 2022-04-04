using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HotelsBookingSystem.GatewayAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// http://localhost:6060 -- Loyalty
// http://localhost:7070 -- Payment
// http://localhost:8080 -- Reservation

namespace HotelsBookingSystem.GatewayAPI.Controllers
{
    [ApiController]
    [Route("api/v1/hotels")]
    public class HotelController : ControllerBase
    {
        public HotelController()
        {
            
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetHotels([FromQuery] int page, [FromQuery] int size=9999)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responsePagination = await client.GetAsync("http://reservation-service:80/api/v1/hotels?" +
                                                                     "page=" + page + "&size=" + size);
                    responsePagination.EnsureSuccessStatusCode();
                    var responsePaginationBody = await responsePagination.Content.ReadAsStringAsync();
                    var resultPagination =
                        JsonConvert.DeserializeObject<PaginationResponseDto>(responsePaginationBody);

                    return Ok(resultPagination);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}