using System.Collections.Generic;

namespace TodoApi.Models
{
    public class Building
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long SiteId { get; set; }
        public ICollection<Floor> Floors { get; set; }
    }
}