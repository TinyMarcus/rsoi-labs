#nullable enable
using System.Runtime.Serialization;

namespace HotelsBookingSystem.GatewayAPI.Dto
{
    public class LoyaltyDto
    {
        [DataMember(Name = "reservation_count", EmitDefaultValue = false)]
        public int? ReservationCount { get; set; }
        
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string? Status { get; set; }
        
        [DataMember(Name = "discount", EmitDefaultValue = false)]
        public int? Discount { get; set; }

        public LoyaltyDto()
        {
            
        }
        
        public LoyaltyDto(int? reservationCount,
            string? status,
            int? discount)
        {
            ReservationCount = reservationCount;
            Status = status;
            Discount = discount;
        }
    }
}