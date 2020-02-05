using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Field
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }

        public string GroupsFilter { get; set; }
        public string FormsFilter { get; set; }
    }
}
