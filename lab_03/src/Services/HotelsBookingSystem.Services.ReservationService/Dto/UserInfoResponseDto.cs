using System.Collections.Generic;
using System.Runtime.Serialization;
using HotelsBookingSystem.Services.ReservationService.Dto;

namespace HotelsBookingSystem.Services.ReservationService.Dto
{
    public class UserInfoResponseDto
    {
        [DataMember(Name = "reservations", EmitDefaultValue = false)]
        public List<ReservationResponseDto>? Reservations { get; set; }
        
        [DataMember(Name = "loyalty", EmitDefaultValue = false)]
        public LoyaltyDto? Loyalty { get; set; }

        public UserInfoResponseDto(List<ReservationResponseDto>? reservations, 
            LoyaltyDto? loyalty)
        {
            Reservations = reservations;
            Loyalty = loyalty;
        }
    }
}