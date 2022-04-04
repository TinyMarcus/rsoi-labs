using System;
using System.Collections.Generic;
using HotelsBookingSystem.Services.ReservationService.Controllers;
using HotelsBookingSystem.Services.ReservationService.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using DbModels = HotelsBookingSystem.Services.ReservationService.Database.Models;
using CoreModels = HotelsBookingSystem.Services.ReservationService.Core.Models;
using HotelResponseDtoWithId = HotelsBookingSystem.Services.ReservationService.Dto.HotelResponseDtoWithId;


namespace HotelsBookingSystem.Tests
{
    public class ReservationServiceTests
    {
        [Fact]
        public async void GetHotelOkTest()
        {
            // Arrange
            var hotelRepository = new Mock<IHotelRepository>();
            
            var hotelDb = new List<DbModels.Hotel>
            {
                new DbModels.Hotel(1, Guid.Parse("164a5474-e8d9-42d6-bc35-618af1a1155b"), 
                    "Max", "Russia", "Moscow", "Kek St., 8", 5, 10000)
            };
            
            var hotelCore = new CoreModels.Hotel(hotelDb[0].Id,
                hotelDb[0].HotelUid,
                hotelDb[0].Name,
                hotelDb[0].Country,
                hotelDb[0].City,
                hotelDb[0].Address,
                hotelDb[0].Stars,
                hotelDb[0].Price);

            hotelRepository.Setup(p => p.FindHotelAsync(hotelCore.HotelUid))
                .ReturnsAsync(hotelCore)
                .Verifiable();
            var controller = new HotelController(hotelRepository.Object);

            // Act
            var res = await controller.GetHotels(Guid.Parse("164a5474-e8d9-42d6-bc35-618af1a1155b"));
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(res);
            var response = Assert.IsAssignableFrom<HotelResponseDtoWithId>(okResult.Value);
            Assert.Equal(hotelCore.Id, response.HotelId);
            Assert.Equal(hotelCore.HotelUid, response.HotelUid);
            Assert.Equal(hotelCore.Name, response.Name);
            Assert.Equal(hotelCore.Country, response.Country);
            Assert.Equal(hotelCore.City, response.City);
            Assert.Equal(hotelCore.Address, response.Address);
            Assert.Equal(hotelCore.Price, response.Price);
            Assert.Equal(hotelCore.Stars, response.Stars);
        }
        
        [Fact]
        public async void GetHotelNotFoundTest()
        {
            // Arrange
            var hotelRepository = new Mock<IHotelRepository>();
            
            var hotelDb = new List<DbModels.Hotel>
            {
                new DbModels.Hotel(1, Guid.Parse("164a5474-e8d9-42d6-bc35-618af1a1155b"), 
                    "Max", "Russia", "Moscow", "Kek St., 8", 5, 10000)
            };
            
            var hotelCore = new CoreModels.Hotel(hotelDb[0].Id,
                hotelDb[0].HotelUid,
                hotelDb[0].Name,
                hotelDb[0].Country,
                hotelDb[0].City,
                hotelDb[0].Address,
                hotelDb[0].Stars,
                hotelDb[0].Price);

            hotelRepository.Setup(p => p.FindHotelAsync(hotelCore.HotelUid))
                .ReturnsAsync(hotelCore)
                .Verifiable();
            var controller = new HotelController(hotelRepository.Object);

            // Act
            var res = await controller.GetHotels(Guid.Parse("164a5474-e8d9-42d6-bc35-618af1a1155c"));
            
            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(res);
        }
    }
}