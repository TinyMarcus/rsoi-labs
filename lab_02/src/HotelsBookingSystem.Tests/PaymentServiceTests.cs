using System;
using System.Collections.Generic;
using HotelsBookingSystem.Services.PaymentService.Controllers;
using HotelsBookingSystem.Services.PaymentService.Core.Repositories;
using HotelsBookingSystem.Services.PaymentService.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using DbModels = HotelsBookingSystem.Services.PaymentService.Database.Models;
using CoreModels = HotelsBookingSystem.Services.PaymentService.Core.Models;


namespace HotelsBookingSystem.Tests
{
    public class PaymentServiceTests
    {
        [Fact]
        public async void GetPaymentOkTest()
        {
            // Arrange
            var paymentRepository = new Mock<IPaymentRepository>();
            
            var paymentDb = new List<DbModels.Payment>
            {
                new DbModels.Payment(1, 
                    Guid.Parse("164a5474-e8d9-42d6-bc35-618af1a1155b"), 
                    "PAID", 
                    20000),
            };
            var paymentCore = new CoreModels.Payment(paymentDb[0].Id,
                paymentDb[0].PaymentUid,
                paymentDb[0].Status,
                paymentDb[0].Price);

            paymentRepository.Setup(p => p.FindPaymentAsync(paymentCore.PaymentUid))
                .ReturnsAsync(paymentCore)
                .Verifiable();
            var controller = new PaymentController(paymentRepository.Object);

            // Act
            var res = await controller.GetPayment(Guid.Parse("164a5474-e8d9-42d6-bc35-618af1a1155b"));
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(res);
            var response = Assert.IsAssignableFrom<PaymentDto>(okResult.Value);
            Assert.Equal(paymentCore.Status, response.Status);
            Assert.Equal(paymentCore.Price, response.Price);
        }
        
        [Fact]
        public async void GetPaymentNotFoundTest()
        {
            // Arrange
            var paymentRepository = new Mock<IPaymentRepository>();
            
            var paymentDb = new List<DbModels.Payment>
            {
                new DbModels.Payment(1, 
                    Guid.Parse("164a5474-e8d9-42d6-bc35-618af1a1155b"), 
                    "PAID", 
                    20000),
            };
            var paymentCore = new CoreModels.Payment(paymentDb[0].Id,
                paymentDb[0].PaymentUid,
                paymentDb[0].Status,
                paymentDb[0].Price);

            paymentRepository.Setup(p => p.FindPaymentAsync(paymentCore.PaymentUid))
                .ReturnsAsync(paymentCore)
                .Verifiable();
            var controller = new PaymentController(paymentRepository.Object);

            // Act
            var res = await controller.GetPayment(Guid.Parse("134a5474-e8d9-42d6-bc35-618af1a1155b"));
            
            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(res);
        }
    }
}