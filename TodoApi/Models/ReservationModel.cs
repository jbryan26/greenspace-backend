using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
    public class ReservationModel
    {
        public long Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public string RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ReservationTitle { get; set; }
        public string ReservationHost { get; set; }
        public int ReservationAttendees { get; set; }
        public string ReservationType { get; set; }
        public bool HasAlcohol { get; set; }
        public string ReservationNotes { get; set; }
        public List<FoodDetailsItem> FoodDetailItems { get; set; }
    }
}
