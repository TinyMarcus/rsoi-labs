using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.ReservationService.Core.Models;

namespace HotelsBookingSystem.Services.ReservationService.Core.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservationAsync();

        Task<Reservation?> FindReservationAsync(Guid reservationUid);
        
        Task<List<Reservation>?> FindReservationAsync(string username);

        Task<Guid?> AddReservationAsync(string? username,
            Guid paymentUid,
            int? hotelId,
            string? status,
            DateTime startDate,
            DateTime endDate);
        
        Task<Reservation?> UpdateReservationAsync(Guid reservationUid,
            string? username,
            Guid paymentUid,
            int? hotelId,
            string? status,
            DateTime startDate,
            DateTime endDate);

        Task<Reservation?> DeleteReservationAsync(Guid reservationUid);
    }
}