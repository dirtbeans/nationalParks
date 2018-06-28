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

            return $"{Name}\nLocation: {Location}\nEstablished: {Establish_date}\n{areaString} sq km\nAnnual Visitors: {visitorString}";
               


           /* Acadia National Park 
            * Location: Maine 
            * Established: 02 / 26 / 1919 
            * Area: 47,389 sq km 
            * Annual Visitors: 2,563,129


Covering most of Mount Desert Island and other coastal islands, Acadia features the tallest mountain on the Atlantic coast of the United States, granite peaks, ocean shoreline, woodlands, and lakes.There are freshwater, estuary, forest, and intertidal habitats.*/

          //  return base.ToString();
        }
    }
}
