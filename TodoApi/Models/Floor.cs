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

        public Building Building { get; set; }

       

        [JsonProperty("items")]
        public ICollection<Room> Rooms { get; set; }
    }
}