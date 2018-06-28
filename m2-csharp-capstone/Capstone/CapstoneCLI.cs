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
                Console.WriteLine($"{i}) {parkList[i-1].name}");
            }

            Console.WriteLine("0) quit");

            while (parkChoice != 0) { 
                
                Console.Write("Choice: ");
                parkChoice = Convert.ToInt32(Console.ReadLine());
            }

            CMDMenu();
        }

        public void CMDMenu()
        {
            Park park = parkSql.GetParkInfo(parkChoice);

            park.ToString();
            Console.ReadLine();


        }
    }
}
