using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.DTO
{
    public class ReservationDto : ReservationModel
    {
        //  [JsonPropertyName("roomName")]
        
        public string RoomName { get; set; }
    }
}
