using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using System.Configuration;

namespace Capstone
{
    public class CapstoneCLI
    {
        private string connectionString;
        int parkChoice = -1;

        public CapstoneCLI()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        }

        public void RunCLI()
        {
            ParkMenu();
        }

        public void ParkMenu()
        {
            ParkSqlDAL parkSql = new ParkSqlDAL(connectionString);
            List<Park> parkList = parkSql.GetAllParks();

            for (int i = 1; i <= parkList.Count; ++i)
            {
                // print i + park.name;
                Console.WriteLine($"{i}) {parkList[i-1].Name}");
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
            ParkSqlDAL parkSql = new ParkSqlDAL(connectionString);
            Park park = parkSql.GetParkInfo();

            park.ToString();
            Console.ReadLine();


        }
    }
}
