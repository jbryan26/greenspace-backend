using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Region
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Site> Sites { get; set; }
    }
}
