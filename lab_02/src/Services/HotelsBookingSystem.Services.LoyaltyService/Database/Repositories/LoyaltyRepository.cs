#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreModels=HotelsBookingSystem.Services.LoyaltyService.Core.Models;
using DbModels=HotelsBookingSystem.Services.LoyaltyService.Database.Models;
using HotelsBookingSystem.Services.LoyaltyService.Core.Repositories;
using HotelsBookingSystem.Services.LoyaltyService.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookingSystem.Services.LoyaltyService.Database.Repositories
{
    public class LoyaltyRepository : ILoyaltyRepository
    {
        private readonly NpgsqlContext _dbContext;
        
        public LoyaltyRepository(NpgsqlContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<List<CoreModels.Loyalty>> GetLoyaltyAsync()
        {
            var loyalties = await _dbContext.Loyalties!
                .AsNoTracking()
                .Select(p => FromCoreLoyaltyToDbConverter.ConvertBack(p)!)
                .ToListAsync();

            return loyalties;
        }

        public async Task<CoreModels.Loyalty?> FindLoyaltyAsync(int id)
        {
            var dbLoyalty = await _dbContext.Loyalties!
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return FromCoreLoyaltyToDbConverter.ConvertBack(dbLoyalty);
        }
        
        public async Task<CoreModels.Loyalty?> FindLoyaltyAsync(string username)
        {
            var dbLoyalty = await _dbContext.Loyalties!
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Username == username);

            return FromCoreLoyaltyToDbConverter.ConvertBack(dbLoyalty);
        }

        public async Task AddLoyaltyAsync(string? username, int? reservationCount, string? status, int? discount)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"Parameter {nameof(username)} must be not null or whitespace.");
            
            if (reservationCount == null || reservationCount < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(reservationCount)}.");

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException($"Parameter {nameof(status)} must be not null or whitespace.");
            
            if (discount == null || discount < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(discount)}.");

            var loyalty = new DbModels.Loyalty(username!,
                (int) reservationCount,
                status!,
                (int) discount);

            await _dbContext.Loyalties!.AddAsync(loyalty);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CoreModels.Loyalty?> UpdateLoyaltyAsync(int id, string? username, int? reservationCount, string? status, int? discount)
        {
            if (id == null || id < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(id)}.");

            var loyalty = await _dbContext.Loyalties!
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"Parameter {nameof(username)} must be not null or whitespace.");
            
            if (reservationCount == null || reservationCount < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(reservationCount)}.");

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException($"Parameter {nameof(status)} must be not null or whitespace.");
            
            if (discount == null || discount < 0)
                throw new ArgumentException($"Trying to assign a negative int value to {nameof(discount)}.");

            
            if (loyalty != null)
            {
                loyalty.Username = username;
                loyalty.ReservationCount = (int) reservationCount;
                loyalty.Status = status;
                loyalty.Discount = (int) discount;
                
                await _dbContext.SaveChangesAsync();
                return FromCoreLoyaltyToDbConverter.ConvertBack(loyalty);
            }
            else
                throw new ArgumentException($"Loyalty with Id {id} does not exist.");
        }

        public async Task<CoreModels.Loyalty?> DeleteLoyaltyAsync(int id)
        {
            var loyalty = await _dbContext.Loyalties!
                .FirstOrDefaultAsync(p => p.Id == id);

            if (loyalty != null)
            {
                _dbContext.Remove(loyalty);
                
                await _dbContext.SaveChangesAsync();
                return FromCoreLoyaltyToDbConverter.ConvertBack(loyalty);
            }
            else
                throw new ArgumentException($"Loyalty with Id {id} does not exist.");
        }
    }
}