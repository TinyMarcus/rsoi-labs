using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HotelsBookingSystem.Services.ReservationService.Dto
{
    public class PaginationResponseDto
    {
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }
        
        [DataMember(Name = "pageSize", EmitDefaultValue = false)]
        public int? PageSize { get; set; }
        
        [DataMember(Name = "totalElements", EmitDefaultValue = false)]
        public int? TotalElements { get; set; }
        
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public List<HotelResponseDto>? Items { get; set; }

        public PaginationResponseDto(int? page, 
            int? pageSize, 
            int? totalElements, 
            List<HotelResponseDto>? items)
        {
            Page = page;
            PageSize = pageSize;
            TotalElements = totalElements;
            Items = items;
        }
    }
}