#nullable enable
using CoreModels=HotelsBookingSystem.Services.ReservationService.Core.Models;
using DbModels=HotelsBookingSystem.Services.ReservationService.Database.Models;

namespace HotelsBookingSystem.Services.ReservationService.Database
{
    public class FromCoreHotelToDbConverter
    {
        public static DbModels.Hotel? Convert(CoreModels.Hotel? coreHotel)
        {
            if (coreHotel is null)
                return null;

            return new DbModels.Hotel(coreHotel.Id,
                coreHotel.HotelUid,
                coreHotel.Name,
                coreHotel.Country,
                coreHotel.City,
                coreHotel.Address,
                coreHotel.Stars,
                coreHotel.Price);
        }
        
        public static CoreModels.Hotel? ConvertBack(DbModels.Hotel? dbHotel)
        {
            if (dbHotel is null)
                return null;

            return new CoreModels.Hotel(dbHotel.Id,
                dbHotel.HotelUid,
                dbHotel.Name,
                dbHotel.Country,
                dbHotel.City,
                dbHotel.Address,
                dbHotel.Stars,
                dbHotel.Price);
        }
    }
}