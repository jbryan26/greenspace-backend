using System.Collections;
using System.Collections.Generic;

namespace TodoApi.Models
{
    public class Location
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string LocationText { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }

        public int NumberOfOccupants { get; set; }

        public bool HaveProjector { get; set; }

        public bool Catering { get; set; }

        public ICollection<FieldValue> FieldValues { get; set; }


    }
}