using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsBookingSystem.Services.ReservationService.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public Guid ReservationUid { get; set; }
        
        public string Username { get; set; }
        
        public Guid PaymentUid { get; set; }
        
        public int HotelId { get; set; }
        
        public string Status { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public Hotel Hotel { get; set; }

        public Reservation(int id, 
            Guid reservationUid, 
            string username, 
            Guid paymentUid, 
            int hotelId, 
            string status, 
            DateTime startDate, 
            DateTime endDate)
        {
            Id = id;
            ReservationUid = reservationUid;
            Username = username;
            PaymentUid = paymentUid;
            HotelId = hotelId;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Reservation(int id, 
            Guid reservationUid, 
            string username, 
            Guid paymentUid, 
            int hotelId,
            string status, 
            DateTime startDate, 
            DateTime endDate, 
            Hotel hotel)
        {
            Id = id;
            ReservationUid = reservationUid;
            Username = username;
            PaymentUid = paymentUid;
            HotelId = hotelId;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            Hotel = hotel;
        }
    }
}