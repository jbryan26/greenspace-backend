using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoApi.Models
{
    public class Region
    {
        public long Id { get; set; }
        public string Name { get; set; }

       


        [JsonProperty("items")]

        public ICollection<Site> Sites { get; set; }
    }
}
