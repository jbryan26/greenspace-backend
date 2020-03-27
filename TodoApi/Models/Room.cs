﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TodoApi.Models
{
    public class Room
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public ResourceType ResourceType { get; set; }
        public List<RoomFeaturesItem> RoomFeatures { get; set; }
        public bool IsCornerDesk { get; set; }
        public bool HasDockingStation { get; set; }
        public bool HasDualMonitors { get; set; }
        public bool IsFrontDesk { get; set; }
        public long SeatingCapacity { get; set; }

        public long FloorId { get; set; }



        public string ImageUrl { get; set; }

        public ICollection<FieldValue> FieldValues { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}