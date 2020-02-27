using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Field
    {
        public long Id { get; set; }
        public string Name { get; set; }
       // public string DataType { get; set; }

        public string GroupsFilter { get; set; }
        public string FormsFilter { get; set; }

        public ParentType ParentType { get; set; }

        public TypeOfData DataType { get; set; } 
    }
}
