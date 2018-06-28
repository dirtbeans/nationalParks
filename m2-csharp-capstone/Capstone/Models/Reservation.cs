using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        public int reservation_id { get; set; }
        public string name { get; set; }
        public string from_date { get; set; } //date int or string?
        public string to_date { get; set; } //date int or string?
        public string create_date { get; set; } //date int or string?
    }
}
