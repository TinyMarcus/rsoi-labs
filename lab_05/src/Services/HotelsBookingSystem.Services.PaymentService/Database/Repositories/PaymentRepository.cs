using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.LoyaltyService.Database.Context;
using CoreModels=HotelsBookingSystem.Services.PaymentService.Core.Models;
using DbModels=HotelsBookingSystem.Services.PaymentService.Database.Models;
using HotelsBookingSystem.Services.PaymentService.Core.Repositories;
using HotelsBookingSystem.Services.PaymentService.Database.Repositories.Converters;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookingSystem.Services.PaymentService.Database.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly NpgsqlContext _dbContext;
        
        public PaymentRepository(NpgsqlContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<CoreModels.Payment>> GetPaymentAsync()
        {
            var payments = await _dbContext.Payments!
                .AsNoTracking()
                .Select(p => FromCorePaymentToDbConverter.ConvertBack(p)!)
                .ToListAsync();

            return payments;
        }

        public async Task<CoreModels.Payment?> FindPaymentAsync(Guid paymentUid)
        {
            var dbPayment = await _dbContext.Payments!
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PaymentUid == paymentUid);

            return FromCorePaymentToDbConverter.ConvertBack(dbPayment);
        }

        public async Task<Guid> AddPaymentAsync(string? status, int? price)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException($"Parameter {nameof(status)} must be not null or whitespace.");
            
            if (price == null || price < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(price)}.");

            var payment = new DbModels.Payment(Guid.NewGuid(),
                status,
                (int) price);

            await _dbContext.Payments!.AddAsync(payment);
            await _dbContext.SaveChangesAsync();

            return payment.PaymentUid;
        }

        public async Task<CoreModels.Payment?> UpdatePaymentAsync(int id, string? status, int? price)
        {
            if (id == null || id < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(id)}.");

            var payment = await _dbContext.Payments!
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException($"Parameter {nameof(status)} must be not null or whitespace.");
            
            if (price == null || price < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(price)}.");

            if (payment != null)
            {
                payment.Status = status;
                payment.Price = (int) price;

                await _dbContext.SaveChangesAsync();
                return FromCorePaymentToDbConverter.ConvertBack(payment);
            }
            else
                throw new ArgumentException($"Payment with Id {id} does not exist.");
        }

        public async Task<CoreModels.Payment?> DeletePaymentAsync(Guid paymentUid)
        {
            var payment = await _dbContext.Payments!
                .FirstOrDefaultAsync(p => p.PaymentUid == paymentUid);

            if (payment != null)
            {
                if (payment.Status == "PAID")
                {
                    payment.Status = "CANCELED";
                    await _dbContext.SaveChangesAsync();
                    return FromCorePaymentToDbConverter.ConvertBack(payment);
                }
                else
                    return null;
            }
            else
                throw new ArgumentException($"Reservation with Id {paymentUid} does not exist.");
        }
    }
}