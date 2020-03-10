using System.Collections.Generic;

namespace TodoApi.Models
{
    public class Site
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long RegionId { get; set; } 

        public ICollection<Building> Buildings { get; set; }
    }
}