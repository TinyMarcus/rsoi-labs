using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsBookingSystem.Services.LoyaltyService.Database.Models
{
    public class Loyalty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("username")]
        public string Username { get; set; }
        
        [Column("reservation_count")]
        public int ReservationCount { get; set; }
        
        [Column("status")]
        public string Status { get; set; }
        
        [Column("discount")]
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
        
        public Loyalty(string username, 
            int reservationCount, 
            string status, 
            int discount)
        {
            Username = username;
            ReservationCount = reservationCount;
            Status = status;
            Discount = discount;
        }
    }
}