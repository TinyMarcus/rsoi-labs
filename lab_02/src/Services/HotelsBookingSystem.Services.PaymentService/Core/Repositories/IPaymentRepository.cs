#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.PaymentService.Core.Models;

namespace HotelsBookingSystem.Services.PaymentService.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetPaymentAsync();

        Task<Payment?> FindPaymentAsync(Guid paymentUid);

        Task<Guid> AddPaymentAsync(string? status,
            int? price);
        
        Task<Payment?> UpdatePaymentAsync(int id,
            string? status,
            int? price);

        Task<Payment?> DeletePaymentAsync(Guid paymentUid);
    }
}