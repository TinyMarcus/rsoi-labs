#nullable enable
using CoreModels=HotelsBookingSystem.Services.LoyaltyService.Core.Models;
using DbModels=HotelsBookingSystem.Services.LoyaltyService.Database.Models;

namespace HotelsBookingSystem.Services.LoyaltyService.Database
{
    public class FromCoreLoyaltyToDbConverter
    {
        public static DbModels.Loyalty? Convert(CoreModels.Loyalty? coreLoyalty)
        {
            if (coreLoyalty is null)
                return null;

            return new DbModels.Loyalty(coreLoyalty.Id,
                coreLoyalty.Username,
                coreLoyalty.ReservationCount,
                coreLoyalty.Status,
                coreLoyalty.Discount);
        }
        
        public static CoreModels.Loyalty? ConvertBack(DbModels.Loyalty? dbLoyalty)
        {
            if (dbLoyalty is null)
                return null;

            return new CoreModels.Loyalty(dbLoyalty.Id,
                dbLoyalty.Username,
                dbLoyalty.ReservationCount,
                dbLoyalty.Status,
                dbLoyalty.Discount);
        }
    }
}