using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using HotelsBookingSystem.GatewayAPI.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("")]
        public async Task<IActionResult> GetLoyalty()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var username = User.FindFirst("name")?.Value;
                    client.DefaultRequestHeaders.Add("username", username);
                    var responseLoyalty = await client.GetAsync("http://loyalty-service:80/api/v1/loyalty");

                    if (responseLoyalty.StatusCode == HttpStatusCode.NotFound)
                        return NotFound();
                    
                    var responseLoyaltyBody = await responseLoyalty.Content.ReadAsStringAsync();
                    var resultLoyalty =
                        JsonConvert.DeserializeObject<LoyaltyDto>(responseLoyaltyBody);

                    return Ok(resultLoyalty);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}