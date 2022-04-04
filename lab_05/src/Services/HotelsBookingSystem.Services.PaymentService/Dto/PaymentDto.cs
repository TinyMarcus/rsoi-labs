using System;
using System.Runtime.Serialization;

namespace HotelsBookingSystem.Services.PaymentService.Dto
{
    [DataContract(Name = "payment")]
    public class PaymentDto
    {
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string? Status { get; set; }
        
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public int? Price { get; set; }

        public PaymentDto()
        {
            
        }
        
        public PaymentDto(string? status,
            int? price)
        {
            Status = status;
            Price = price;
        }
    }
    
    public class PaymentDtoWithUid : PaymentDto
    {
        [DataMember(Name = "paymentUid", EmitDefaultValue = false)]
        public Guid? PaymentUid { get; set; }
        
        public PaymentDtoWithUid()
        {
            
        }
        
        public PaymentDtoWithUid(Guid? paymentUid,
            string? status,
            int? price) : base(status, price)
        {
            PaymentUid = paymentUid;
            Status = status;
            Price = price;
        }
    }
}