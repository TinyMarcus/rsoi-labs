using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsBookingSystem.Services.ReservationService.Database.Models
{
    public class Reservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("reservation_uid")]
        public Guid ReservationUid { get; set; }
        
        [Column("username")]
        public string Username { get; set; }
        
        [Column("payment_uid")]
        public Guid PaymentUid { get; set; }
        
        [Column("hotel_id")]
        public int HotelId { get; set; }
        
        [Column("status")]
        public string Status { get; set; }
        
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        
        public Hotel Hotel { get; set; }

        public Reservation(string username, 
            Guid paymentUid, 
            int hotelId, 
            string status, 
            DateTime startDate, 
            DateTime endDate)
        {
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

        public Reservation(Guid reservationUid, 
            string username, 
            Guid paymentUid, 
            int hotelId, 
            string status, 
            DateTime startDate, 
            DateTime endDate)
        {
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