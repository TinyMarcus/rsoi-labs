using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HotelsBookingSystem.GatewayAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HotelsBookingSystem.GatewayAPI.Controllers
{
    [ApiController]
    [Route("api/v1/reservations")]
    public class ReservationController : ControllerBase
    {
        public ReservationController()
        {
            
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> BookHotel([FromHeader (Name = "X-User-Name")][Required] string username, 
            [FromBody] CreateReservationRequestDto createReservationRequestDto)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("username", username);
                    
                    // Check if hotel exists
                    var responseHotel = await client.GetAsync("http://rsoi-ilyasov-reservation.herokuapp.com/api/v1/hotels/" +
                                                              createReservationRequestDto.HotelUid);
                    if (responseHotel.StatusCode == HttpStatusCode.NotFound)
                        return BadRequest();
                    
                    var responseHotelBody = await responseHotel.Content.ReadAsStringAsync();
                    var resultHotel =
                        JsonConvert.DeserializeObject<HotelResponseDtoWithId>(responseHotelBody);
                    
                    // Count number of nights and price
                    var interval = createReservationRequestDto.EndDate - createReservationRequestDto.StartDate;
                    var nightsCount = interval!.Value.Days;
                    var price = resultHotel.Price * nightsCount;
                    
                    // Get discount value and status
                    var responseLoyalty = await client.GetAsync("http://rsoi-ilyasov-loyalty.herokuapp.com/api/v1/loyalty");
                    if (responseLoyalty.StatusCode == HttpStatusCode.NotFound)
                        await client.PostAsync("http://rsoi-ilyasov-loyalty.herokuapp.com/api/v1/loyalty", null);

                    // Count price
                    var responseLoyaltyBody = await responseLoyalty.Content.ReadAsStringAsync();
                    var resultLoyalty =
                        JsonConvert.DeserializeObject<LoyaltyDto>(responseLoyaltyBody);
                    if (responseLoyalty.StatusCode == HttpStatusCode.NotFound)
                        resultLoyalty.Discount = 5;
                    price = price * (100 - resultLoyalty.Discount) / 100;

                    // Create payment new record
                    HttpRequestMessage requestMessage = new HttpRequestMessage();
                    requestMessage.Content = JsonContent.Create(new {
                        Status = "PAID",
                        Price = price });
                    var responsePayment = await client.PostAsync("http://rsoi-ilyasov-payment.herokuapp.com/api/v1/payment", 
                        requestMessage.Content);
                    var responsePaymentBody = await responsePayment.Content.ReadAsStringAsync();
                    var resultPaymentWithUid =
                        JsonConvert.DeserializeObject<PaymentDtoWithUid>(responsePaymentBody);
                    var resultPayment = new PaymentDto(resultPaymentWithUid.Status, resultPaymentWithUid.Price);


                    // requestMessage.Content = JsonContent.Create(new { HotelUid = createReservationRequestDto.HotelUid, 
                    //     StartDate = createReservationRequestDto.StartDate,
                    //     EndDate = createReservationRequestDto.EndDate });
                    
                    // Increment count of bookings in loyalty 
                    var responseLoyaltyPost = await client.PostAsync("http://rsoi-ilyasov-loyalty.herokuapp.com/api/v1/loyalty", 
                        null);
                    var responseLoyaltyPostBody = await responseLoyaltyPost.Content.ReadAsStringAsync();
                    var resultLoyaltyPost =
                        JsonConvert.DeserializeObject<LoyaltyDto>(responseLoyaltyPostBody);
                    
                    
                    // Create new record of reservation
                    var reservationDto = JsonContent.Create(new
                    {
                        PaymentUid = resultPaymentWithUid.PaymentUid,
                        HotelId = resultHotel.HotelId,
                        Status = resultPaymentWithUid.Status,
                        StartDate = createReservationRequestDto.StartDate.Value,
                        EndDate = createReservationRequestDto.EndDate.Value,
                    });
                    
                    var responseReservationPost = await client.PostAsync("http://rsoi-ilyasov-reservation.herokuapp.com/api/v1/reservations", 
                        reservationDto);
                    var responseReservationPostBody = await responseReservationPost.Content.ReadAsStringAsync();
                    var resultReservationPost =
                        JsonConvert.DeserializeObject<Guid?>(responseReservationPostBody);

                    var createReservationResponseDto = new CreateReservationResponseDto(resultReservationPost,
                        createReservationRequestDto.HotelUid,
                        createReservationRequestDto.StartDate.Value.ToString("yyyy-MM-dd"),
                        createReservationRequestDto.EndDate.Value.ToString("yyyy-MM-dd"),
                        resultPayment.Status,
                        resultLoyalty.Discount,
                        resultPayment);

                    return Ok(createReservationResponseDto);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete]
        [Route("{reservationUid}")]
        public async Task<IActionResult> UnbookHotel([FromHeader (Name = "X-User-Name")][Required] string username, 
            [FromRoute][Required] Guid reservationUid)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("username", username);
                    
                    // Check if reservation exists
                    var responseReservationGet = await client.GetAsync("http://rsoi-ilyasov-reservation.herokuapp.com/api/v1/reservations/" +
                                                                    reservationUid);

                    if (responseReservationGet.StatusCode == HttpStatusCode.NotFound || 
                        responseReservationGet.StatusCode == HttpStatusCode.Conflict)
                        return NotFound();

                    // Set reservation as CANCELED
                    var responseReservationDel = await client.DeleteAsync("http://rsoi-ilyasov-reservation.herokuapp.com/api/v1/reservations/" +
                                                                             reservationUid);
                    
                    if (responseReservationDel.StatusCode == HttpStatusCode.NotFound)
                        return NotFound();
                    var responseReservationDelBody = responseReservationDel.Content.ReadAsStringAsync().Result;
                    var paymentUid = JsonConvert.DeserializeObject<Guid?>(responseReservationDelBody);

                    
                    // Set payment as CANCELED
                    var responsePaymentDel = await client.DeleteAsync("http://rsoi-ilyasov-payment.herokuapp.com/api/v1/payment/" +
                                                                        paymentUid);
                    
                    if (responsePaymentDel.StatusCode == HttpStatusCode.NotFound)
                        return NotFound();
                    var responsePaymentDelBody = responseReservationDel.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<Guid?>(responseReservationDelBody);

                    
                    // Decrease loyalty
                    var responseLoyaltyDel = await client.DeleteAsync("http://rsoi-ilyasov-loyalty.herokuapp.com/api/v1/loyalty/");
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetReservationsOfUser([FromHeader (Name = "X-User-Name")][Required] string username)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("username", username);
                    var responseReservations = await client.GetAsync("http://rsoi-ilyasov-reservation.herokuapp.com/api/v1/reservations");

                    if (responseReservations.StatusCode == HttpStatusCode.NotFound)
                        return NotFound();
                    
                    var responseReservationsBody = await responseReservations.Content.ReadAsStringAsync();
                    var resultReservations =
                        JsonConvert.DeserializeObject<List<ReservationDto>>(responseReservationsBody);

                    foreach (var reservation in resultReservations)
                    {
                        var responsePayment = await client.GetAsync("http://rsoi-ilyasov-payment.herokuapp.com/api/v1/payment/" + 
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
                    
                    return Ok(resArray);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet]
        [Route("{reservationUid}")]
        public async Task<IActionResult> GetReservation([FromHeader (Name = "X-User-Name")][Required] string username, 
            [FromRoute][Required] Guid reservationUid)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("username", username);
                    var responseReservation = await client.GetAsync("http://rsoi-ilyasov-reservation.herokuapp.com/api/v1/reservations/" +
                                                                     reservationUid);

                    if (responseReservation.StatusCode == HttpStatusCode.Conflict || 
                        responseReservation.StatusCode == HttpStatusCode.NotFound)
                        return Conflict();
                    
                    var responseReservationBody = await responseReservation.Content.ReadAsStringAsync();
                    var resultReservation =
                        JsonConvert.DeserializeObject<ReservationDto>(responseReservationBody);

                    
                    var responsePayment = await client.GetAsync("http://rsoi-ilyasov-payment.herokuapp.com/api/v1/payment/" + 
                                                                resultReservation.PaymentUid);
                    var responsePaymentBody = await responsePayment.Content.ReadAsStringAsync();
                    var resultPayment = JsonConvert.DeserializeObject<PaymentDto>(responsePaymentBody);

                    resultReservation.Payment = resultPayment;

                    var resultReservationResponse = new ReservationResponseDto(resultReservation.ReservationUid,
                        resultReservation.Hotel, 
                        resultReservation.StartDate.Value.Date, 
                        resultReservation.EndDate.Value.Date,
                        resultReservation.Status, 
                        resultReservation.Payment);

                    var res = new JsonResult(new
                    {
                        reservationUid = resultReservation.ReservationUid,
                        hotel = resultReservation.Hotel,
                        startDate = resultReservation.StartDate.Value.ToString("yyyy-MM-dd"),
                        endDate = resultReservation.EndDate.Value.ToString("yyyy-MM-dd"),
                        status = resultReservation.Status,
                        payment = resultReservation.Payment
                    });
                    
                    return Ok(res.Value);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}