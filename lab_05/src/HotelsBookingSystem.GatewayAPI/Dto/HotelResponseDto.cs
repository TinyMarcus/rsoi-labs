using System;
using System.Runtime.Serialization;

namespace HotelsBookingSystem.GatewayAPI.Dto
{
    public class HotelResponseDto
    {
        [DataMember(Name = "hotelUid", EmitDefaultValue = false)]
        public Guid? HotelUid { get; set; }
        
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string? Name { get; set; }
        
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string? Country { get; set; }
        
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string? City { get; set; }
        
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string? Address { get; set; }
        
        [DataMember(Name = "stars", EmitDefaultValue = false)]
        public int? Stars { get; set; }
        
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public int? Price { get; set; }

        public HotelResponseDto(Guid? hotelUid, 
            string? name, 
            string? country, 
            string? city, 
            string? address, 
            int? stars, 
            int? price)
        {
            HotelUid = hotelUid;
            Name = name;
            Country = country;
            City = city;
            Address = address;
            Stars = stars;
            Price = price;
        }
    }
    
    public class HotelResponseDtoWithId : HotelResponseDto
    {
        [DataMember(Name = "hotelId", EmitDefaultValue = false)]
        public int? HotelId { get; set; }

        public HotelResponseDtoWithId(int hotelId,
            Guid? hotelUid, 
            string? name, 
            string? country, 
            string? city, 
            string? address, 
            int? stars, 
            int? price) : base(hotelUid, name, country, city, address, stars, price)
        {
            HotelId = hotelId;
            HotelUid = hotelUid;
            Name = name;
            Country = country;
            City = city;
            Address = address;
            Stars = stars;
            Price = price;
        }
    }
}