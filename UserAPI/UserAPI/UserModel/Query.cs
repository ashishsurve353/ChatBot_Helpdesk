using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIReact.Model
{
    public class Query
    {
        public int QueryId { get; set; }
        public string queryname { get; set; }
        public string Department { get; set; }
        public string Descript { get; set; }
    }
}
