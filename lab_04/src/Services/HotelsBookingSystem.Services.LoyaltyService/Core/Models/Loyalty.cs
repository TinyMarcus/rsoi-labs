using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsBookingSystem.Services.LoyaltyService.Core.Models
{
    public class Loyalty
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        
        public int ReservationCount { get; set; }
        
        public string Status { get; set; }
        
        public int Discount { get; set; }

        public Loyalty(int id,
            string username, 
            int reservationCount, 
            string status, 
            int discount)
        {
            Id = id;
            Username = username;
            ReservationCount = reservationCount;
            Status = status;
            Discount = discount;
        }
    }
}