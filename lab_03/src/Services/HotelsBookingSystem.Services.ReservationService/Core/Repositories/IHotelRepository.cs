#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.ReservationService.Core.Models;

namespace HotelsBookingSystem.Services.ReservationService.Core.Repositories
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetHotelAsync();

        Task<Hotel?> FindHotelAsync(Guid hotelUid);
        
        Task<Hotel?> FindHotelAsync(int id);

        Task AddHotelAsync(string? name, 
            string? country, 
            string? city, 
            string? address, 
            int? stars, 
            int? price);
        
        Task<Hotel?> UpdateHotelAsync(Guid hotelUid,
            string? name, 
            string? country, 
            string? city, 
            string? address, 
            int? stars, 
            int? price);

        Task<Hotel?> DeleteHotelAsync(Guid hotelUid);
    }
}