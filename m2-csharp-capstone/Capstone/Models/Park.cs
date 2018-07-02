using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        public int Park_id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Establish_date { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            string areaString = Area.ToString();
            string visitorString = Visitors.ToString();

            return $"{Name} National Park\n{"Location: ".PadRight(21)} {Location}\n{"Established: ".PadRight(21)} {Establish_date.ToShortDateString().PadRight(10)}\n{"Area: ".PadRight(21)} {areaString} sq km\n{"Annual Visitors: ".PadRight(21)} {visitorString.PadRight(10)} \n\n{Description}";
        }
    }
}
