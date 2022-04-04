using System;

namespace HotelsBookingSystem.Services.PaymentService.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        
        public Guid PaymentUid { get; set; }
        
        public string Status { get; set; }
        
        public int Price { get; set; }

        public Payment(int id, Guid paymentUid, string status, int price)
        {
            Id = id;
            PaymentUid = paymentUid;
            Status = status;
            Price = price;
        }
    }
}