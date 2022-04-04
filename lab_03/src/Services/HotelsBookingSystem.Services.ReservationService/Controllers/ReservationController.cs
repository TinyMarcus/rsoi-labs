using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.ReservationService.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using HotelsBookingSystem.Services.ReservationService.Dto;
using HotelInfoDto = HotelsBookingSystem.Services.ReservationService.Dto.HotelInfoDto;
using ReservationResponseDto = HotelsBookingSystem.Services.ReservationService.Dto.ReservationResponseDto;

namespace HotelsBookingSystem.Services.ReservationService.Controllers
{
    [ApiController]
    [Route("api/v1/reservations")]
    public class ReservationCController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IHotelRepository _hotelRepository;

        public ReservationCController(IReservationRepository reservationRepository, IHotelRepository hotelRepository)
        {
            _reservationRepository = reservationRepository;
            _hotelRepository = hotelRepository;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddReservation([FromHeader][Required] string username,
            [FromBody][Required] AddReservationDto reservationDto)
        {
            try
            {
                var hotel = await _hotelRepository.FindHotelAsync((int) reservationDto.HotelId!);
                
                var reservationUid = await _reservationRepository.AddReservationAsync(username, 
                    (Guid) reservationDto.PaymentUid!,
                    hotel!.Id,
                    reservationDto.Status, 
                    (DateTime) reservationDto.StartDate!, 
                    (DateTime) reservationDto.EndDate!);

                return Ok(reservationUid);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete]
        [Route("{reservationUid}")]
        public async Task<IActionResult> DeleteReservation([FromHeader][Required] string username,
            [FromRoute][Required] Guid reservationUid)
        {
            try
            {
                var res = await _reservationRepository.DeleteReservationAsync(reservationUid);
                
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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetReservations([FromHeader][Required] string username)
        {
            try
            {
                var reservations = await _reservationRepository.FindReservationAsync(username);
        
                if (reservations == null)
                    return NotFound();
                
                var reservationsDtoList = new List<ReservationDto> { };
        
                foreach (var reservation in reservations!)
                {
                    var hotelInfoDto = new HotelInfoDto(reservation.Hotel.HotelUid, 
                        reservation.Hotel.Name,
                        reservation.Hotel.Country + ", " + reservation.Hotel.City + ", " + reservation.Hotel.Address, 
                        reservation.Hotel.Stars);
                    var reservationResponseDto = new ReservationDto(reservation.ReservationUid, hotelInfoDto,
                        reservation.StartDate, reservation.EndDate, reservation.Status, null, reservation.PaymentUid);
                    reservationsDtoList.Add(reservationResponseDto);
                }
        
                return Ok(reservationsDtoList);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet]
        [Route("{reservationUid}")]
        public async Task<IActionResult> GetReservationByUidAndUsername([FromRoute][Required] Guid reservationUid,
            [FromHeader][Required] string username)
        {
            try
            {
                var reservation = await _reservationRepository.FindReservationAsync(reservationUid);

                if (reservation == null)
                    return NotFound();
                
                if (reservation.Username != username)
                {
                    return Conflict();
                }
                
                var hotelInfoDto = new HotelInfoDto(reservation.Hotel.HotelUid, 
                    reservation.Hotel.Name,
                    reservation.Hotel.Country + ", " + reservation.Hotel.City + ", " + reservation.Hotel.Address, 
                    reservation.Hotel.Stars);
                var reservationResponseDto = new ReservationDto(reservation.ReservationUid, hotelInfoDto,
                    reservation.StartDate, reservation.EndDate, reservation.Status, null, reservation.PaymentUid);
                
                return Ok(reservationResponseDto);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}