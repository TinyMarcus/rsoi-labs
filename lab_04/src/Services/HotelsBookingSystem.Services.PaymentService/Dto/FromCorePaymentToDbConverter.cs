using CoreModels=HotelsBookingSystem.Services.PaymentService.Core.Models;

namespace HotelsBookingSystem.Services.PaymentService.Dto
{
    public class FromCorePaymentToDbConverter
    {
        public static PaymentDto Convert(CoreModels.Payment corePayment)
        {
            return new PaymentDto()
            {
                Status = corePayment.Status,
                Price = corePayment.Price
            };
        }
    }
}