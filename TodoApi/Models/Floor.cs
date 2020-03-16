using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TodoApi.Models
{
    public class Floor
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long BuildingId { get; set; }

        public string ImageUrl { get; set; }

        [JsonProperty("items")]
        public ICollection<RoomModel> Rooms { get; set; }
    }
}