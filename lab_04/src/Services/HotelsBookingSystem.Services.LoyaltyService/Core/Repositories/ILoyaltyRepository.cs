#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelsBookingSystem.Services.LoyaltyService.Core.Models;

namespace HotelsBookingSystem.Services.LoyaltyService.Core.Repositories
{
    public interface ILoyaltyRepository
    {
        Task<List<Loyalty>> GetLoyaltyAsync();

        Task<Loyalty?> FindLoyaltyAsync(int id);
        
        Task<Loyalty?> FindLoyaltyAsync(string username);

        Task AddLoyaltyAsync(string? username,
            int? reservationCount,
            string? status,
            int? discount);
        
        Task<Loyalty?> UpdateLoyaltyAsync(int id,
            string? username,
            int? reservationCount,
            string? status,
            int? discount);

        Task<Loyalty?> DeleteLoyaltyAsync(int id);
    }
}