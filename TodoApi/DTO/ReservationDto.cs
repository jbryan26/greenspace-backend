using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.DTO
{
    public class ReservationDto : ReservationModel
    {
        public string RoomRoomName { get; set; }
    }
}
