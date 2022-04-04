using System.Runtime.Serialization;

namespace HotelsBookingSystem.Services.ReservationService.Dto
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
}