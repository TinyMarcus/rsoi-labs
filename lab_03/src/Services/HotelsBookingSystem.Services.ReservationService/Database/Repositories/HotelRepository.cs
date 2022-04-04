#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.ReservationService.Database;
using CoreModels=HotelsBookingSystem.Services.ReservationService.Core.Models;
using DbModels=HotelsBookingSystem.Services.ReservationService.Database.Models;
using HotelsBookingSystem.Services.ReservationService.Core.Repositories;
using HotelsBookingSystem.Services.ReservationService.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookingSystem.Services.ReservationService.Database.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly NpgsqlContext _dbContext;
        
        public HotelRepository(NpgsqlContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<CoreModels.Hotel>> GetHotelAsync()
        {
            var hotels = await _dbContext.Hotels!
                .AsNoTracking()
                .Select(p => FromCoreHotelToDbConverter.ConvertBack(p)!)
                .ToListAsync();

            return hotels;
        }

        public async Task<CoreModels.Hotel?> FindHotelAsync(Guid hotelUid)
        {
            var dbHotel = await _dbContext.Hotels!
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.HotelUid == hotelUid);

            return FromCoreHotelToDbConverter.ConvertBack(dbHotel);
        }
        
        public async Task<CoreModels.Hotel?> FindHotelAsync(int id)
        {
            var dbHotel = await _dbContext.Hotels!
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return FromCoreHotelToDbConverter.ConvertBack(dbHotel);
        }

        public async Task AddHotelAsync(string? name, 
            string? country, 
            string? city, 
            string? address, 
            int? stars, 
            int? price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Parameter {nameof(name)} must be not null or whitespace.");
            
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException($"Parameter {nameof(country)} must be not null or whitespace.");
            
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException($"Parameter {nameof(city)} must be not null or whitespace.");
            
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException($"Parameter {nameof(address)} must be not null or whitespace.");

            if (stars == null || stars < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(stars)}.");
            
            if (price == null || price < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(price)}.");

            var hotel = new DbModels.Hotel(Guid.NewGuid(),
                name,
                country,
                city,
                address,
                (int) stars,
                (int) price);

            await _dbContext.Hotels!.AddAsync(hotel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CoreModels.Hotel?> UpdateHotelAsync(Guid hotelUid, 
            string? name, 
            string? country, 
            string? city, 
            string? address, 
            int? stars, 
            int? price)
        {
            if (hotelUid == null)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(hotelUid)}.");

            var hotel = await _dbContext.Hotels!
                .FirstOrDefaultAsync(p => p.HotelUid == hotelUid);
            
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Parameter {nameof(name)} must be not null or whitespace.");
            
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException($"Parameter {nameof(country)} must be not null or whitespace.");
            
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException($"Parameter {nameof(city)} must be not null or whitespace.");
            
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException($"Parameter {nameof(address)} must be not null or whitespace.");

            if (stars == null || stars < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(stars)}.");
            
            if (price == null || price < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(price)}.");

            if (hotel != null)
            {
                hotel.Name = name;
                hotel.Country = country;
                hotel.City = city;
                hotel.Address = address;
                hotel.Stars = (int) stars;
                hotel.Price = (int) price;

                await _dbContext.SaveChangesAsync();
                return FromCoreHotelToDbConverter.ConvertBack(hotel);
            }
            else
                throw new ArgumentException($"Hotel with Id {hotelUid} does not exist.");
        }

        public async Task<CoreModels.Hotel?> DeleteHotelAsync(Guid hotelUid)
        {
            var hotel = await _dbContext.Hotels!
                .FirstOrDefaultAsync(p => p.HotelUid == hotelUid);

            if (hotel != null)
            {
                _dbContext.Remove(hotel);
                
                await _dbContext.SaveChangesAsync();
                return FromCoreHotelToDbConverter.ConvertBack(hotel);
            }
            else
                throw new ArgumentException($"Hotel with Id {hotelUid} does not exist.");
        }
    }
}