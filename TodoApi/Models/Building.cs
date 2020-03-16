using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TodoApi.Models
{
    public class Building
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long SiteId { get; set; }

        public string ImageUrl { get; set; }

        [JsonProperty("items")]
        public ICollection<Floor> Floors { get; set; }
    }
}