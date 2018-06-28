using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        public int park_id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string establish_date { get; set; } //date string or int?    
        public int area { get; set; }
        public int visitors { get; set; }
        public string description { get; set; }
    }
}
