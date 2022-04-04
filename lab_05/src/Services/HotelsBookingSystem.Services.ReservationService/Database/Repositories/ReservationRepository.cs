using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.ReservationService.Database.Context;
using HotelsBookingSystem.Services.ReservationService.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using CoreModels=HotelsBookingSystem.Services.ReservationService.Core.Models;
using DbModels=HotelsBookingSystem.Services.ReservationService.Database.Models;

namespace HotelsBookingSystem.Services.ReservationService.Database.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly NpgsqlContext _dbContext;
        
        public ReservationRepository(NpgsqlContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<CoreModels.Reservation>> GetReservationAsync()
        {
            var reservations = await _dbContext.Reservations!
                .AsNoTracking()
                .Select(p => FromCoreReservationToDbConverter.ConvertBack(p)!)
                .ToListAsync();

            return reservations;
        }

        public async Task<CoreModels.Reservation?> FindReservationAsync(Guid reservationUid)
        {
            var dbReservation = await _dbContext.Reservations!
                .AsNoTracking()
                .Include(p => p.Hotel)
                .FirstOrDefaultAsync(p => p.ReservationUid == reservationUid);

            return FromCoreReservationToDbConverter.ConvertBack(dbReservation);
        }
        
        public async Task<List<CoreModels.Reservation>?> FindReservationAsync(string username)
        {
            // var dbReservations = await _dbContext.Reservations!
            //     .AsNoTracking()
            //     .Select(p => FromCoreReservationToDbConverter.ConvertBack(p)!)
            //     .Where(p => p.Username == username)
            //     .ToListAsync();

            var dbReservations = await _dbContext.Reservations!
                .AsNoTracking()
                .Where(p => p.Username == username)
                .Include(p => p.Hotel)
                .ToListAsync();
            
            // var dbReservations = await _dbContext.Reservations!
            //     .AsNoTracking()
            //     // .Select(p => FromCoreReservationToDbConverter.ConvertBack(p)!)
            //     .Include(p => p.Hotel)
            //     .ToListAsync();
            
            // return dbReservations;
            var res = new List<CoreModels.Reservation> { };
            
            foreach (var dbReservation in dbReservations)
            {
                res.Add(FromCoreReservationToDbConverter.ConvertBack(dbReservation)!);
            }
            
            return res;
        }

        public async Task<Guid?> AddReservationAsync(string? username, 
            Guid paymentUid, 
            int? hotelId, 
            string? status, 
            DateTime startDate,
            DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"Parameter {nameof(username)} must be not null or whitespace.");

            if (hotelId == null || hotelId < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(hotelId)}.");

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException($"Parameter {nameof(status)} must be not null or whitespace.");
            
            if (startDate == null)
                throw new ArgumentException($"Trying to assign a null value to {nameof(startDate)}.");
            
            if (endDate == null)
                throw new ArgumentException($"Trying to assign a null value to {nameof(endDate)}.");

            var reservation = new DbModels.Reservation(Guid.NewGuid(), 
                username!,
                paymentUid!,
                (int) hotelId,
                status!,
                startDate!,
                endDate!);

            await _dbContext.Reservations!.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();

            return reservation.ReservationUid;
        }

        public async Task<CoreModels.Reservation?> UpdateReservationAsync(Guid reservationUid, 
            string? username, 
            Guid paymentUid, 
            int? hotelId, 
            string? status, 
            DateTime startDate,
            DateTime endDate)
        {
            if (reservationUid == null)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(reservationUid)}.");

            var reservation = await _dbContext.Reservations!
                .FirstOrDefaultAsync(p => p.ReservationUid == reservationUid);
            
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"Parameter {nameof(username)} must be not null or whitespace.");

            if (hotelId == null || hotelId < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(hotelId)}.");

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException($"Parameter {nameof(status)} must be not null or whitespace.");
            
            if (startDate == null)
                throw new ArgumentException($"Trying to assign a null value to {nameof(startDate)}.");
            
            if (endDate == null)
                throw new ArgumentException($"Trying to assign a null value to {nameof(endDate)}.");

            if (reservation != null)
            {
                reservation.Username = username;
                reservation.PaymentUid = paymentUid;
                reservation.HotelId = (int) hotelId;
                reservation.Status = status;
                reservation.StartDate = startDate;
                reservation.EndDate = endDate;
                
                await _dbContext.SaveChangesAsync();
                return FromCoreReservationToDbConverter.ConvertBack(reservation);
            }
            else
                throw new ArgumentException($"Reservation with Id {reservationUid} does not exist.");
        }

        public async Task<CoreModels.Reservation?> DeleteReservationAsync(Guid reservationUid)
        {
            var reservation = await _dbContext.Reservations!
                .FirstOrDefaultAsync(p => p.ReservationUid == reservationUid);

            if (reservation != null)
            {
                if (reservation.Status == "PAID")
                {
                    reservation.Status = "CANCELED";
                    await _dbContext.SaveChangesAsync();
                    return FromCoreReservationToDbConverter.ConvertBack(reservation);
                }
                else
                    return null;
            }
            else
                throw new ArgumentException($"Reservation with Id {reservationUid} does not exist.");
        }
    }
}