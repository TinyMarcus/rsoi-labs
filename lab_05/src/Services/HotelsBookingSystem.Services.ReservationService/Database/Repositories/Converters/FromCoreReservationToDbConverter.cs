#nullable enable
using CoreModels=HotelsBookingSystem.Services.ReservationService.Core.Models;
using DbModels=HotelsBookingSystem.Services.ReservationService.Database.Models;

namespace HotelsBookingSystem.Services.ReservationService.Database
{
    public class FromCoreReservationToDbConverter
    {
        public static DbModels.Reservation? Convert(CoreModels.Reservation? coreReservation)
        {
            if (coreReservation is null)
                return null;

            return new DbModels.Reservation(coreReservation.Id,
                coreReservation.ReservationUid,
                coreReservation.Username,
                coreReservation.PaymentUid,
                coreReservation.HotelId,
                coreReservation.Status,
                coreReservation.StartDate,
                coreReservation.EndDate,
                FromCoreHotelToDbConverter.Convert(coreReservation.Hotel)!);
        }
        
        public static CoreModels.Reservation? ConvertBack(DbModels.Reservation? dbReservation)
        {
            if (dbReservation is null)
                return null;

            return new CoreModels.Reservation(dbReservation.Id,
                dbReservation.ReservationUid,
                dbReservation.Username,
                dbReservation.PaymentUid,
                dbReservation.HotelId,
                dbReservation.Status,
                dbReservation.StartDate,
                dbReservation.EndDate,
                FromCoreHotelToDbConverter.ConvertBack(dbReservation.Hotel)!);
        }
    }
}