using System;
using System.Runtime.Serialization;

namespace HotelsBookingSystem.GatewayAPI.Dto
{
    public class CreateReservationResponseDto
    {
        [DataMember(Name = "reservationUid", EmitDefaultValue = false)]
        public Guid? ReservationUid { get; set; }
        
        [DataMember(Name = "hotelUid", EmitDefaultValue = false)]
        public Guid? HotelUid { get; set; }
        
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public string StartDate { get; set; }
        
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public string EndDate { get; set; }
        
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string? Status { get; set; }
        
        [DataMember(Name = "discount", EmitDefaultValue = false)]
        public int? Discount { get; set; }
        
        [DataMember(Name = "payment", EmitDefaultValue = false)]
        public PaymentDto? Payment{ get; set; }

        public CreateReservationResponseDto(Guid? reservationUid, 
            Guid? hotelUid, 
            string startDate, 
            string endDate, 
            string? status, 
            int? discount, 
            PaymentDto? payment)
        {
            ReservationUid = reservationUid;
            HotelUid = hotelUid;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Discount = discount;
            Payment = payment;
        }
    }
}