﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    public class ReservationModel
    {
        public long Id { get; set; }
        public DateTime ReservationDate { get; set; }
      //  public string RoomName { get; set; }

    [JsonIgnore]
      [Newtonsoft.Json.JsonIgnore]
      public Room Room { get; set; }
      public long RoomId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
       
        public string ReservationHost { get; set; }
        public int ReservationAttendees { get; set; }
        public string ReservationType { get; set; }
        public bool HasAlcohol { get; set; }
        public string ReservationNotes { get; set; }

        public string AssignCategory { get; set; }

        public string ReservationTitle { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ApproveStatus Approved { get; set; }

        public List<FoodDetailsItem> FoodDetailItems { get; set; }


    }

    public enum ApproveStatus
    {
        Pending,
        Approved,
        Disapproved
    }
}
