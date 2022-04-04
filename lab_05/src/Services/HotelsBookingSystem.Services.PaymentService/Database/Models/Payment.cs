using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsBookingSystem.Services.PaymentService.Database.Models
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("payment_uid")]
        public Guid PaymentUid { get; set; }
        
        [Column("status")]
        public string Status { get; set; }
        
        [Column("price")]
        public int Price { get; set; }

        public Payment(int id, 
            Guid paymentUid, 
            string status, 
            int price)
        {
            Id = id;
            PaymentUid = paymentUid;
            Status = status;
            Price = price;
        }

        public Payment(Guid paymentUid,
            string status,
            int price)
        {
            PaymentUid = paymentUid;
            Status = status;
            Price = price;
        }
    }
}