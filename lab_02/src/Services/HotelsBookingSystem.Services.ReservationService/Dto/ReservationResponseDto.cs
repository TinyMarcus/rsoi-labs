using System;
using System.Runtime.Serialization;
using HotelsBookingSystem.Services.ReservationService.Dto;

namespace HotelsBookingSystem.Services.ReservationService.Dto
{
    public class ReservationResponseDto
    {
        [DataMember(Name = "reservationUid", EmitDefaultValue = false)]
        public Guid? ReservationUid { get; set; }
        
        [DataMember(Name = "hotel", EmitDefaultValue = false)]
        public HotelInfoDto? Hotel { get; set; }
        
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public DateTime? StartDate { get; set; }
        
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public DateTime? EndDate { get; set; }
        
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string? Status { get; set; }
        
        [DataMember(Name = "payment", EmitDefaultValue = false)]
        public PaymentDto? Payment { get; set; }

        public ReservationResponseDto(Guid? reservationUid, HotelInfoDto? hotel, DateTime? startDate, DateTime? endDate, string? status, PaymentDto? payment)
        {
            ReservationUid = reservationUid;
            Hotel = hotel;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Payment = payment;
        }
    }

    public class CreateReservationDto
    {
        [DataMember(Name = "reservationUid", EmitDefaultValue = false)]
        public Guid? PaymentUid { get; set; }
        
        [DataMember(Name = "hotel", EmitDefaultValue = false)]
        public int HotelId { get; set; }
        
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string? Status { get; set; }
        
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public DateTime? StartDate { get; set; }
        
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public DateTime? EndDate { get; set; }
    }
    
    public class ReservationDto : ReservationResponseDto
    {
        [DataMember(Name = "paymentUid", EmitDefaultValue = false)]
        public Guid? PaymentUid { get; set; }

        public ReservationDto(Guid? reservationUid, 
            HotelInfoDto? hotel, 
            DateTime? startDate, 
            DateTime? endDate, 
            string? status, 
            PaymentDto? payment,
            Guid? paymentUid) : base(reservationUid, hotel, startDate, endDate, status, payment)
        {
            PaymentUid = paymentUid;
        }
    }
    
    public class AddReservationDto
    {
        [DataMember(Name = "reservationUid", EmitDefaultValue = false)]
        public Guid? PaymentUid { get; set; }
        
        [DataMember(Name = "hotelId", EmitDefaultValue = false)]
        public int? HotelId { get; set; }
        
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public DateTime? StartDate { get; set; }
        
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public DateTime? EndDate { get; set; }
        
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string? Status { get; set; }

        public AddReservationDto(Guid? paymentUid, 
            int? hotelId,
            DateTime? startDate, 
            DateTime? endDate, 
            string? status)
        {
            PaymentUid = paymentUid;
            HotelId = hotelId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }
}