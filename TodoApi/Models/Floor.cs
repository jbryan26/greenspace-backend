using System.Collections.Generic;

namespace TodoApi.Models
{
    public class Floor
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long BuildingId { get; set; }

        public ICollection<RoomModel> Rooms { get; set; }
    }
}