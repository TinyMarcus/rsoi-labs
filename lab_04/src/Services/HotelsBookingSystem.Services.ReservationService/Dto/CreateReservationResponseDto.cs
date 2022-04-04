using System;
using System.Runtime.Serialization;
using HotelsBookingSystem.Services.ReservationService.Dto;

namespace HotelsBookingSystem.Services.ReservationService.Dto
{
    public class CreateReservationResponseDto
    {
        [DataMember(Name = "reservationUid", EmitDefaultValue = false)]
        public Guid? ReservationUid { get; set; }
        
        [DataMember(Name = "hotelUid", EmitDefaultValue = false)]
        public Guid? HotelUid { get; set; }
        
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public DateTime? StartDate { get; set; }
        
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public DateTime? EndDate { get; set; }
        
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string? Status { get; set; }
        
        [DataMember(Name = "discount", EmitDefaultValue = false)]
        public int? Discount { get; set; }
        
        [DataMember(Name = "payment", EmitDefaultValue = false)]
        public PaymentDto? PaymentDto { get; set; }

        public CreateReservationResponseDto(Guid? reservationUid, 
            Guid? hotelUid, 
            DateTime? startDate, 
            DateTime? endDate, 
            string? status, 
            int? discount, 
            PaymentDto? paymentDto)
        {
            ReservationUid = reservationUid;
            HotelUid = hotelUid;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Discount = discount;
            PaymentDto = paymentDto;
        }
    }
}