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
            int parkChoice;

            for (int i = 1; i <= parkList.Count; ++i)
            {
                // print i + park.name;
                Console.WriteLine($"{i}) {parkList[i-1].Name}");
            }

            Console.WriteLine("0) quit");

            do
            {
                Console.Write("Choice: ");
                parkChoice = Convert.ToInt32(Console.ReadLine());
            } while (parkChoice <= 0 && parkChoice >= parkList.Count);

            CMDMenu(parkChoice);
        }

        public void CMDMenu(int parkChoice)
        {
            ParkSqlDAL parkSql = new ParkSqlDAL(connectionString);
            Park park = parkSql.GetParkInfo(parkChoice);
            int cmdChoice;
            
            Console.WriteLine("\n" + park.ToString() + "\n");

            do
            {
                Console.WriteLine($"Select a Command" +
                $"\n\t1) View Campgrounds" +
                $"\n\t2) Search for Reservation" +
                $"\n\t3) Return to Previous Screen" +
                $"\n\tChoice: ");
                cmdChoice = Convert.ToInt32(Console.ReadLine());
            } while (cmdChoice < 1 && cmdChoice > 3);

            if (cmdChoice < 3)
            {
                PrintAllCampgrounds();
            }
            
        }

        public void PrintAllCampgrounds()
        {
            CampgroundSqlDAL cgDAL = new CampgroundSqlDAL(connectionString);
            List<Campground> campgroundList = cgDAL.GetAllCampgrounds();

            for (int i = 1; i <= campgroundList.Count; ++i)
            {
                // print i + park.name;
                Console.WriteLine($"#{i}\t{campgroundList[i - 1].Name}" +
                    $"\t{campgroundList[i-1].Open_from_mm}" +
                    $"\t{campgroundList[i-1].Open_to_mm}" +
                    $"\t{campgroundList[i-1].Daily_fee}");
            }
        }
    }
}
