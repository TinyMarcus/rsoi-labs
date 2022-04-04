using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HotelsBookingSystem.GatewayAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelsBookingSystem.GatewayAPI.Controllers
{
    [ApiController]
    [Route("api/v1/loyalty")]
    public class LoyaltyController : ControllerBase
    {
        public LoyaltyController()
        {
            
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetLoyalty([FromHeader (Name = "X-User-Name")][Required] string username)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("username", username);
                    

                    try
                    {
                        var responseLoyalty = await client.GetAsync("http://rsoi-ilyasov-loyalty.herokuapp.com/api/v1/loyalty");
                        // var responseLoyalty = await client.GetAsync("http://localhost:20000/api/v1/loyalty");

                        if (responseLoyalty.StatusCode == HttpStatusCode.NotFound)
                            return NotFound();

                        var responseLoyaltyBody = await responseLoyalty.Content.ReadAsStringAsync();
                        var resultLoyalty =
                            JsonConvert.DeserializeObject<LoyaltyDto>(responseLoyaltyBody);

                        return Ok(resultLoyalty);
                    }
                    catch (Exception e)
                    {
                        return StatusCode(503, new ErrorResponseDto("Loyalty Service unavailable"));
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}