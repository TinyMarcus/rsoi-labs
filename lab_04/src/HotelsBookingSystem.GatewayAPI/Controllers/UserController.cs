using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HotelsBookingSystem.GatewayAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelsBookingSystem.GatewayAPI.Controllers
{
    [ApiController]
    [Route("api/v1/me")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUserInfo([FromHeader (Name = "X-User-Name")][Required] string username)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("username", username);
                    var responseReservations = await client.GetAsync("http://reservation-service:80/api/v1/reservations");
                    var responseReservationsBody = await responseReservations.Content.ReadAsStringAsync();
                    var resultReservations = JsonConvert.DeserializeObject<List<ReservationDto>>(responseReservationsBody);
                    
                    var responseLoyalty = await client.GetAsync("http://loyalty-service:80/api/v1/loyalty");
                    var responseLoyaltyBody = await responseLoyalty.Content.ReadAsStringAsync();
                    var resultLoyalty = JsonConvert.DeserializeObject<LoyaltyDto>(responseLoyaltyBody);
                    
                    if (resultLoyalty.Status == "404")
                        return NotFound();
                    
                    foreach (var reservation in resultReservations)
                    {
                        var responsePayment = await client.GetAsync("http://payment-service:80/api/v1/payment/" + 
                                                                    reservation.PaymentUid);
                        var responsePaymentBody = await responsePayment.Content.ReadAsStringAsync();
                        var resultPayment = JsonConvert.DeserializeObject<PaymentDto>(responsePaymentBody);

                        reservation.Payment = resultPayment;
                    }

                    var resultReservationsResponse = new List<ReservationResponseDto>() { };

                    for (int i = 0; i < resultReservations.Count; i++)
                    {
                        resultReservationsResponse.Add(resultReservations[i]);
                    }
                    
                    var resArray = new List<object>();
                    
                    foreach (var reservation in resultReservationsResponse)
                    {
                        var res = new
                        {
                            reservationUid = reservation.ReservationUid,
                            hotel = reservation.Hotel,
                            startDate = reservation.StartDate.Value.ToString("yyyy-MM-dd"),
                            endDate = reservation.EndDate.Value.ToString("yyyy-MM-dd"),
                            status = reservation.Status,
                            payment = reservation.Payment
                        };

                        resArray.Add(res);
                    }

                    var jsonResponse = new JsonResult(new
                    {
                        reservations = resArray,
                        loyalty = resultLoyalty
                    });

                    // var userInfoResponseDto = new UserInfoResponseDto(resultReservationsResponse, resultLoyalty);
                    return Ok(jsonResponse.Value);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}