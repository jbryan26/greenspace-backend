using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
    public class RoomModel
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string ResourceType { get; set; }
        public List<RoomFeaturesItem> RoomFeatures { get; set; }
        public bool IsCornerDesk { get; set; }
        public bool HasDockingStation { get; set; }
        public bool HasDualMonitors { get; set; }
        public bool IsFrontDesk { get; set; }
        public int SeatingCapacity { get; set; }

        public long FloorId { get; set; }

        public ICollection<FieldValue> FieldValues { get; set; }
    }
}
