using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Image
    {

        public long Id { get; set; }
        public Room Room { get; set; }

        public long RoomId { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        
    }
}
