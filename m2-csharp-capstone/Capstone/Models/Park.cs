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

        public override string ToString()
        {
           /* Acadia National Park 
            * Location: Maine 
            * Established: 02 / 26 / 1919 
            * Area: 47,389 sq km 
            * Annual Visitors: 2,563,129


Covering most of Mount Desert Island and other coastal islands, Acadia features the tallest mountain on the Atlantic coast of the United States, granite peaks, ocean shoreline, woodlands, and lakes.There are freshwater, estuary, forest, and intertidal habitats.*/

            return base.ToString();
        }
    }
}
