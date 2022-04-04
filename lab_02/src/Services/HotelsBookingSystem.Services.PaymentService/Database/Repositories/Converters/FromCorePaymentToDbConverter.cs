#nullable enable
using CoreModels=HotelsBookingSystem.Services.PaymentService.Core.Models;
using DbModels=HotelsBookingSystem.Services.PaymentService.Database.Models;


namespace HotelsBookingSystem.Services.PaymentService.Database.Repositories.Converters
{
    public class FromCorePaymentToDbConverter
    {
        public static DbModels.Payment? Convert(CoreModels.Payment? corePayment)
        {
            if (corePayment is null)
                return null;

            return new DbModels.Payment(corePayment.Id,
                corePayment.PaymentUid,
                corePayment.Status,
                corePayment.Price);
        }
        
        public static CoreModels.Payment? ConvertBack(DbModels.Payment? dbPayment)
        {
            if (dbPayment is null)
                return null;

            return new CoreModels.Payment(dbPayment.Id,
                dbPayment.PaymentUid,
                dbPayment.Status,
                dbPayment.Price);
        }
    }
}