#nullable enable
using System;
using System.Runtime.Serialization;

namespace HotelsBookingSystem.GatewayAPI.Dto
{
    public class HotelInfoDto
    {
        [DataMember(Name = "hotelUid", EmitDefaultValue = false)]
        public Guid? HotelUid { get; set; }
        
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string? Name { get; set; }
        
        [DataMember(Name = "fullAddress", EmitDefaultValue = false)]
        public string? FullAddress { get; set; }
        
        [DataMember(Name = "stars", EmitDefaultValue = false)]
        public int? Stars { get; set; }

        public HotelInfoDto()
        {
            
        }

        public HotelInfoDto(Guid? hotelUid, 
            string? name, 
            string? fullAddress, 
            int? stars)
        {
            HotelUid = hotelUid;
            Name = name;
            FullAddress = fullAddress;
            Stars = stars;
        }
    }
}