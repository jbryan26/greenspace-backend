using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class FieldValue
    {
        public int Id { get; set; }

        public Field Field { get; set; }

        public long FieldId { get; set; }

        /// <summary>
        /// serialized value
        /// </summary>
        public string ValueString { get; set; }

        public bool ValueBool { get; set; }

        public int ValueInt { get; set; }

        public DateTime ValueDate { get; set; }



      

    }
}
