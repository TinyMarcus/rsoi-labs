using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.ReservationService.Dto;
using HotelsBookingSystem.Services.ReservationService.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelsBookingSystem.Services.ReservationService.Controllers
{
    [ApiController]
    [Route("api/v1/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetHotels([FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var allHotels = await _hotelRepository.GetHotelAsync();
                var hotels = allHotels.Skip((page - 1) * size).Take(size).ToList();
                var hotelResponseDtoList = new List<HotelResponseDto> { };
                var result = new List<PaginationResponseDto> { };

                foreach (var hotel in hotels)
                {
                    var hotelResponseDto = new HotelResponseDto(hotel.HotelUid, hotel.Name, hotel.Country,
                        hotel.City, hotel.Address, hotel.Stars, hotel.Price);
                    hotelResponseDtoList.Add(hotelResponseDto);
                }

                var paginationResponseDto =
                    new PaginationResponseDto(page, size, allHotels.Count, hotelResponseDtoList);

                return Ok(paginationResponseDto);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{hotelUid}")]
        public async Task<IActionResult> GetHotels([FromRoute] Guid hotelUid)
        {
            try
            {
                var hotel = await _hotelRepository.FindHotelAsync(hotelUid);

                if (hotel == null)
                    return NotFound();
                else
                {
                    var hotelResponseDtoWithId = new HotelResponseDtoWithId(hotel.Id, hotel.HotelUid,
                        hotel.Name, hotel.Country, hotel.City, hotel.Address, hotel.Stars, hotel.Price);
                    return Ok(hotelResponseDtoWithId);
                }
                    
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}