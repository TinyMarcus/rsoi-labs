using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsBookingSystem.Services.ReservationService.Database.Models
{
    public class Hotel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("hotel_uid")]
        public Guid HotelUid { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("country")]
        public string Country { get; set; }
        
        [Column("city")]
        public string City { get; set; }
        
        [Column("address")]
        public string Address { get; set; }

        [Column("stars")]
        public int Stars { get; set; }
        
        [Column("price")]
        public int Price { get; set; }

        public Hotel(int id, 
            Guid hotelUid, 
            string name, 
            string country, 
            string city, 
            string address, 
            int stars, 
            int price)
        {
            Id = id;
            HotelUid = hotelUid;
            Name = name;
            Country = country;
            City = city;
            Address = address;
            Stars = stars;
            Price = price;
        }

        public Hotel(string name, 
            string country, 
            string city, 
            string address, 
            int stars, 
            int price)
        {
            Name = name;
            Country = country;
            City = city;
            Address = address;
            Stars = stars;
            Price = price;
        }

        public Hotel(Guid hotelUid, 
            string name, 
            string country, 
            string city, 
            string address, 
            int stars, 
            int price)
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
}