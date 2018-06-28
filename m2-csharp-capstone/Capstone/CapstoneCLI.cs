using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    public class CapstoneCLI
    {
        ParkSqlDAL parkSql = new ParkSqlDAL();
        int parkChoice = 0;

        public void RunCLI()
        {
            ParkMenu();
        }

        public void ParkMenu()
        {
            
            List<Park> parkList = parkSql.GetAllParks();

            for (int i = 1; i <= parkList.Count; ++i)
            {
                // print i + park.name;
            }
            // print q for quit

            //while (choice is invalid) { 
            // parkChoice = readline()
            //}

            CMDMenu();
        }

        public void CMDMenu()
        {
            Park park = parkSql.GetParkInfo(parkChoice);

            park.ToString();


        }
    }
}
