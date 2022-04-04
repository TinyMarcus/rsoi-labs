using System.Collections.Generic;
using HotelsBookingSystem.Services.LoyaltyService.Controllers;
using HotelsBookingSystem.Services.LoyaltyService.Core.Repositories;
using HotelsBookingSystem.Services.LoyaltyService.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using DbModels = HotelsBookingSystem.Services.LoyaltyService.Database.Models;
using CoreModels = HotelsBookingSystem.Services.LoyaltyService.Core.Models;


namespace HotelsBookingSystem.Tests
{
    public class LoyaltyServiceTests
    {
        [Fact]
        public async void GetLoyaltyOfUserOkTest()
        {
            // Arrange
            var loyaltyRepository = new Mock<ILoyaltyRepository>();
            
            var loyaltyDb = new List<DbModels.Loyalty>
            {
                new DbModels.Loyalty(1, "Max", 8, "BRONZE", 5),
                new DbModels.Loyalty(2, "Idris", 15, "SILVER", 7),
            };
            var loyaltyCore = new CoreModels.Loyalty(loyaltyDb[0].Id,
                loyaltyDb[0].Username,
                loyaltyDb[0].ReservationCount,
                loyaltyDb[0].Status,
                loyaltyDb[0].Discount);
            
            loyaltyRepository.Setup(p => p.FindLoyaltyAsync("Max"))
                .ReturnsAsync(loyaltyCore)
                .Verifiable();
            var controller = new LoyaltyController(loyaltyRepository.Object);

            // Act
            var res = await controller.GetLoyaltyForUser("Max");
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(res);
            var response = Assert.IsAssignableFrom<LoyaltyDto>(okResult.Value);
            Assert.Equal(loyaltyCore.Discount, response.Discount);
            Assert.Equal(loyaltyCore.Status, response.Status);
            Assert.Equal(loyaltyCore.ReservationCount, response.ReservationCount);
        }
        
        [Fact]
        public async void GetLoyaltyOfUserNotFoundTest()
        {
            // Arrange
            var loyaltyRepository = new Mock<ILoyaltyRepository>();
            
            var loyaltyDb = new List<DbModels.Loyalty>
            {
                new DbModels.Loyalty(1, "Max", 8, "BRONZE", 5),
                new DbModels.Loyalty(2, "Idris", 15, "SILVER", 7),
            };
            var loyaltyCore = new CoreModels.Loyalty(loyaltyDb[0].Id,
                loyaltyDb[0].Username,
                loyaltyDb[0].ReservationCount,
                loyaltyDb[0].Status,
                loyaltyDb[0].Discount);
            
            loyaltyRepository.Setup(p => p.FindLoyaltyAsync("Max"))
                .ReturnsAsync(loyaltyCore)
                .Verifiable();
            var controller = new LoyaltyController(loyaltyRepository.Object);

            // Act
            var res = await controller.GetLoyaltyForUser("Mem");
            
            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(res);
        }
    }
}