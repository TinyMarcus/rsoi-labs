using System;
using System.Runtime.Serialization;

namespace HotelsBookingSystem.GatewayAPI.Dto
{
    public class CreateReservationRequestDto
    {
        [DataMember(Name = "hotelUid", EmitDefaultValue = false)]
        public Guid? HotelUid { get; set; }
        
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public DateTime? StartDate { get; set; }
        
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public DateTime? EndDate { get; set; }

        public CreateReservationRequestDto(Guid? hotelUid, 
            DateTime? startDate, 
            DateTime? endDate)
        {
            HotelUid = hotelUid;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}