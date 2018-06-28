using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        public int Reservation_id { get; set; }
        public string Name { get; set; }
        public string From_date { get; set; } //date int or string?
        public string To_date { get; set; } //date int or string?
        public string Create_date { get; set; } //date int or string?
    }
}
