using CoreModels=HotelsBookingSystem.Services.LoyaltyService.Core.Models;

namespace HotelsBookingSystem.Services.LoyaltyService.Dto
{
    public class FromCoreLoyaltyToDtoConverter
    {
        public static LoyaltyDto Convert(CoreModels.Loyalty coreLoyalty)
        {
            return new LoyaltyDto()
            {
                ReservationCount = coreLoyalty.ReservationCount,
                Status = coreLoyalty.Status,
                Discount = coreLoyalty.Discount
            };
        }
    }
}