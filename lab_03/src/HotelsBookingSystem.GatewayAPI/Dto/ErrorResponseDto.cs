using System;
using System.Runtime.Serialization;

namespace HotelsBookingSystem.GatewayAPI.Dto
{
    public class ErrorResponseDto
    {
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        public ErrorResponseDto(string message)
        {
            Message = message;
        }
    }
}